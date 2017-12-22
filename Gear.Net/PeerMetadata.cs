using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net
{
    /// <summary>
    /// Represents basic information for a network peer.
    /// </summary>
    [ProtoContract]
    public class PeerMetadata : INotifyPropertyChanged
    {
        #region Fields
        private Guid peerId;
        private Version softwareVersion;
        private DateTime connectedAt;
        private DateTime lastSeenAt;
        private IPEndPoint peerAddress;
        private PeerStatus status;
        private PeerRole role;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PeerMetadata"/> class.
        /// </summary>
        public PeerMetadata()
        {
            this.PeerId = Guid.NewGuid();
        }

        #endregion
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the unique id of the peer.
        /// </summary>
        [ProtoMember(0)]
        public Guid PeerId
        {
            get
            {
                return this.peerId;
            }

            set
            {
                this.peerId = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the version of the program that the peer is using.
        /// </summary>
        /// <remarks>
        /// This is not necessarily the same as the version of the Gear.Net library.
        /// </remarks>
        [ProtoMember(1)]
        public Version SoftwareVersion
        {
            get
            {
                return this.softwareVersion;
            }

            set
            {
                this.softwareVersion = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> that the current connection to this peer was established.
        /// </summary>
        [ProtoMember(2)]
        public DateTime ConnectedAt
        {
            get

            {
                return this.connectedAt;
            }

            set
            {
                this.connectedAt = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> that indicates when the last message was sent by the peer.
        /// </summary>
        [ProtoMember(3)]
        public DateTime LastSeenAt
        {
            get
            {
                return this.lastSeenAt;
            }

            set
            {
                this.lastSeenAt = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="IPEndPoint"/> that describes the client's network address.
        /// </summary>
        [ProtoMember(4)]
        public IPEndPoint PeerAddress
        {
            get
            {
                return this.peerAddress;
            }

            set
            {
                this.peerAddress = value;
                this.OnPropertyChanged();
            }
        }

        [ProtoIgnore]
        public PeerStatus Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
                this.OnPropertyChanged();
            }
        }

        [ProtoIgnore]
        public PeerRole Role
        {
            get
            {
                return this.role;

            }

            set
            {
                this.role = value;
                this.OnPropertyChanged();
            }
        }
        #endregion
        #region Methods

        /// <summary>
        /// Assists the implementation of the <see cref="INotifyPropertyChanged"/> interface. Raises the <see cref="ClientInfo.PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void OnPropertyChanged([CallerMemberName]string propertyName = "unknown")
        {
            Contract.Requires<ArgumentNullException>(propertyName != null);

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
