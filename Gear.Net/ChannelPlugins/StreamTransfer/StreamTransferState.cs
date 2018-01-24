using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GSCore;
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

        private int transferId;
        private long length;
        private int crc32;
        private string name;
        private Dictionary<string, string> extendedAttributes;
        private long sentBytes;
        private long receivedBytes;
        private string localPath;
        private Stream localStream;
        private ushort? dataPort;
        private Socket dataConnection;
        private DateTime transferInitiatedAt;
        private DateTime? transferStartedAt;
        private DateTime? transferCompletedAt;
        private TransferDirection localDirection;
        private TransferProgressHint progressHint = TransferProgressHint.Queued;
        private StreamTransferPlugin parent;
        private StreamTransferProgressWorker worker;
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
        public int TransferId
        {
            get
            {
                return this.transferId;
            }

            set
            {
                this.transferId = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the total length of the stream to transfer (in bytes).
        /// </summary>
        [ProtoMember(2)]
        public long Length
        {
            get
            {
                return this.length;
            }

            set
            {
                this.length = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the CRC32 hash-code of the source stream's contents.
        /// </summary>
        [ProtoMember(3)]
        public int CRC32
        {
            get
            {
                return this.crc32;
            }

            set
            {
                this.crc32 = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a name associated with the stream.
        /// </summary>
        [ProtoMember(4)]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets a collection used to hold metadata key/value pairs associated with the transfer state.
        /// </summary>
        [ProtoMember(5)]
        public Dictionary<string, string> ExtendedAttributes
        {
            get
            {
                return this.extendedAttributes;
            }

            set
            {
                this.extendedAttributes = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating how many bytes have been sent.
        /// </summary>
        [ProtoIgnore]
        public long SentBytes
        {
            get
            {
                return this.sentBytes;
            }

            set
            {
                this.sentBytes = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating how many bytes have been received.
        /// </summary>
        [ProtoIgnore]
        public long ReceivedBytes
        {
            get
            {
                return this.receivedBytes;
            }

            set
            {
                this.receivedBytes = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a string that describes the local path of the file stream being transfered (if applicable).
        /// </summary>
        [ProtoIgnore]
        public string LocalPath
        {
            get
            {
                return this.localPath;
            }

            set
            {
                this.localPath = value;
                this.OnPropertyChanged();
            }
        }

        [ProtoIgnore]
        public Stream LocalStream
        {
            get
            {
                return this.localStream;
            }

            set
            {
                this.localStream = value;
                this.OnPropertyChanged();
            }
        }

        [ProtoMember(6)]
        public ushort? DataPort
        {
            get
            {
                return this.dataPort;
            }

            set
            {
                this.dataPort = value;
                this.OnPropertyChanged();
            }
        }

        [ProtoIgnore]
        public Socket DataConnection
        {
            get
            {
                return this.dataConnection;
            }

            set
            {
                this.dataConnection = value;
                this.OnPropertyChanged();
            }
        }

        [ProtoIgnore]
        public DateTime TransferInitiatedAt
        {
            get
            {
                return this.transferInitiatedAt;
            }

            set
            {
                this.transferInitiatedAt = value;
                this.OnPropertyChanged();
            }
        }

        [ProtoIgnore]
        public DateTime? TransferStartedAt
        {
            get
            {
                return this.transferStartedAt;
            }

            set
            {
                this.transferStartedAt = value;
                this.OnPropertyChanged();
            }
        }

        [ProtoIgnore]
        public DateTime? TransferCompletedAt
        {
            get
            {
                return this.transferCompletedAt;
            }

            set
            {
                this.transferCompletedAt = value;
                this.OnPropertyChanged();
            }
        }

        [ProtoIgnore]
        public TransferDirection LocalDirection
        {
            get
            {
                return this.localDirection;
            }

            set
            {
                this.localDirection = value;
                this.OnPropertyChanged();
            }
        }

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

                this.OnPropertyChanged();

                if (value == TransferProgressHint.Completed)
                {
                    this.OnCompleted();
                }
                else if (value == TransferProgressHint.Failed)
                {
                    this.OnAborted();
                }
            }
        }

        [ProtoIgnore]
        public StreamTransferPlugin Parent
        {
            get
            {
                return this.parent;
            }

            set
            {
                this.parent = value;
                this.OnPropertyChanged();
            }
        }

        [ProtoIgnore]
        public StreamTransferProgressWorker Worker
        {
            get
            {
                return this.worker;
            }

            set
            {
                this.worker = value;
                this.OnPropertyChanged();
            }
        }

        #region Methods

        /// <summary>
        /// Raises the <see cref="StreamTransferState.Completed"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event (unused).</param>
        protected virtual void OnCompleted(EventArgs e = null)
        {
            this.Completed?.Invoke(this, e ?? EventArgs.Empty);
        }

        protected virtual void OnAborted(EventArgs e = null)
        {
            Log.Write(LogMessageGroup.Informational, "Transfer {0} failed / aborted.", this.TransferId);

            this.Aborted?.Invoke(this, e ?? EventArgs.Empty);
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "unknown")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
