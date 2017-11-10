using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    public class PeerEventArgs : EventArgs
    {
        public PeerMetadata Peer { get; set; }

        public Channel PeerConnection { get; set; }
    }
}
