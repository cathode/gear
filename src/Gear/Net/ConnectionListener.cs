/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        #endregion
        #region Constructors
        public ConnectionListener()
        {
            this.listenPort = Connection.DefaultPort;
            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        #endregion
        #region Events

        #endregion
        #region Properties

        #endregion
        #region Methods
        public void Start()
        {
            this.listener.Bind(new IPEndPoint(IPAddress.Any, this.listenPort));
            this.isListening = true;
            this.listener.BeginAccept(new AsyncCallback(this.AcceptCallback), null);
        }

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
        }
        #endregion
    }
}
