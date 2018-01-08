using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GSCore;

namespace Gear.Net.ChannelPlugins.StreamTransfer
{
    /// <summary>
    /// Implements a <see cref="ChannelPlugin"/> that handles file and data stream transfer tasks for the attached <see cref="Channel"/>.
    /// </summary>
    public class StreamTransferPlugin : ChannelPlugin
    {
        #region Fields

        private static readonly object poolLock = new object();
        private static readonly Collection<StreamTransferProgressWorker> workers = new Collection<StreamTransferProgressWorker>();
        private static ushort lastAssignedDataPort;
        private static int maxGlobalSimultaneousTransfers;
        private readonly ObservableCollection<StreamTransferState> transfers;
        private readonly ReadOnlyObservableCollection<StreamTransferState> transfersRO;
        private readonly Dictionary<int, StreamTransferProgressWorker> boundWorkers = new Dictionary<int, StreamTransferProgressWorker>();
        //private readonly Queue<StreamTransferState> newStates = new Queue<StreamTransferState>();
        private readonly object processLock = new object();
        #endregion
        #region Constructors

        static StreamTransferPlugin()
        {
            TransferPortPoolStart = 55000;
            TransferPortPoolEnd = 55999;
            MaxGlobalSimultaneousTransfers = 2;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamTransferPlugin"/> class.
        /// </summary>
        public StreamTransferPlugin()
        {
            this.transfers = new ObservableCollection<StreamTransferState>();

            // Create read-only wrapper of transfers
            this.transfersRO = new ReadOnlyObservableCollection<StreamTransferState>(this.transfers);
        }

        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets a maximum number of global stream transfers across all instances.
        /// </summary>
        public static int MaxGlobalSimultaneousTransfers
        {
            get
            {
                return StreamTransferPlugin.maxGlobalSimultaneousTransfers;
            }

            set
            {
                StreamTransferPlugin.maxGlobalSimultaneousTransfers = value;

                StreamTransferPlugin.UpdateTransferWorkerPool();
            }
        }

        /// <summary>
        /// Gets or sets the lowest port number that will be used for stream transfer connections.
        /// </summary>
        public static ushort TransferPortPoolStart { get; set; }

        /// <summary>
        /// Gets or sets the highest port number that will be used for stream transfer connections.
        /// </summary>
        public static ushort TransferPortPoolEnd { get; set; }

        /// <summary>
        /// Gets a value indicating how many live (connected) sockets exist for data transfer between the local endpoint and the remote peer.
        /// </summary>
        public int LiveDataConnectionCount
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the system this <see cref="StreamTransferPlugin"/> is running on is capable of
        /// hosting active transfers.
        /// </summary>
        /// <remarks>
        /// If this system is behind a NAT device, or a firewall, it will likely not support hosting active transfers.
        /// </remarks>
        public bool CanHostActiveTransfers { get; set; }

        /// <summary>
        /// Gets an observable collection of all file transfers associated with this <see cref="StreamTransferPlugin"/> in it's lifetime.
        /// </summary>
        public ReadOnlyObservableCollection<StreamTransferState> FileTransfers
        {
            get
            {
                return this.transfersRO;
            }
        }

        #endregion
        #region Methods

        public static StreamTransferProgressWorker BindNextAvailableTransferWorker(StreamTransferState transferState)
        {
            if (transferState.Worker == null)
            {
                lock (StreamTransferPlugin.workers)
                {
                    StreamTransferPlugin.UpdateTransferWorkerPool();

                    var available = workers.FirstOrDefault(e => e.IsIdle);

                    if (available != null)
                    {
                        available.TransferState = transferState;
                        transferState.Worker = available;
                    }

                    return available;
                }
            }
            else
            {
                return transferState.Worker;
            }
        }

        /// <summary>
        /// Returns a new <see cref="Socket"/> instance, which is already in a listening state.
        /// The new socket is bound to the next available port number in the configured data port pool.
        /// </summary>
        /// <returns>A new <see cref="Socket"/> instance, or null if the data port pool is exhausted.</returns>
        public static Socket GetNextDataPortListener()
        {
            lock (StreamTransferPlugin.poolLock)
            {
                int tries = 0;
                int max = Math.Abs(StreamTransferPlugin.TransferPortPoolStart - StreamTransferPlugin.TransferPortPoolEnd);

                while (tries < max)
                {
                    var port = StreamTransferPlugin.lastAssignedDataPort++;

                    var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    try
                    {
                        socket.Bind(new IPEndPoint(IPAddress.Any, port));
                        socket.Listen(1);

                        return socket;
                    }
                    catch
                    {
                        socket.Close();
                        socket.Dispose();
                        continue;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Overridden. Attaches this <see cref="StreamTransferPlugin"/> instance to the specified <see cref="Channel"/>.
        /// </summary>
        /// <param name="channel">The <see cref="Channel"/> to attach to.</param>
        public override void Attach(Channel channel)
        {
            this.Detach(this.AttachedChannel);

            if (channel != null)
            {
                channel.RegisterHandler<StreamDataPortReadyMessage>(this.Handle_StreamDataPortReadyMessage, this);
                channel.RegisterHandler<TransferStreamMessage>(this.Handle_TransferStreamMessage, this);
            }

            this.AttachedChannel = channel;
        }

        /// <summary>
        /// Overridden. Detaches this <see cref="StreamTransferPlugin"/> instance from the specified <see cref="Channel"/>.
        /// </summary>
        /// <param name="channel">The <see cref="Channel"/> to detach from.</param>
        public override void Detach(Channel channel)
        {
            if (channel != null)
            {
                channel.UnregisterHandler(this);
            }

            this.AttachedChannel = null;
        }

        /// <summary>
        /// Initiates a stream transfer of the file at the specified path.
        /// </summary>
        /// <param name="filePath">The local path of the file to send.</param>
        /// <returns>A <see cref="StreamTransferState"/> object that can be used to track the transfer operation.</returns>
        public StreamTransferState SendFile(string filePath)
        {
            Contract.Requires<ArgumentNullException>(filePath != null);
            Contract.Requires<InvalidOperationException>(this.IsAttached);

            Log.Write(LogMessageGroup.Informational, "Queuing {0} for sending to remote peer", filePath);

            // Sanity checking:
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("No file found at the specified path.", filePath);
            }

            filePath = Path.GetFullPath(filePath);
            var finfo = new FileInfo(filePath);

            // Build transfer state:
            var state = new StreamTransferState(finfo);
            state.LocalDirection = TransferDirection.Outgoing;
            state.LocalStream = finfo.OpenRead();
            state.ProgressHint = TransferProgressHint.Queued;

            // Queue transfer
            this.AddTransferState(state);

            Task.Run(() => this.ProcessStateQueue());

            return state;
        }

        protected void ProcessStateQueue()
        {
            try
            {
                if (Monitor.TryEnter(this.processLock))
                {
                    // Collect any queued (but not initiated) transfers)
                    var waiting = this.transfers.Where(e => e.ProgressHint == TransferProgressHint.Queued).ToArray();

                    Log.Write(LogMessageGroup.Debug, "Processing state queue on thread {0} - {1} queued waiting.", Thread.CurrentThread.ManagedThreadId, waiting.Length);
                    foreach (var state in waiting)
                    {
                        if (state == null)
                        {
                            Log.Write(LogMessageGroup.Critical, "Null stream transfer state object in collection.");
                            throw new Exception();
                        }

                        if (state.Parent != null)
                        {
                            if (state.Parent != this)
                            {
                                Log.Write(LogMessageGroup.Critical, "Incorrect transfer state <-> plugin parent relationship.");
                                throw new Exception();
                            }
                        }
                        else
                        {
                            state.Parent = this;
                        }

                        // Attempt to attach the new state to a transfer worker.
                        var worker = StreamTransferPlugin.BindNextAvailableTransferWorker(state);

                        if (worker != null)
                        {
                            state.ProgressHint = TransferProgressHint.Initiated;

                            worker.Start();
                        }
                        else
                        {
                            // Bail out because we're out of available workers.
                            return;
                        }
                    }
                }
                else
                {
                    Log.Write(LogMessageGroup.Debug, "Another thread already processing state queue.");
                }
            }
            finally
            {
                if (Monitor.IsEntered(this.processLock))
                {
                    Monitor.Exit(this.processLock);
                }
            }
        }

        protected static void UpdateTransferWorkerPool()
        {
            lock (workers)
            {
                // Clean up dead workers
                var dead = workers.Where(e => !e.IsAlive).ToArray();
                for (int i = 0; i < dead.Length; ++i)
                {
                    workers.Remove(dead[i]);
                }

                if (workers.Count < MaxGlobalSimultaneousTransfers)
                {
                    // Add workers up to the max
                    var add = MaxGlobalSimultaneousTransfers - workers.Count;

                    for (int i = 0; i < add; ++i)
                    {
                        var worker = new StreamTransferProgressWorker();

                        workers.Add(worker);
                    }
                }
                else if (workers.Count > MaxGlobalSimultaneousTransfers)
                {
                    // Purge extra workers
                    var remove = workers.Count - MaxGlobalSimultaneousTransfers;

                    var extra = workers.OrderBy(k => k.IsIdle).Take(remove).ToArray();

                    foreach (var worker in extra)
                    {
                        worker.FlagDestroyed();

                        if (worker.IsIdle)
                        {
                            workers.Remove(worker);
                        }
                    }
                }
            }
        }

        protected virtual void Handle_StreamDataPortReadyMessage(MessageEventArgs e, StreamDataPortReadyMessage message)
        {
            Log.Write(LogMessageGroup.Debug, "Transfer {0} - Remote peer listening on TCP port {1} for data connection.", message.TransferId, message.DataPort);
            var state = this.transfers.First(t => t.TransferId == message.TransferId);
            state.DataConnection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            state.DataConnection.Connect(new IPEndPoint(this.AttachedChannel.RemoteEndPoint.Address, message.DataPort));
            state.ProgressStep.Set();
            //state.Worker.Start();
        }

        protected virtual void Handle_TransferStreamMessage(MessageEventArgs e, TransferStreamMessage message)
        {
            Contract.Requires<ArgumentNullException>(message != null);

            Log.Write(LogMessageGroup.Debug, "Received stream transfer request for file {0} from {1}", message.TransferState.Name, e.Sender);

            var state = message.TransferState;
            state.Parent = this;
            state.LocalDirection = TransferDirection.Incoming;
            state.TransferInitiatedAt = DateTime.Now;

            this.AddTransferState(state);

            Task.Run(() => this.ProcessStateQueue());
        }

        protected void AddTransferState(StreamTransferState state)
        {
            lock (this.processLock)
            {
                state.Completed += this.HandleTransferStateCompletion;
                state.Aborted += this.HandleTransferStateAbortion;
                this.transfers.Add(state);
            }
        }

        protected virtual void HandleTransferStateCompletion(object sender, EventArgs e)
        {
            // cleanup transfer state
            var state = sender as StreamTransferState;

            state.Completed -= this.HandleTransferStateCompletion;
            state.Aborted -= this.HandleTransferStateAbortion;

            state.Worker?.Recycle();

            this.ProcessStateQueue();
        }

        protected virtual void HandleTransferStateAbortion(object sender, EventArgs e)
        {
            this.ProcessStateQueue();
        }

        [ContractInvariantMethod]
        private void ContractInvariants()
        {
            Contract.Invariant(this.transfers != null);
            Contract.Invariant(this.FileTransfers != null);
            //Contract.Invariant(this.newStates != null);
            Contract.Invariant(this.boundWorkers != null);
        }
        #endregion
    }
}
