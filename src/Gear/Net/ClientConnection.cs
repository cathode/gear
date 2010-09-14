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
    /// Represents a client-side connection.
    /// </summary>
    public class ClientConnection : Connection
    {
        public void Connect(IPAddress target)
        {
            this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Socket.BeginConnect(new IPEndPoint(target, Connection.DefaultPort), new AsyncCallback(this.ConnectCallback), null);
        }

        private void ConnectCallback(IAsyncResult result)
        {
            this.Socket.EndConnect(result);
            this.State = ConnectionState.Connected;
        }
    }
}
