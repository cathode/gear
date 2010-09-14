/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Net;
using System.Net.Sockets;

namespace Gear.Net
{
    /// <summary>
    /// Listens for incoming connections over TCP/IP.
    /// </summary>
    public class ConnectionListener
    {
        #region Fields
        private Socket listener;
        private bool isListening;
        private ushort listenPort;
        private EngineBase engine;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionListener"/> class.
        /// </summary>
        /// <param name="engine">The <see cref="EngineBase"/> which the new <see cref="ConnectionListener"/> belongs to.</param>
        public ConnectionListener(EngineBase engine)
        {
            this.engine = engine;
            this.listenPort = Connection.DefaultPort;
        }
        #endregion
        #region Events
        public event EventHandler<ConnectionEventArgs> ConnectionAccepted;
        #endregion
        #region Properties

        #endregion
        #region Methods
        /// <summary>
        /// Starts listening for connections.
        /// </summary>
        public void Start()
        {
            if (this.listener != null)
                this.listener.Close();

            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            this.listener.Bind(new IPEndPoint(IPAddress.Any, this.listenPort));
            this.isListening = true;
            this.listener.BeginAccept(new AsyncCallback(this.AcceptCallback), null);
        }

        /// <summary>
        /// Stops listening for connections.
        /// </summary>
        public void Stop()
        {
            this.listener.Close();
            this.isListening = false;
        }

        protected virtual void AcceptCallback(IAsyncResult result)
        {
            var s = this.listener.EndAccept(result);
            this.listener.BeginAccept(new AsyncCallback(this.AcceptCallback), null);

            ServerConnection connection = new ServerConnection(s);
            if (this.engine != null)
                connection.Attach(this.engine);
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
