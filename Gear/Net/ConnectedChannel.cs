/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2014 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
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

        protected override Stream GetMessageDestinationStream()
        {
            return this.ns;
        }
    }
}
