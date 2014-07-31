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

        private bool hasSentGreeting;

        private bool hasReceivedGreeting;

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


        public static Channel ConnectTo(IPEndPoint remoteEP)
        {
            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);

            try
            {
                sock.Connect(remoteEP);
            }
            catch (Exception ex)
            {
                throw ex;

            }

            var channel = new Channel(sock);
            //channel.SetUp();
            var msg = new Gear.Net.Messages.EndPointGreetingMessage();
            msg.EndPointId = Guid.NewGuid();
            msg.Kind = EndPointKind.Client;

            channel.QueueMessage(msg);

            return channel;
        }

        public void SetUp()
        {

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
            Contract.Requires(message != null);

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

            this.FlushMessages();
        }

        private void FlushMessages()
        {
            var ws = this.ns;

            var ser = ProtoBuf.Serializer.CreateFormatter<MessageContainer>();

            lock (this.outgoingActive)
            {
                var msg = this.outgoingActive.Dequeue();
                ser.Serialize(ws, new MessageContainer(msg));
            }
        }

        private void SwapBuffersIncoming()
        {
            // Swap the buffers used for incoming messages.
            try
            {
                // Attempt to grab the active queue. If this fails it probably means that
                // it has messages and they're being processed still.
                if (Monitor.TryEnter(this.incomingActive, 1))
                {
                    // Always lock active first, then inactive.
                    lock (this.incomingInactive)
                    {
                        // Only swap if the active buffer is empty (all the messages in it were processed)
                        // Otherwise, the queue would become out-of-order very quickly.
                        if (this.incomingActive.Count > 0)
                            return;

                        var q = this.incomingInactive;
                        this.incomingInactive = this.incomingActive;
                        this.incomingActive = q;
                    }
                }
            }
            catch (Exception ex)
            {
                if (Monitor.IsEntered(this.incomingActive))
                    Monitor.Exit(this.incomingActive);

                throw ex;
            }
        }

        private void SwapBuffersOutgoing()
        {
            // Swap the buffers used for outgoing messages.
            try
            {
                if (Monitor.TryEnter(this.outgoingActive, 1))
                {
                    lock (this.outgoingInactive)
                    {
                        if (outgoingActive.Count > 0)
                            return;

                        var q = this.outgoingInactive;
                        this.outgoingInactive = this.outgoingActive;
                        this.outgoingActive = q;
                    }
                }
            }
            catch (Exception ex)
            {
                if (Monitor.IsEntered(this.outgoingActive))
                    Monitor.Exit(this.outgoingActive);

                throw ex;
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
