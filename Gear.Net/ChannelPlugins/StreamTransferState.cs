using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.ChannelPlugins
{
    /// <summary>
    /// Implements a state tracking structure for pending, in-progress, and completed stream transfers.
    /// </summary>
    [ProtoContract]
    public class StreamTransferState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StreamTransferState"/> class.
        /// </summary>
        public StreamTransferState()
        {
            this.TransferId = Guid.NewGuid().GetHashCode() << 16 | Guid.NewGuid().GetHashCode() >> 16;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamTransferState"/> class.
        /// </summary>
        /// <param name="finfo"></param>
        public StreamTransferState(FileInfo finfo)
            : this()
        {
            this.Length = finfo.Length;
            this.Name = finfo.Name;

            this.LocalPath = finfo.FullName;
        }

        /// <summary>
        /// Gets or sets the numeric id of the transfer.
        /// </summary>
        [ProtoMember(1)]
        public int TransferId { get; set; }

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

        [ProtoMember(5)]
        public Dictionary<string, string> ExtendedAttributes { get; set; }

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

        [ProtoIgnore]
        public string LocalPath { get; set; }

        [ProtoIgnore]
        public Stream LocalStream { get; set; }

        [ProtoIgnore]
        public Socket DataConnection { get; set; }

        [ProtoIgnore]
        public DateTime TransferInitiated { get; set; }

        [ProtoIgnore]
        public DateTime? TransferStarted { get; set; }

        [ProtoIgnore]
        public DateTime? TransferCompleted { get; set; }
    }
}
