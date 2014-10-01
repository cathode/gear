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
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Sockets;

namespace Gear.Net
{
    /// <summary>
    /// Listens for incoming connections and handles the mundane socket work.
    /// </summary>
    public class ConnectionListener
    {
        private Socket listener;

        public ConnectionListener(ushort port)
        {
            Contract.Requires(port > 1024);

            this.ListenPort = port;

            this.Allowed = new NetworkList();
            this.Denied = new NetworkList();
        }

        /// <summary>
        /// Gets or sets a collection of network addresses or ranges that are explicitly allowed to connect to this end point.
        /// </summary>
        public NetworkList Allowed { get; set; }

        /// <summary>
        /// Gets or sets a collection of network addresses or ranges that are explicitly denied from connecting to this end point.
        /// </summary>
        public NetworkList Denied { get; set; }

        public ushort ListenPort { get; private set; }

        public event EventHandler ChannelConnected;

        public void Start()
        {
            if (this.listener != null)
                return;

            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.listener.Bind(new IPEndPoint(IPAddress.Any, this.ListenPort));
            this.listener.Listen(32);


            while (true)
            {
                try
                {
                    var sock = this.listener.Accept();

                    var channel = new Channel(sock);

                    channel.SetUp();

                }
                catch (TimeoutException ex)
                {
                    // TODO: Add logging.
                    break;
                }
            }
        }


        public object StartInBackground()
        {
            Task.Run(() => this.Start());

            //this.Start();
            return null;
        }

        public void Stop()
        {

        }

        public void OnConnectionEstablished(EventArgs e)
        {

        }
    }
}
