using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GSCore;

namespace Gear.Net.ChannelPlugins.StreamTransfer
{
    public class StreamTransferProgressWorker
    {
        #region Fields
        private Thread workThread;
        private StreamTransferState transferState;
        private AutoResetEvent workAvailable;
        private bool isPendingDestruction;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamTransferProgressWorker"/> class.
        /// </summary>
        public StreamTransferProgressWorker()
        {
            this.workAvailable = new AutoResetEvent(false);
            this.workThread = new Thread(new ThreadStart(this.BackgroundMainLoop));
            this.workThread.Start();
        }

        #endregion
        #region Properties
        public StreamTransferState TransferState
        {
            get
            {
                return this.transferState;
            }

            internal set
            {
                this.transferState = value;
            }
        }

        public bool IsIdle
        {
            get
            {
                return this.TransferState == null;
            }
        }

        public bool IsAlive
        {
            get
            {
                if (this.workThread != null && this.workThread.IsAlive)
                {
                    return true;
                }

                return false;
            }
        }

        #endregion
        #region Methods

        internal void Start()
        {
            this.workAvailable.Set();
        }

        internal void Recycle()
        {
            if (this.TransferState != null)
            {
                this.TransferState.Worker = null;
            }

            this.TransferState = null;
        }

        /// <summary>
        /// Flags the worker as destroyed so that when it's done processing it will shut down gracefully.
        /// </summary>
        internal void FlagDestroyed()
        {
            this.isPendingDestruction = true;
            this.workAvailable.Set();
        }

        private void BackgroundMainLoop()
        {
            while (true)
            {
                this.workAvailable.WaitOne();
                this.workAvailable.Reset();

                // Check if the worker needs to be shut down:
                if (this.isPendingDestruction)
                {
                    Log.Write("Stream Transfer Progress Worker on thread id {0} terminating by application request.", this.workThread.ManagedThreadId);
                    return;
                }

                // Work on the transfer
                if (this.TransferState != null)
                {
                    Log.Write(LogMessageGroup.Informational, "StreamTransferProgressWorker on thread id {0} initiating transfer for {1}", this.workThread.ManagedThreadId, this.TransferState.TransferId);
                    this.TransferState.ProgressHint = TransferProgressHint.Initiated;
                    this.TransferState.TransferInitiatedAt = DateTime.Now;

                    if (this.TransferState.LocalDirection == TransferDirection.Outgoing)
                    {
                        this.WorkTransferStateOutbound();
                    }
                    else if (this.TransferState.LocalDirection == TransferDirection.Incoming)
                    {
                        this.WorkTransferStateInbound();
                    }

                    this.TransferState.TransferCompletedAt = DateTime.Now;
                    Log.Write("Transfer {0} completed.", this.TransferState.TransferId);
                    this.TransferState.ProgressHint = TransferProgressHint.Completed;
                }
            }
        }

        private void WorkTransferStateInbound()
        {
            // Inbound transfer - remote peer is trying to transfer data to us.

            // Set up transfer state
            if (this.TransferState.DataPort == null)
            {
                if (!this.TransferState.Parent.CanHostActiveTransfers)
                {
                    Log.Write(LogMessageGroup.Severe, "Client requested data port but local end cannot host active transfers!");

                    // TODO: send error message back to client.
                    throw new NotImplementedException();
                }
                else
                {
                    // Create a listener for incoming connection from sending peer.
                    var listener = StreamTransferPlugin.GetNextDataPortListener();

                    // Accept connection on a background thread.
                    var accTask = Task.Run(() =>
                    {
                        // TODO: handle timeout
                        this.TransferState.DataConnection = listener.Accept();
                        listener.Close();
                        listener.Dispose();
                    });

                    var msg = new StreamDataPortReadyMessage();
                    msg.DataPort = (ushort)((IPEndPoint)listener.LocalEndPoint).Port;
                    msg.TransferId = this.TransferState.TransferId;

                    Log.Write(LogMessageGroup.Informational, "Notifying sender of data port listener bound to TCP port {0} for transfer id {1}", msg.DataPort, msg.TransferId);
                    this.TransferState.Parent.AttachedChannel.Send(msg);

                    accTask.Wait();
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            this.TransferState.ProgressHint = TransferProgressHint.Receiving;

            // Receive data from remote
            long remain = this.TransferState.Length;
            long recvTotal = 0;
            this.TransferState.LocalStream = new MemoryStream();

            var bufferSize = 8192;
            var buffer = new byte[bufferSize];
            var dc = this.TransferState.DataConnection;

            while (remain > 0)
            {
                // Calculate buffer size of next receive
                int expect = (int)Math.Min(8192, remain);

                var recvd = dc.Receive(buffer, expect, System.Net.Sockets.SocketFlags.None);

                this.TransferState.LocalStream.Write(buffer, 0, recvd);

                remain -= recvd;
                recvTotal += recvd;
            }

            Log.Write(LogMessageGroup.Debug, "Transfer {0} - received {1} bytes.", this.TransferState.TransferId, recvTotal);

            // Cleanup
            dc.Close();
        }

        private void WorkTransferStateOutbound()
        {
            // Outbound transfer - local peer has the stream data and needs to transfer it to remote peer.
            var sm = this.TransferState.LocalStream;

            // Build metadata to send to remote
            var msg = new TransferStreamMessage();
            msg.TransferState = this.TransferState;

            if (!this.TransferState.Parent.CanHostActiveTransfers)
            {
                this.TransferState.ProgressHint = TransferProgressHint.ConnectingToDataPort;

                // The remote peer needs to listen for a data connection from the local peer.
                msg.RequestDataPort = true;

                this.TransferState.Parent.AttachedChannel.Send(msg);
            }
            else
            {
                this.transferState.ProgressHint = TransferProgressHint.WaitingForDataConnection;
                var listener = StreamTransferPlugin.GetNextDataPortListener();

                var t = Task.Run(() =>
                {
                    this.TransferState.DataConnection = listener.Accept();
                    this.TransferState.ProgressStep.Set();
                });
                msg.RequestDataPort = false;
                msg.DataPort = (ushort)((IPEndPoint)listener.LocalEndPoint).Port;

                this.TransferState.Parent.AttachedChannel.Send(msg);

                t.Wait();
            }

            // Wait until a data port confirmation is received.
            this.TransferState.ProgressStep.WaitOne();

            this.TransferState.ProgressHint = TransferProgressHint.Sending;

            long remain = this.TransferState.Length;
            long sendTotal = 0;

            var bufferSize = 8192;
            var buffer = new byte[bufferSize];
            var dc = this.TransferState.DataConnection;

            while (remain > 0)
            {
                // Calculate buffer size of next send
                int expect = (int)Math.Min(8192, remain);

                this.TransferState.LocalStream.Read(buffer, 0, expect);

                var sent = dc.Send(buffer, expect, System.Net.Sockets.SocketFlags.None);

                remain -= sent;
                sendTotal += sent;
            }

            Log.Write(LogMessageGroup.Debug, "Transfer {0} -  sent {1} bytes.", this.TransferState.TransferId, sendTotal);
        }

        [ContractInvariantMethod]
        private void ContractInvariants()
        {
            Contract.Invariant(this.workAvailable != null);
            Contract.Invariant(this.workThread != null);
        }
        #endregion
    }
}
