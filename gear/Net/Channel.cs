using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics.Contracts;

namespace Gear.Net
{
    /// <summary>
    /// Represents a communication channel between the local end point and a remote end point.
    /// </summary>
    public class Channel
    {
        private readonly Socket socket;
        private NetworkStream ns;

        private MessageQueue outgoing;
        private MessageQueue incoming;

        internal Channel(Socket socket)
        {
            Contract.Requires(socket != null);

            this.socket = socket;
            this.ns = new NetworkStream(socket, false);
        }

        public Guid RemoteEndPointId { get; set; }

        public EndPointKind RemoteEndPointKind { get; set; }

        public void SetUp()
        {

            throw new NotImplementedException();
        }

        /// <summary>
        /// All messages published by the specified publisher will be forwarded
        /// to the remote endpoint via this channel.
        /// </summary>
        /// <param name="publisher"></param>
        public void SubscribeToPublisher(IMessagePublisher publisher)
        {
            Contract.Requires(publisher != null);

            publisher.MessageAvailable += publisher_MessageAvailable;
            publisher.ShuttingDown += publisher_ShuttingDown;
        }

        void publisher_ShuttingDown(object sender, EventArgs e)
        {
            var publisher = (IMessagePublisher)sender;

            // Remove event bindings
            publisher.ShuttingDown -= publisher_ShuttingDown;
            publisher.MessageAvailable -= publisher_MessageAvailable;
        }

        void publisher_MessageAvailable(object sender, MessageEventArgs e)
        {
            Contract.Requires(e != null);

            this.QueueMessage(e.Message);
        }



        /// <summary>
        /// Queues an individual message on the channel, to be sent to the remote endpoint.
        /// </summary>
        /// <param name="message"></param>
        public void QueueMessage(object message)
        {
            this.QueueMessageThreadSafe(message);
        }

        private void QueueMessageThreadSafe(object message)
        {

        }

        private void FlushMessages()
        {

        }
    }
}
