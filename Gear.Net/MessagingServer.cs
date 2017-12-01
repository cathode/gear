﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    /// <summary>
    /// Implements a framework for coordinating connections from clients.
    /// </summary>
    public class MessagingServer
    {
        #region Fields
        private readonly ObservableCollection<PeerMetadata> peers;
        private readonly ReadOnlyObservableCollection<PeerMetadata> peersRO;
        private ushort listenPort;
        private ConnectionListener listener;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingServer"/> class.
        /// </summary>
        public MessagingServer()
        {
            this.peers = new ObservableCollection<PeerMetadata>();
            this.peersRO = new ReadOnlyObservableCollection<PeerMetadata>(this.peers);

        }

        #endregion

        #region Events
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

        public ReadOnlyObservableCollection<PeerMetadata> Peers
        {
            get
            {
                return this.peersRO;
            }
        }
        #endregion
        #region Methods

        /// <summary>
        /// Starts the messaging server.
        /// </summary>
        public void Start()
        {
            if (this.listener == null)
            {
                //this.listener = new ConnectionListener(this.ListenPort);
            }

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
            throw new NotImplementedException();
        }

        [ContractInvariantMethod]
        private void ContractInvariants()
        {
            Contract.Invariant(this.Peers != null);
        }
        #endregion
    }
}
