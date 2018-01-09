using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.ChannelPlugins.StreamTransfer
{
    /// <summary>
    /// Represents event data for stream transfer state events.
    /// </summary>
    public class StreamTransferEventArgs : EventArgs
    {
        private StreamTransferState transferState;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamTransferEventArgs"/> class.
        /// </summary>
        /// <param name="state">The <see cref="StreamTransferState"/> data for the event.</param>
        public StreamTransferEventArgs(StreamTransferState state)
        {
            this.transferState = state;
        }

        /// <summary>
        /// Gets the <see cref="StreamTransferState"/> data for the event.
        /// </summary>
        public StreamTransferState TransferState
        {
            get
            {
                return this.transferState;
            }
        }
    }
}
