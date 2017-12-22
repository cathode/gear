using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gear.Net.Messages;

namespace Gear.Net
{
    /// <summary>
    /// Implements a framework for coordinating connections from clients.
    /// </summary>
    public class MessagingServer
    {
        #region Fields
        private readonly ObservableCollection<PeerSession> peers;
        private readonly List<MessagingServerPendingClient> pending;
        private readonly ConnectionListener listener;
        private readonly object syncLock = new object();
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingServer"/> class.
        /// </summary>
        /// <param name="port">The TCP port number that peers will connect on.</param>
        public MessagingServer(ushort port)
        {
            this.listener = new ConnectionListener(port);
            this.peers = new ObservableCollection<PeerSession>();

            this.pending = new List<MessagingServerPendingClient>();

            this.listener.ChannelConnected += this.Listener_ChannelConnected;
        }

        #endregion
        #region Events

        /// <summary>
        /// Notifies subscribes when a new peer connects to this messaging server.
        /// </summary>
        public event EventHandler<PeerEventArgs> PeerConnected;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a collection of network addresses or ranges that are explicitly allowed to connect to this end point.
        /// </summary>
        public NetworkList Allowed { get; set; }

        /// <summary>
        /// Gets or sets a collection of network addresses or ranges that are explicitly denied from connecting to this end point.
        /// </summary>
        public NetworkList Denied { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="TimeSpan"/> that determines how much time must pass without receiving a message from a peer for the peer to be considered idle.
        /// </summary>
        public TimeSpan IdleThreshold { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="TimeSpan"/> that determines how much time must pass before an idle peer is forcibly disconnected.
        /// </summary>
        public TimeSpan IdleDisconnectThreshold { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="TimeSpan"/> that determines how much time must pass after a peer becomes dead before the peer's local metadata cache is purged and resources that were used by the peer are cleaned up.
        /// </summary>
        public TimeSpan DeadExpirationThreshold { get; set; }

        /// <summary>
        /// Gets the <see cref="ConnectionListener"/> that the messaging server is using to listen for new connections.
        /// </summary>
        public ConnectionListener Listener
        {
            get
            {
                return this.listener;
            }
        }

        public ServerMetadata ServerMetadata { get; private set; }

        /// <summary>
        /// Gets a collection of peers that are known to the messaging server.
        /// </summary>
        public ObservableCollection<PeerSession> Peers
        {
            get
            {
                return this.peers;
            }
        }
        #endregion
        #region Methods

        /// <summary>
        /// Starts the messaging server.
        /// </summary>
        public void Start()
        {
            this.listener.StartInBackground();
        }

        /// <summary>
        /// Stops all messaging activity related to the current messaging server.
        /// </summary>
        /// <param name="closeExistingConnections">If true, existing peer connections are stopped; otherwise, only new connections are stopped.</param>
        /// <param name="immediate">If true, existing peer connections are simply dropped without warning; otherwise peer connections are shut down gracefully.</param>
        /// <remarks>
        /// This method does not block, even if the <paramref name="immediate"/> parameter is passed as true.
        /// </remarks>
        public void Stop(bool closeExistingConnections = true, bool immediate = false)
        {
        }

        protected virtual void Listener_ChannelConnected(object sender, ChannelEventArgs e)
        {
            var cc = e.Channel as ConnectedChannel;

            if (cc != null)
            {
                var pc = new MessagingServerPendingClient();
                pc.ConnectedAt = DateTime.Now;
                pc.Connection = cc;

                lock (this.syncLock)
                {
                    this.pending.Add(pc);
                }

                cc.RegisterHandler<PeerGreetingMessage>(this.MessageHandler_PeerGreeting, this);
            }
        }

        protected virtual void MessageHandler_PeerGreeting(MessageEventArgs e, PeerGreetingMessage message)
        {
            // Check if the connection is still a pending client:
            lock (this.syncLock)
            {
                var pc = this.pending.FirstOrDefault(p => p.Connection == e.Channel);

                if (pc != null)
                {

                }
            }

            if (message.IsResponseRequested)
            {
                var reply = new PeerGreetingMessage();
                reply.IsResponseRequested = false;
                reply.Metadata = this.ServerMetadata;

                e.Channel.Send(reply);
            }
        }

        protected virtual void PendingGC()
        {
            // Drop connections that have exceeded the idle threshold.

            if (Monitor.TryEnter(this.syncLock))
            {
                try
                {
                    var now = DateTime.Now;

                    var deads = new List<MessagingServerPendingClient>();

                    foreach (var pc in this.pending)
                    {
                        var delta = pc.ConnectedAt - now;

                        // (pc.ConnectedAt - now)
                    }
                }
                catch
                {

                }
            }
        }

        [ContractInvariantMethod]
        private void ContractInvariants()
        {
            Contract.Invariant(this.Peers != null);
            Contract.Invariant(this.Listener != null);
        }
        #endregion
    }
}
