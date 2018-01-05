using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                //this.workAvailable.Set();
            }
        }

        public bool IsIdle
        {
            get
            {
                return this.TransferState == null;
            }
        }

        #endregion
        #region Methods

        internal void Start()
        {
            this.workAvailable.Set();
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
            while (this.workAvailable.WaitOne())
            {
                // Check if the worker needs to be shut down:
                if (this.isPendingDestruction)
                {
                    return;
                }

                // Work on the transfer
                if (this.TransferState != null)
                {
                    this.TransferState.TransferStartedAt = DateTime.Now;

                    if (this.TransferState.LocalDirection == TransferDirection.Outgoing)
                    {
                        this.WorkTransferStateOutbound();
                    }
                    else if (this.TransferState.LocalDirection == TransferDirection.Incoming)
                    {
                        this.WorkTransferStateInbound();
                    }

                    this.TransferState.TransferCompletedAt = DateTime.Now;

                }

                this.workAvailable.Reset();
            }
        }

        private void WorkTransferStateInbound()
        {

        }

        private void WorkTransferStateOutbound()
        {
            var sm = this.TransferState.LocalStream;

            //var netStream = new System.Net.Sockets.NetworkStream(this.TransferState.DataConnection);

            //sm.CopyTo(netStream);
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
