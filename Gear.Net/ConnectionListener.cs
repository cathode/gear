/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gear.Net
{
    /// <summary>
    /// Listens for incoming connections and handles the mundane socket work.
    /// </summary>
    public class ConnectionListener
    {
        #region Fields
        public static readonly int DefaultBacklogSize = 10;
        private readonly Socket listener;
        private bool isListening;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionListener"/> class.
        /// </summary>
        /// <param name="port">The port number that the listening socket operates on.</param>
        public ConnectionListener(ushort port)
        {
            this.BacklogSize = ConnectionListener.DefaultBacklogSize;

            this.ListenPort = port;
            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        #endregion

        #region Events

        /// <summary>
        /// Raised when an incoming connection is accepted and forked off into a new <see cref="Channel"/> instance.
        /// </summary>
        public event EventHandler<ChannelEventArgs> ChannelConnected;

        public event EventHandler ListenError;
        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the <see cref="ConnectionListener"/> is actively listening for connections.
        /// </summary>
        public bool IsListening
        {
            get
            {
                return this.isListening;
            }
        }

        /// <summary>
        /// Gets the port number that the listener operates on.
        /// </summary>
        public ushort ListenPort { get; set; }

        public int BacklogSize { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Starts the listening socket to accept incoming connections on the currently configured <see cref="ListenPort"/>.
        /// </summary>
        public void Start()
        {
            if (this.isListening)
            {
                return;
            }

            this.isListening = true;

            this.listener.Bind(new IPEndPoint(IPAddress.Any, this.ListenPort));
            this.listener.Listen(this.BacklogSize);

            // while (!this.token.IsCancellationRequested)
            while (this.isListening)
            {
                //try
                //{
                var sock = this.listener.Accept();

                Task.Run(() =>
                {
                    var channel = new ConnectedChannel(sock);
                    this.OnChannelConnected(this, new ChannelEventArgs(channel));
                });
                //}
                //catch (TimeoutException ex)
                //{
                //    // TODO: Add logging.
                //    this.isListening = false;
                //    break;
                //}
                //catch (SocketException ex)
                //{
                //    this.isListening = false;
                //    break;
                //}
            }
        }

        /// <summary>
        /// Starts the <see cref="ConnectionListener"/> as a background task and returns immediately.
        /// </summary>
        public void StartInBackground()
        {
            Task.Run(() => this.Start());
        }

        /// <summary>
        /// Stops the listening socket.
        /// </summary>
        public void Stop()
        {
            this.isListening = false;
            this.listener.Shutdown(SocketShutdown.Both);
            this.listener.Close();
        }

        /// <summary>
        /// Raises the <see cref="ChannelConnected"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnChannelConnected(object sender, ChannelEventArgs e)
        {
            Contract.Requires(e != null);

            if (this.ChannelConnected != null)
            {
                this.ChannelConnected(sender, e);
            }

            e.Channel.Setup();
        }

        /// <summary>
        /// Invariant code contracts for the <see cref="ConnectionListener"/> class.
        /// </summary>
        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.listener != null);
        }
        #endregion
    }
}
