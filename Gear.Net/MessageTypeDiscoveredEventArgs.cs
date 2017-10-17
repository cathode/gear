using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    public sealed class MessageTypeDiscoveredEventArgs : EventArgs
    {
        public int DispatchId { get; set; }

        public Type DiscoveredType { get; set; }
    }
}
