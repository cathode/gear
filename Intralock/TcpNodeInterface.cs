using System;
using System.Collections.Generic;
using System.Text;

namespace Intralock
{
    /// <summary>
    /// A <see cref="NodeInterface"/> implementation that uses TCP/IP sockets to communicate with the remote node.
    /// </summary>
    public sealed class TcpNodeInterface : NodeInterface
    {
        public override TimeSpan Latency
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }
    }
}
