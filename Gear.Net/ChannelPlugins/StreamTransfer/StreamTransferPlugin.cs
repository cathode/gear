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
        private readonly Queue<StreamTransferState> newStates = new Queue<StreamTransferState>();
        #endregion
        #region Constructors

        static StreamTransferPlugin()
        {
            TransferPortPoolStart = 55000;
            TransferPortPoolEnd = 55999;
            MaxGlobalSimultaneousTransfers = 1;
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
            lock (StreamTransferPlugin.workers)
            {
                var available = workers.FirstOrDefault(e => e.IsIdle);

                if (available != null)
                {
                    available.TransferState = transferState;
                }

                return available;
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
            this.transfers.Add(state);
            this.newStates.Enqueue(state);

            this.ProcessNewStates();

            var msg = new TransferStreamMessage();
            msg.TransferState = state;

            if (!this.CanHostActiveTransfers)
            {
                msg.RequestDataPort = true;
            }
            else
            {
                var listener = StreamTransferPlugin.GetNextDataPortListener();
                state.DataConnection = listener;

                msg.RequestDataPort = false;
                msg.DataPort = (ushort)((IPEndPoint)listener.LocalEndPoint).Port;
            }

            this.AttachedChannel.Send(msg);

            StreamTransferPlugin.BindNextAvailableTransferWorker(state);

            return state;
        }

        protected static void UpdateTransferWorkerPool()
        {
            lock (workers)
            {
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
            var state = this.transfers.First(t => t.TransferId == message.TransferId);

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(new IPEndPoint(this.AttachedChannel.RemoteEndPoint.Address, message.DataPort));

            var buffer = File.ReadAllBytes(state.LocalPath);
            socket.Send(buffer);
        }

        protected virtual void Handle_TransferStreamMessage(MessageEventArgs e, TransferStreamMessage message)
        {
            Contract.Requires<ArgumentNullException>(message != null);

            var state = message.TransferState;
            state.Parent = this;
            state.LocalDirection = TransferDirection.Incoming;
            state.TransferInitiatedAt = DateTime.Now;

            this.transfers.Add(state);
            this.newStates.Enqueue(state);

            this.ProcessNewStates();
        }

        protected void ProcessNewStates()
        {
            try
            {
                if (Monitor.TryEnter(this.newStates))
                {
                    while (this.newStates.Count > 0)
                    {
                        var state = this.newStates.Peek();
                        var worker = StreamTransferPlugin.BindNextAvailableTransferWorker(state);

                        if (worker != null)
                        {
                            Contract.Assume(state != null);
                            Contract.Assume(this.newStates.Count > 0);

                            this.newStates.Dequeue();
                            this.boundWorkers.Add(state.TransferId, worker);
                        }
                        else
                        {
                            // Bail out because we're out of available workers.
                            return;
                        }
                    }
                }
            }
            finally
            {
                if (Monitor.IsEntered(this.newStates))
                {
                    Monitor.Exit(this.newStates);
                }
            }
        }

        [ContractInvariantMethod]
        private void ContractInvariants()
        {
            Contract.Invariant(this.transfers != null);
            Contract.Invariant(this.FileTransfers != null);
            Contract.Invariant(this.newStates != null);
            Contract.Invariant(this.boundWorkers != null);
        }
        #endregion
    }
}
