using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    /// <summary>
    /// Indicates the state of a <see cref="Channel"/>.
    /// </summary>
    public enum ChannelState
    {
        /// <summary>
        /// Indicates that the channel instance has been created but has not been instructed to connect to the remote endpoint.
        /// </summary>
        Initialized,

        /// <summary>
        /// Indicates that the channel send and receive messages between the local and remote endpoints.
        /// </summary>
        Connected,

        /// <summary>
        /// Indicates the channel cannot send or receieve messages with the remote endpoint.
        /// </summary>
        Disconnected
    }
}
