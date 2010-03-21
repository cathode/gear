using System;
using System.Collections.Generic;
using System.Text;

namespace Intralock
{
    /// <summary>
    /// Represents the status of a <see cref="NodeInterface"/>.
    /// </summary>
    public enum NodeInterfaceStatus
    {
        /// <summary>
        /// Indicates the <see cref="NodeInterface"/> is able to communicate with the remote node.
        /// </summary>
        Connected,

        /// <summary>
        /// Indicates the <see cref="NodeInterface"/> is not able to communicate with the remote node.
        /// </summary>
        Disconnected,
    }
}
