using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    /// <summary>
    /// Indicates supported role categories for network peers.
    /// </summary>
    public enum PeerRole
    {
        /// <summary>
        /// Indicates the peer's role is unknown.
        /// </summary>
        Unknown = 0x00,

        /// <summary>
        /// Indicates the peer is operating as a client.
        /// </summary>
        Client = 0x01,

        /// <summary>
        /// Indicates the peer is operating as a server.
        /// </summary>
        Server = 0x02,

        /// <summary>
        /// Indicates the peer is operating as a message broker, e.g. a gateway or relay.
        /// </summary>
        Broker = 0x03
    }
}
