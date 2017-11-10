using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    /// <summary>
    /// Represents basic information for a network peer.
    /// </summary>
    public class PeerMetadata
    {
        /// <summary>
        /// Gets or sets the unique id of the peer.
        /// </summary>
        public Guid PeerId { get; set; }

        /// <summary>
        /// Gets or sets the version of the program that the peer is using.
        /// </summary>
        /// <remarks>
        /// This is not necessarily the same as the version of the Gear.Net library.
        /// </remarks>
        public Version SoftwareVersion { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> that the current connection to this peer was established.
        /// </summary>
        public DateTime ConnectedAt { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> that indicates when the last message was sent by the peer.
        /// </summary>
        public DateTime LastSeenAt { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IPEndPoint"/> that describes the client's network address.
        /// </summary>
        public IPAddress PeerAddress { get; set; }

        public PeerStatus Status { get; set; }

        public PeerRole Role { get; set; }
    }
}
