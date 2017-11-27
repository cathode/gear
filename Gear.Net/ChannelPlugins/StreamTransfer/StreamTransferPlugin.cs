using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.ChannelPlugins.FileTransfer
{
    /// <summary>
    /// Implements a <see cref="ChannelPlugin"/> that handles file and data stream transfer tasks for the attached <see cref="Channel"/>.
    /// </summary>
    public class StreamTransferPlugin : ChannelPlugin
    {
        #region Fields
        private readonly ObservableCollection<StreamTransferState> transfers;
        private readonly ReadOnlyObservableCollection<StreamTransferState> transfersRO;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamTransferPlugin"/> class.
        /// </summary>
        public StreamTransferPlugin()
        {
            this.transfers = new ObservableCollection<StreamTransferState>();

            // Create read-only wrapper of transfers
            this.transfersRO = new ReadOnlyObservableCollection<StreamTransferState>(this.transfers);
        }

        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets a maximum number of global stream transfers across all instances.
        /// </summary>
        public static int MaxGlobalSimultaneousTransfers { get; set; }

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
        public ReadOnlyObservableCollection<StreamTransferState> FileTransfers
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
            channel.RegisterHandler<TransferStreamMessage>(this.Handle_TransferFileMessage, this);
        }

        public override void Detach(Channel channel)
        {
            channel.UnregisterHandler(this);
        }

        protected virtual void Handle_FileDataPortReadyMessage(MessageEventArgs e, FileDataPortReadyMessage message)
        {

        }

        protected virtual void Handle_TransferFileMessage(MessageEventArgs e, TransferStreamMessage message)
        {

        }
        #endregion
    }
}
