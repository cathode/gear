using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.ChannelPlugins.StreamTransfer
{
    /// <summary>
    /// Implements a <see cref="ChannelPlugin"/> that handles file and data stream transfer tasks for the attached <see cref="Channel"/>.
    /// </summary>
    public class StreamTransferPlugin : ChannelPlugin
    {
        #region Fields
        private readonly ObservableCollection<StreamTransferState> transfers;
        private readonly ReadOnlyObservableCollection<StreamTransferState> transfersRO;

        private Channel attachedChannel;

        #endregion
        #region Constructors
        static StreamTransferPlugin()
        {
            MaxGlobalSimultaneousTransfers = 900;
            TransferPortPoolStart = 55100;
        }

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

        public static ushort TransferPortPoolStart { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the system this <see cref="StreamTransferPlugin"/> is running on is capable of
        /// hosting active transfers.
        /// </summary>
        /// <remarks>
        /// If this system is behind a NAT device, or a firewall, it will likely not support hosting active transfers.
        /// </remarks>
        public bool CanHostActiveTransfers { get; set; }

        /// <summary>
        /// Gets an observable collection of all file transfers associated with this <see cref="StreamTransferPlugin"/> in it's lifetime.
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
            channel.RegisterHandler<StreamDataPortReadyMessage>(this.Handle_StreamDataPortReadyMessage, this);
            channel.RegisterHandler<TransferStreamMessage>(this.Handle_TransferStreamMessage, this);

            this.attachedChannel = channel;
        }

        public override void Detach(Channel channel)
        {
            channel.UnregisterHandler(this);

            this.attachedChannel = null;
        }

        /// <summary>
        /// Initiates a stream transfer of the file at the specified path.
        /// </summary>
        /// <param name="filePath"></param>
        public void SendFile(string filePath)
        {
            Contract.Requires<ArgumentNullException>(filePath != null);

            // Sanity checking:
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("No file found at the specified path.", filePath);
            }

            filePath = Path.GetFullPath(filePath);
            var finfo = new FileInfo(filePath);

            // Build transfer state:
            var state = new StreamTransferState(finfo);

            this.transfers.Add(state);

            var msg = new TransferStreamMessage();
            msg.TransferState = state;

            this.attachedChannel.Send(msg);
        }

        protected virtual void Handle_StreamDataPortReadyMessage(MessageEventArgs e, StreamDataPortReadyMessage message)
        {
            var state = this.transfers.First(t => t.TransferId == message.TransferId);

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(new IPEndPoint(this.attachedChannel.RemoteEndPoint.Address, message.DataPort));

            var buffer = File.ReadAllBytes(state.LocalPath);
            socket.Send(buffer);
        }

        protected virtual void Handle_TransferStreamMessage(MessageEventArgs e, TransferStreamMessage message)
        {
            var state = message.TransferState;

        

            // Two modes - spawn data port and wait, or connect to peer's data port.

            if (message.DataPort.HasValue && message.DataPort > 0)
            {
                // we connect to them
            }
            else
            {
                // spawn data port and send ready message:

                this.transfers.Add(state);

                var listener = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                listener.Bind(new IPEndPoint(IPAddress.Any, 55100));
                listener.Listen(1);
                var msg = new StreamDataPortReadyMessage();
                msg.TransferId = state.TransferId;
                msg.DataPort = 55100;
                this.attachedChannel.Send(msg);

                state.DataConnection = listener.Accept();

                byte[] buffer = new byte[Math.Min(state.Length, 65536)];

                var recvd = state.DataConnection.Receive(buffer);

                var path = Path.GetFullPath(Path.Combine("./transfers", state.Name));

                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }

                File.WriteAllBytes(path, buffer);
            }
        }
        #endregion
    }
}
