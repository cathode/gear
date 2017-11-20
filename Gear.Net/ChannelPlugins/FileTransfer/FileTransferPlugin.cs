using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.ChannelPlugins.FileTransfer
{
    /// <summary>
    /// Implements a <see cref="ChannelPlugin"/> that handles file transfer tasks for the attached <see cref="Channel"/>.
    /// </summary>
    public class FileTransferPlugin : ChannelPlugin
    {
        #region Fields
        private readonly ObservableCollection<FileTransferState> transfers;
        private readonly ReadOnlyObservableCollection<FileTransferState> transfersRO;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTransferPlugin"/> class.
        /// </summary>
        public FileTransferPlugin()
        {
            this.transfers = new ObservableCollection<FileTransferState>();

            // Create read-only wrapper of transfers
            this.transfersRO = new ReadOnlyObservableCollection<FileTransferState>(this.transfers);
        }

        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the system this <see cref="FileTransferPlugin"/> is running on is capable of
        /// hosting active transfers.
        /// </summary>
        /// <remarks>
        /// If this system is behind a NAT device, or a firewall, it will likely not support hosting active transfers.
        /// </remarks>
        public bool CanHostActiveTransfers { get; set; }

        /// <summary>
        /// Gets an observable collection of all file transfers associated with this <see cref="FileTransferPlugin"/> in it's lifetime.
        /// </summary>
        public ReadOnlyObservableCollection<FileTransferState> FileTransfers
        {
            get
            {
                return this.transfersRO;
            }
        }

        #endregion
        #region Methods
        public override void Attach(Channel channel)
        {
            channel.RegisterHandler<FileDataPortReadyMessage>(this.Handle_FileDataPortReadyMessage, this);
            channel.RegisterHandler<TransferFileMessage>(this.Handle_TransferFileMessage, this);
        }

        public override void Detach(Channel channel)
        {
            channel.UnregisterHandler(this);
        }

        protected virtual void Handle_FileDataPortReadyMessage(MessageEventArgs e, FileDataPortReadyMessage message)
        {

        }

        protected virtual void Handle_TransferFileMessage(MessageEventArgs e, TransferFileMessage message)
        {

        }
        #endregion
    }
}
