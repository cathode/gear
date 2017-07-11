/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private bool isRunning;
        private readonly Socket listener;
        // private Task t;

        // private CancellationToken token;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionListener"/> class.
        /// </summary>
        /// <param name="port">The port number that the listening socket operates on.</param>
        public ConnectionListener(ushort port)
        {
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

        /// <summary>
        /// Gets the port number that the listener operates on.
        /// </summary>
        public ushort ListenPort { get; private set; }

        /// <summary>
        /// Raised when an incoming connection is accepted and forked off into a new <see cref="Channel"/> instance.
        /// </summary>
        public event EventHandler<ChannelEventArgs> ChannelConnected;

        /// <summary>
        ///
        /// </summary>
        public void Start()
        {
            if (this.isRunning)
            {
                return;
            }

            this.isRunning = true;

            this.listener.Bind(new IPEndPoint(IPAddress.Any, this.ListenPort));
            this.listener.Listen(32);

            // while (!this.token.IsCancellationRequested)
            while (this.isRunning)
            {
                try
                {
                    var sock = this.listener.Accept();

                    var channel = new ConnectedChannel(sock);

                    this.OnConnectionEstablished(this, new ChannelEventArgs(channel));
                }
                catch (TimeoutException ex)
                {
                    // TODO: Add logging.
                    break;
                }
                catch (SocketException ex)
                {
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
            this.isRunning = false;
            this.listener.Shutdown(SocketShutdown.Both);
            this.listener.Close();
        }

        protected void OnConnectionEstablished(object sender, ChannelEventArgs e)
        {
            Contract.Requires(e != null);

            if (this.ChannelConnected != null)
            {
                this.ChannelConnected(sender, e);
            }

            e.Channel.Setup();
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.listener != null);
            // Contract.Invariant(this.)
        }
    }
}
