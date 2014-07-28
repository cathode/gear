using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics.Contracts;
using System.Threading;

namespace Gear.Net
{
    /// <summary>
    /// Represents a communication channel between the local end point and a remote end point.
    /// </summary>
    public class Channel
    {
        private readonly Socket socket;
        private NetworkStream ns;

        private Queue<IMessage> outgoingInactive;
        private Queue<IMessage> outgoingActive;

        private Queue<IMessage> incomingInactive;
        private Queue<IMessage> incomingActive;


        internal Channel(Socket socket)
        {
            Contract.Requires(socket != null);

            this.socket = socket;
            this.ns = new NetworkStream(socket, false);

            this.outgoingActive = new Queue<IMessage>();
            this.outgoingInactive = new Queue<IMessage>();
            this.incomingActive = new Queue<IMessage>();
            this.incomingInactive = new Queue<IMessage>();
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

            if (publisher != null)
            {
                // Remove event bindings
                publisher.ShuttingDown -= publisher_ShuttingDown;
                publisher.MessageAvailable -= publisher_MessageAvailable;
            }
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
        public void QueueMessage(IMessage message)
        {
            this.QueueMessageThreadSafe(message);
        }

        private void QueueMessageThreadSafe(IMessage message)
        {
            Contract.Requires(message != null);

            // TODO: ensure that locking against the queue itself is correct.
            lock (this.outgoingInactive)
            {
                this.outgoingInactive.Enqueue(message);
            }
        }

        private void FlushMessages()
        {

        }

        private void FlipBuffersIncoming()
        {
            // Swap the buffers used for incoming messages.

            // Always lock inactive->active
            lock (this.incomingInactive)
            {
                lock (this.incomingInactive)
                {
                    if (this.incomingActive.Count > 0)
                        return;

                    var q = this.incomingInactive;
                    this.incomingInactive = this.incomingActive;
                    this.incomingActive = q;
                }
            }
        }

        private void FlipBuffersOutgoing()
        {
            // Swap the buffers used for outgoing messages.
            lock (this.outgoingInactive)
            {
                lock (this.outgoingActive)
                {
                    if (this.outgoingActive.Count > 0)
                        return;

                    // TODO: Evaluate the need for locking on the individual buffers.
                    var q = this.outgoingInactive;
                    this.outgoingInactive = this.outgoingActive;
                    this.outgoingActive = q;
                }
            }
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.outgoingActive != null);
            Contract.Invariant(this.outgoingInactive != null);
            Contract.Invariant(this.incomingActive != null);
            Contract.Invariant(this.incomingInactive != null);

        }
    }
}
