using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gear.Net.ChannelPlugins
{
    public class StreamTransferProgressWorker
    {
        #region Fields
        private Stream source;
        private Stream target;
        private Thread workThread;
        private StreamTransferState workState;
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
        public StreamTransferState WorkState
        {
            get
            {
                return this.workState;
            }

            internal set
            {
                this.workState = value;

                if (value != null)
                {
                    this.workAvailable.Set();
                }
            }
        }

        public bool IsIdle
        {
            get
            {
                return false;
            }
        }

        #endregion
        #region Methods

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
                    this.workThread.Abort();
                }

                // Work on the transfer

            }
        }

        #endregion
    }
}
