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

        /// <summary>
        /// Holds the default buffer size (in bytes) used for individual reads/writes/sends/receives during stream transfer operations.
        /// </summary>
        public const int DefaultBufferSize = 65000;

        /// <summary>
        /// Holds the default value for the low end of the TCP port pool used for transfer operations.
        /// </summary>
        public const ushort DefaultTransferPortPoolStart = 55000;
        public const ushort DefaultTransferPortPoolEnd = 59999;
        public const int DefaultMaxGlobalActiveWorkers = 1;

        private static readonly object poolLock = new object();
        private static readonly Collection<StreamTransferProgressWorker> workers = new Collection<StreamTransferProgressWorker>();
        private static ushort lastAssignedDataPort;
        private static int maxGlobalSimultaneousTransfers;
        private readonly ObservableCollection<StreamTransferState> transfers;
        private readonly object processLock = new object();
        private Timer workerWatchdog;
        #endregion
        #region Constructors

        static StreamTransferPlugin()
        {
            TransferPortPoolStart = DefaultTransferPortPoolStart;
            TransferPortPoolEnd = DefaultTransferPortPoolEnd;
            MaxGlobalActiveWorkers = DefaultMaxGlobalActiveWorkers;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamTransferPlugin"/> class.
        /// </summary>
        public StreamTransferPlugin()
        {
            // Defaults:
            this.AutoRetryFailedTransfers = true;
            this.WatchdogTimeout = TimeSpan.FromSeconds(30);

            this.transfers = new ObservableCollection<StreamTransferState>();

            this.workerWatchdog = new Timer(this.RunWatchdog, null, this.WatchdogTimeout, Timeout.InfiniteTimeSpan);
        }

        #endregion
        #region Events

        /// <summary>
        /// Notifies subscribers when this instance receives a stream transfer from the remote endpoint associated with the <see cref="ChannelPlugin.AttachedChannel"/>.
        /// </summary>
        public event EventHandler<StreamTransferEventArgs> TransferReceived;

        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets a maximum number of global stream transfers across all instances.
        /// </summary>
        public static int MaxGlobalActiveWorkers
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
        public ObservableCollection<StreamTransferState> Transfers
        {
            get
            {
                return this.transfers;
            }
        }

        public bool AutoRetryFailedTransfers { get; set; }

        public TimeSpan WatchdogTimeout { get; set; }
        #endregion
        #region Methods

        /// <summary>
        /// Gets an available <see cref="StreamTransferProgressWorker"/> and attaches the specified <see cref="StreamTransferState"/> to it.
        /// </summary>
        /// <param name="transferState">The <see cref="StreamTransferState"/> instance to attach to an available worker.</param>
        /// <returns>The <see cref="StreamTransferProgressWorker"/> instance that was attached, or null if no workers were available.</returns>
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

                StreamTransferPlugin.lastAssignedDataPort = Math.Max(StreamTransferPlugin.lastAssignedDataPort, StreamTransferPlugin.TransferPortPoolStart);

                while (tries < max)
                {
                    if (StreamTransferPlugin.lastAssignedDataPort > StreamTransferPlugin.TransferPortPoolEnd)
                    {
                        StreamTransferPlugin.lastAssignedDataPort = StreamTransferPlugin.TransferPortPoolStart;
                    }

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

        protected static void UpdateTransferWorkerPool()
        {
            lock (workers)
            {
                // Clean up dead workers
                var dead = workers.Where(e => !e.IsAlive).ToArray();
                if (dead.Length > 0)
                {
                    Log.Write(LogMessageGroup.Debug, "Purging {0} dead stream transfer workers.", dead.Length);

                    for (int i = 0; i < dead.Length; ++i)
                    {
                        workers.Remove(dead[i]);
                    }
                }

                if (workers.Count < MaxGlobalActiveWorkers)
                {
                    // Add workers up to the max
                    var add = MaxGlobalActiveWorkers - workers.Count;
                    Log.Write(LogMessageGroup.Debug, "Adding {0} new stream transfer workers.", add);
                    for (int i = 0; i < add; ++i)
                    {
                        var worker = new StreamTransferProgressWorker();

                        workers.Add(worker);
                    }
                }
                else if (workers.Count > MaxGlobalActiveWorkers)
                {
                    // Purge extra workers
                    var remove = workers.Count - MaxGlobalActiveWorkers;

                    var extra = workers.OrderBy(k => k.IsIdle).Take(remove).ToArray();

                    foreach (var worker in extra)
                    {
                        worker.FlagDestroyed();

                        if (worker.IsIdle)
                        {
                            Log.Write(LogMessageGroup.Debug, "Removing idle transfer worker due to MaxGlobalActiveWorkers threshold exceeded.");
                            workers.Remove(worker);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Overridden. Attaches this <see cref="StreamTransferPlugin"/> instance to the specified <see cref="Channel"/>.
        /// </summary>
        /// <param name="channel">The <see cref="Channel"/> to attach to.</param>
        protected override void DoAttach(Channel channel)
        {
            if (channel != null)
            {
                channel.RegisterHandler<StreamDataPortReadyMessage>(this.Handle_StreamDataPortReadyMessage, this);
                channel.RegisterHandler<TransferStreamMessage>(this.Handle_TransferStreamMessage, this);

                var cc = channel as ConnectedChannel;

                if (cc != null)
                {
                    cc.Disconnected += this.AttachedChannel_Disconnected;
                    cc.Connected += this.AttachedChannel_Connected;
                }
            }

            this.AttachedChannel = channel;
        }

        private void AttachedChannel_Connected(object sender, EventArgs e)
        {
        }

        private void AttachedChannel_Disconnected(object sender, ChannelDisconnectedEventArgs e)
        {
            // Fail any in-progress transfers and force stop all workers.
            this.AbortActiveTransfers();
        }

        /// <summary>
        /// Overridden. Detaches this <see cref="StreamTransferPlugin"/> instance from the specified <see cref="Channel"/>.
        /// </summary>
        /// <param name="channel">The <see cref="Channel"/> to detach from.</param>
        protected override void DoDetach(Channel channel)
        {
            if (channel != null)
            {
                channel.UnregisterHandler(this);

                var cc = channel as ConnectedChannel;

                if (cc != null)
                {
                    cc.Disconnected -= this.AttachedChannel_Disconnected;
                    cc.Connected -= this.AttachedChannel_Connected;
                }
            }

            this.AttachedChannel = null;

            this.AbortActiveTransfers();

            this.Transfers.Clear();
        }

        private void AbortActiveTransfers()
        {
            lock (this.processLock)
            {
                var active = this.transfers.Where(t => t.ProgressHint != TransferProgressHint.Queued && t.ProgressHint != TransferProgressHint.Completed);

                foreach (var trans in active)
                {
                    trans.Worker?.ForceStop();
                    trans.ProgressHint = TransferProgressHint.Failed;
                    trans.Worker?.Recycle();
                }
            }
        }

        /// <summary>
        /// Queues a <see cref="Stream"/> for sending to the remote peer.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to transfer.</param>
        /// <param name="extendedAttributes">Optional. A dictionary of extended metadata to include with the stream transfer.</param>
        /// <returns>A <see cref="StreamTransferState"/> object used to track the progress of the transfer.</returns>
        public StreamTransferState QueueStream(Stream stream, Dictionary<string, string> extendedAttributes = null)
        {
            var state = new StreamTransferState();

            state.LocalStream = stream;
            state.LocalDirection = TransferDirection.Outgoing;
            state.LocalPath = string.Empty;
            state.Length = stream.Length;
            state.ExtendedAttributes = extendedAttributes;

            this.AddTransferState(state);

            return state;
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

        public void ProcessQueue()
        {
            Task.Run(() => this.ProcessStateQueue());
        }

        protected void ProcessStateQueue()
        {
            try
            {
                if (Monitor.TryEnter(this.processLock))
                {
                    this.ResetWatchdog();

                    // Only process if connection is open (if relevant):
                    if (this.AttachedChannel is ConnectedChannel && ((ConnectedChannel)this.AttachedChannel).State == ChannelState.Disconnected)
                    {
                        return;
                    }


                    // Collect any queued (but not initiated) transfers)
                    var waiting = this.transfers.Where(e => e.ProgressHint == TransferProgressHint.Queued).ToArray();

                    if (waiting.Length > 0)
                    {
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

        protected void RunWatchdog(object state)
        {
            var qc = this.Transfers.Count(e => e.ProgressHint == TransferProgressHint.Queued);

            if (qc > 0)
            {
                Log.Write(
                    LogMessageGroup.Important,
                    "Stream transfer watchdog invoked on thread {0}; {1} queued transfers.",
                    Thread.CurrentThread.ManagedThreadId,
                    qc);
            }

            lock (this.processLock)
            {
                // Collect stuck transfers:
                var stuckTransfers = this.Transfers.Where(e => e.Worker != null).ToArray();
                var stuckThreshold = TimeSpan.FromSeconds(120);

                foreach (var t in stuckTransfers)
                {
                    var stuckFor = t.Worker.LastActivity - DateTime.Now;

                    if (stuckFor > stuckThreshold)
                    {
                        Log.Write(LogMessageGroup.Important, "Aborting stuck transfer {0}.", t.TransferId);
                        t.ProgressHint = TransferProgressHint.Failed;
                        t.Worker.Recycle();
                    }
                }

                // Collect all failed transfers:
                var ftrans = this.Transfers.Where(e => e.ProgressHint == TransferProgressHint.Failed && e.Worker != null).ToArray();

                if (ftrans.Length > 0)
                {
                    Log.Write(LogMessageGroup.Important, "Recycling {0} workers with failed transfers", ftrans.Length);

                    foreach (var t in ftrans)
                    {
                        if (t.Worker != null)
                        {
                            t.Worker.Recycle();
                        }
                    }
                }

                if (this.AutoRetryFailedTransfers)
                {
                    var outTrans = this.Transfers.Where(e => e.ProgressHint == TransferProgressHint.Failed && e.LocalDirection == TransferDirection.Outgoing).ToArray();

                    foreach (var t in outTrans)
                    {
                        Log.Write(LogMessageGroup.Normal, "Transfer {0} - Retrying.", t.TransferId);
                        t.ProgressHint = TransferProgressHint.Queued;
                    }
                }
            }

            this.ProcessQueue();

            this.ResetWatchdog();
        }

        protected internal void ResetWatchdog()
        {
            this.workerWatchdog.Change(this.WatchdogTimeout, Timeout.InfiniteTimeSpan);
        }

        protected virtual void Handle_StreamDataPortReadyMessage(MessageEventArgs e, StreamDataPortReadyMessage message)
        {
            Log.Write(LogMessageGroup.Debug, "Transfer {0} - Remote peer listening on TCP port {1} for data connection.", message.TransferId, message.DataPort);
            var state = this.transfers.First(t => t.TransferId == message.TransferId);
            try
            {
                state.DataConnection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                state.DataConnection.Connect(new IPEndPoint(this.AttachedChannel.RemoteEndPoint.Address, message.DataPort));
                state.ProgressStep.Set();
            }
            catch (Exception ex)
            {
                Log.Write(
                    LogMessageGroup.Severe,
                    "Transfer {0} - Exception occurred while connecting to data transfer port {1}. Message: {2}",
                    message.TransferId,
                    message.DataPort,
                    ex.Message);

                state.ProgressHint = TransferProgressHint.Failed;
            }
        }

        protected virtual void Handle_TransferStreamMessage(MessageEventArgs e, TransferStreamMessage message)
        {
            Contract.Requires<ArgumentNullException>(message != null);

            Log.Write(LogMessageGroup.Debug, "Received stream transfer metadata for stream {0} from {1}", message.TransferState.Name, e.Sender);

            // Look for existing transfer state
            var state = this.Transfers.FirstOrDefault(t => t.TransferId == message.TransferState.TransferId);

            if (state != null)
            {
                Log.Write("Transfer {0} - Sender is retrying.", state.TransferId);
                state.ProgressHint = TransferProgressHint.Queued;
            }
            else
            {
                state = message.TransferState;
                state.Parent = this;
                state.LocalDirection = TransferDirection.Incoming;
                state.TransferInitiatedAt = DateTime.Now;

                this.AddTransferState(state);
            }

            this.OnTransferReceived(new StreamTransferEventArgs(state));

            this.ProcessQueue();
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

            if (state.LocalDirection == TransferDirection.Outgoing)
            {
                state.LocalStream?.Close();
            }

            state.Completed -= this.HandleTransferStateCompletion;
            state.Aborted -= this.HandleTransferStateAbortion;

            state.Worker?.Recycle();

            this.ProcessStateQueue();
        }

        protected virtual void HandleTransferStateAbortion(object sender, EventArgs e)
        {
            var state = sender as StreamTransferState;

            //state.LocalStream?.Close();

            state.Worker.Recycle();

            this.ProcessStateQueue();
        }

        protected virtual void OnTransferReceived(StreamTransferEventArgs e)
        {
            this.TransferReceived?.Invoke(this, e);

            if (e.TransferState.LocalStream == null)
            {
                e.TransferState.LocalStream = new MemoryStream();
            }
        }

        [ContractInvariantMethod]
        private void ContractInvariants()
        {
            Contract.Invariant(this.transfers != null);
        }
        #endregion
    }
}
