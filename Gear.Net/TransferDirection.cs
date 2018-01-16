using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    /// <summary>
    /// Represents direction of a stream transfer.
    /// </summary>
    public enum TransferDirection
    {
        /// <summary>
        /// No transfer or unknown direction.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates an outgoing (sending) transfer.
        /// </summary>
        Outgoing = 1,

        /// <summary>
        /// Indicates an incoming (receiving) transfer.
        /// </summary>
        Incoming = 2,

        /// <summary>
        /// Indicates both types of transfers.
        /// </summary>
        Bidirectional = Outgoing | Incoming
    }
}
