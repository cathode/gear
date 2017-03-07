using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    /// <summary>
    /// Represents event data for the <see cref="ConnectedChannel.Disconnected"/> event.
    /// </summary>
    public class ChannelDisconnectedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelDisconnectedEventArgs"/> class.
        /// </summary>
        public ChannelDisconnectedEventArgs()
        {
            this.ReconnectCount = 0;
            this.ReconnectInterval = TimeSpan.FromSeconds(0);
        }

        /// <summary>
        /// Gets or sets a value that determines how many times a reconnection attempt should be made before giving up.
        /// </summary>
        /// <remarks>
        /// A value of -1 indicates that reconnection will be attempted indefinitely.
        /// </remarks>
        public int ReconnectCount { get; set; }

        /// <summary>
        /// Gets or sets a value that determines the period between each reconnection attempt.
        /// </summary>
        /// <remarks>
        /// The interval is used betwen the initial disconnection and the first reconnect attempt in addition to each subsequent reconnect attempt.
        /// </remarks>
        public TimeSpan ReconnectInterval { get; set; }
    }
}
