using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics.Contracts;

namespace Gear.Net
{
    /// <summary>
    /// Represents a communication channel that 
    /// </summary>
    public class ConnectedChannel : Channel
    {
        private readonly Socket socket;
        private NetworkStream ns;

        private bool hasSentGreeting;

        private bool hasReceivedGreeting;

        public ConnectedChannel(Socket socket)
        {
            Contract.Requires(socket != null);

            this.socket = socket;
            this.ns = new NetworkStream(socket, false);
        }

        public Guid RemoteEndPointId { get; set; }

        public EndPointKind RemoteEndPointKind { get; set; }

        public static ConnectedChannel ConnectTo(IPEndPoint remoteEP)
        {
            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sock.Connect(remoteEP);
            }
            catch (Exception ex)
            {
                throw ex;

            }

            var channel = new ConnectedChannel(sock);
            //channel.SetUp();
            var msg = new Gear.Net.Messages.EndPointGreetingMessage();
            msg.EndPointId = Guid.NewGuid();
            msg.Kind = EndPointKind.Client;

            channel.QueueMessage(msg);

            return channel;
        }

        protected override void FlushMessages()
        {

            var ws = this.ns;

            var ser = ProtoBuf.Serializer.CreateFormatter<MessageContainer>();

            lock (this.outgoingActive)
            {
                if (this.outgoingActive.Count == 0)
                    this.SwapBuffersOutgoing();

                if (this.outgoingActive.Count > 0)
                {
                    var msg = this.outgoingActive.Dequeue();
                    ser.Serialize(ws, new MessageContainer(msg));
                }
            }

            // Receive data for pending messages
            lock (this.incomingInactive)
            {
                try
                {
                    while (ws.DataAvailable)
                    {
                        var msg = ser.Deserialize(ws) as MessageContainer;
                        this.incomingInactive.Enqueue(msg.Contents);
                    }
                }
                catch
                {

                }
            }

        }
    }
}
