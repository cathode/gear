using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    /// <summary>
    /// Represents event argument data for the <see cref="Gear.Net.MessageSerializationHelper.MessageTypeDiscovered"/> event.
    /// </summary>
    public sealed class MessageTypeDiscoveredEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the dispatch id of the discovered message type.
        /// </summary>
        public int DispatchId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.Type"/> of the discovered message type.
        /// </summary>
        public Type DiscoveredType { get; set; }
    }
}
