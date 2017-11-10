using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    /// <summary>
    /// Enumerates logical statuses that a peer can exist in.
    /// </summary>
    public enum PeerStatus
    {
        /// <summary>
        /// Indicates the peer is idle, e.g. the last-seen threshold has been exceeded, but the connection with the peer has not been broken yet.
        /// </summary>
        Idle = 0x00,

        /// <summary>
        /// Indicates the peer is operating normally.
        /// </summary>
        Active = 0x01,

        /// <summary>
        /// Indicates the peer has shut down or been exited.
        /// </summary>
        Dead = 0x02,

        /// <summary>
        /// Indicates that the peer's status is unknown, possibly because it has not fully connected yet.
        /// </summary>
        Pending = 0x03
    }
}
