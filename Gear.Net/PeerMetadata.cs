using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Gear.Net
{
    public class PeerMetadata
    {
        public Guid PeerId { get; set; }

        public Version SoftwareVersion { get; set; }


    }
}
