/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
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
    /// Represents a client-side connection.
    /// </summary>
    public class ClientConnection : Connection
    {
        #region Fields
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="ClientConnection"/> class.
        /// </summary>
        public ClientConnection()
        {
        }
        #endregion
        #region Events
        public event EventHandler ConnectionEstablished;
        #endregion
        #region Properties

        #endregion
        #region Methods
        public void Connect(IPAddress target)
        {
            this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Socket.BeginConnect(new IPEndPoint(target, Connection.DefaultPort), new AsyncCallback(this.ConnectCallback), null);
        }

        protected virtual void OnConnectionEstablished(EventArgs e)
        {
            if (this.ConnectionEstablished != null)
                this.ConnectionEstablished(this, e);

            this.Send(new ClientInfoMessage());
            this.Flush();
        }

        private void ConnectCallback(IAsyncResult result)
        {
            this.Socket.EndConnect(result);
            this.Start();
            this.OnConnectionEstablished(EventArgs.Empty);
        }
        #endregion
    }
}
