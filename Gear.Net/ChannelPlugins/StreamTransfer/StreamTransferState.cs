using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.ChannelPlugins.StreamTransfer
{
    /// <summary>
    /// Implements a state tracking structure for pending, in-progress, and completed stream transfers.
    /// </summary>
    [ProtoContract]
    public class StreamTransferState
    {
        /// <summary>
        /// Gets or sets the numeric id of the transfer.
        /// </summary>
        [ProtoMember(1)]
        public long TransferId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the total length of the stream to transfer (in bytes).
        /// </summary>
        [ProtoMember(2)]
        public long Length { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the CRC32 hash-code of the source stream's contents.
        /// </summary>
        [ProtoMember(3)]
        public int CRC32 { get; set; }

        /// <summary>
        /// Gets or sets a name associated with the stream.
        /// </summary>
        [ProtoMember(4)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating how many bytes have been sent.
        /// </summary>
        [ProtoIgnore]
        public long SentBytes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating how many bytes have been received.
        /// </summary>
        [ProtoIgnore]
        public long ReceivedBytes { get; set; }

        [ProtoMember(5)]
        public Dictionary<string, string> ExtendedAttributes { get; set; }
    }
}
