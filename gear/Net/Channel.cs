using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Gear.Net
{
    /// <summary>
    /// Represents a communication channel between the local end point and a remote end point.
    /// </summary>
    public class Channel
    {
        private readonly Socket socket;
        private NetworkStream ns;

        internal Channel(Socket socket)
        {
            this.socket = socket;
            this.ns = new NetworkStream(socket, false);
        }

        public Guid RemoteEndPointId { get; set; }

        public EndPointKind RemoteEndPointKind { get; set; }
    }
}
