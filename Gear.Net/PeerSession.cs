using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    public class PeerSession
    {
        public ConnectedChannel Connection { get; set; }
        public PeerMetadata Metadata { get; set; }
    }
}
