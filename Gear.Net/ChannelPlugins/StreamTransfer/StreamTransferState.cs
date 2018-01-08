using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.ChannelPlugins.StreamTransfer
{
    /// <summary>
    /// Implements a state tracking structure for pending, in-progress, and completed stream transfers.
    /// </summary>
    [ProtoContract]
    public class StreamTransferState : INotifyPropertyChanged
    {
        #region Fields
        internal AutoResetEvent ProgressStep = new AutoResetEvent(false);
        private TransferProgressHint progressHint = TransferProgressHint.Queued;
        #endregion
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
            Contract.Requires<ArgumentNullException>(finfo != null);

            this.Length = finfo.Length;
            this.Name = finfo.Name;

            this.LocalPath = finfo.FullName;
        }

        /// <summary>
        /// Raised when the transfer completes.
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// Raised if the transfer is aborted (fails or is cancelled);
        /// </summary>
        public event EventHandler Aborted;

        /// <summary>
        /// Raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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

        [ProtoMember(6)]
        public ushort? DataPort { get; set; }

        [ProtoIgnore]
        public Socket DataConnection { get; set; }

        [ProtoIgnore]
        public DateTime TransferInitiatedAt { get; set; }

        [ProtoIgnore]
        public DateTime? TransferStartedAt { get; set; }

        [ProtoIgnore]
        public DateTime? TransferCompletedAt { get; set; }

        [ProtoIgnore]
        public TransferDirection LocalDirection { get; set; }

        [ProtoIgnore]
        public TransferProgressHint ProgressHint
        {
            get
            {
                return this.progressHint;
            }

            set
            {
                this.progressHint = value;

                if (value == TransferProgressHint.Completed)
                {
                    this.OnCompleted();
                }
            }
        }

        [ProtoIgnore]
        public StreamTransferPlugin Parent { get; set; }

        [ProtoIgnore]
        public StreamTransferProgressWorker Worker { get; set; }

        /// <summary>
        /// Raises the <see cref="StreamTransferState.Completed"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event (unused).</param>
        protected virtual void OnCompleted(EventArgs e = null)
        {
            this.Completed?.Invoke(this, e ?? EventArgs.Empty);
        }
    }
}
