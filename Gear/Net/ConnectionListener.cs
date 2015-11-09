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
            //Contract.Requires(port > 1024);

            this.ListenPort = port;
            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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

        public event EventHandler<ChannelEventArgs> ChannelConnected;

        public void Start()
        {   
            this.listener.Bind(new IPEndPoint(IPAddress.Any, this.ListenPort));
            this.listener.Listen(32);


            while (true)
            {
                try
                {
                    var sock = this.listener.Accept();

                    var channel = new ConnectedChannel(sock);

                    this.OnConnectionEstablished(this, new ChannelEventArgs { Channel = channel });
                }
                catch (TimeoutException ex)
                {
                    // TODO: Add logging.
                    break;
                }
            }
        }


        public void StartInBackground()
        {
            Task.Run(() => this.Start());

        }

        public void Stop()
        {

        }

        protected void OnConnectionEstablished(object sender, ChannelEventArgs e)
        {
            Contract.Requires(e != null);


            if (this.ChannelConnected != null)
                this.ChannelConnected(sender, e);

            e.Channel.Setup();
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.listener != null);
            //Contract.Invariant(this.)

        }
    }
}
