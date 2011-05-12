/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Net;
using System.Net.Sockets;
using Gear.Net.Messaging;

namespace Gear.Net
{
    /// <summary>
    /// Listens for incoming connections over TCP/IP.
    /// </summary>
    public class ConnectionListener
    {
        #region Fields
        /// <summary>
        /// Holds the <see cref="Socket"/> that listens for incoming connections.
        /// </summary>
        private Socket listener;

        /// <summary>
        /// Backing field for the <see cref="ConnectionListener.IsListening"/> property.
        /// </summary>
        private bool isListening;
        private ushort listenPort;
        private ServerInfoMessage serverInfoMessage;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="ConnectionListener"/> class.
        /// </summary>
        /// <param name="engine">The <see cref="EngineBase"/> which the new <see cref="ConnectionListener"/> belongs to.</param>
        public ConnectionListener()
        {
            this.listenPort = Connection.DefaultPort;
            this.serverInfoMessage = new ServerInfoMessage();
        }
        public ConnectionListener(ushort listenPort)
        {
            this.listenPort = listenPort;
            this.serverInfoMessage = new ServerInfoMessage();
        }
        #endregion
        #region Events
        /// <summary>
        /// Raised when a new <see cref="Connection"/> is accepted.
        /// </summary>
        public event EventHandler<ConnectionEventArgs> ConnectionAccepted;
        #endregion
        #region Properties
        /// <summary>
        /// Gets a value indicating whether the current <see cref="ConnectionListener"/> is actively listening for incoming connections.
        /// </summary>
        public bool IsListening
        {
            get
            {
                return this.isListening;
            }
        }

        /// <summary>
        /// Gets or sets the port number used to listen for connections on.
        /// </summary>
        public ushort ListenPort
        {
            get
            {
                return this.listenPort;
            }
            set
            {
                this.listenPort = value;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Starts listening for connections. Returns immediately.
        /// </summary>
        public void Start()
        {
            if (this.IsListening)
                this.Stop();

            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            this.listener.Bind(new IPEndPoint(IPAddress.Any, this.listenPort));
            this.isListening = true;
            this.listener.Listen(10);
            this.listener.BeginAccept(new AsyncCallback(this.AcceptCallback), null);
            Log.Write(string.Format("Listening for connections on {0}", this.listener.LocalEndPoint), "network", LogMessageGroup.Info);
        }

        /// <summary>
        /// Stops listening for connections. Returns immediately.
        /// </summary>
        public void Stop()
        {
            if (!this.IsListening)
                return;

            if (this.listener != null)
                this.listener.Close();

            this.isListening = false;
        }

        protected virtual void AcceptCallback(IAsyncResult result)
        {
            var s = this.listener.EndAccept(result);
            this.listener.BeginAccept(new AsyncCallback(this.AcceptCallback), null);
            Log.Write(string.Format("Accepted connection from {0}", s.RemoteEndPoint), "network");
            ServerConnection connection = new ServerConnection(s);
            connection.Send(this.serverInfoMessage);
            this.OnConnectionAccepted(new ConnectionEventArgs(connection));
            connection.Start();
        }

        /// <summary>
        /// Raises the <see cref="ConnectionListener.ConnectionAccepted"/> event.
        /// </summary>
        /// <param name="e">A <see cref="ConnectionEventArgs"/> that contains the event data.</param>
        protected virtual void OnConnectionAccepted(ConnectionEventArgs e)
        {
            if (this.ConnectionAccepted != null)
                this.ConnectionAccepted(this, e);
        }
        #endregion
    }
}
