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
        #region Properties
        /// <summary>
        /// Gets a <see cref="TimeSpan"/> indicating the latency (delay) between the local node and the remote node.
        /// </summary>
        public override TimeSpan Latency
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Sends all unsent updates to the remote node.
        /// </summary>
        public override void Flush()
        {
            throw new NotImplementedException();
        }
        #endregion

        public override int SendCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int ReceiveCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void Enqueue(Update update)
        {
            throw new NotImplementedException();
        }

        public override Update Dequeue()
        {
            throw new NotImplementedException();
        }

        public override NodeInterfaceStatus Status
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
