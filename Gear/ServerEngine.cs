/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System.Net;
using System.Net.Sockets;
using System;

namespace Gear
{
    /// <summary>
    /// Represents a <see cref="EngineBase"/> implementation used by a Gear server.
    /// </summary>
    public class ServerEngine : EngineBase
    {
        #region Fields
        //private readonly ConnectionListener listener;
        private UdpClient udpClient;
        #endregion
        #region Constructors
        public ServerEngine()
        {
            //this.listener = new ConnectionListener();

            // this.InitGShell();
        }
        #endregion
        #region Properties

        #endregion
        #region Methods
        protected override void OnStarting(System.EventArgs e)
        {
            base.OnStarting(e);

            if (this.udpClient != null)
                this.udpClient.Close();

            var client = new UdpClient(new IPEndPoint(IPAddress.Any, 21073));
            client.EnableBroadcast = true;

            client.BeginReceive(new System.AsyncCallback(this.ReceiveCallback), client);
            //this.listener.Start();
        }

        private void ReceiveCallback(System.IAsyncResult ar)
        {
            if (ar == null)
                throw new NotImplementedException();

            var client = ar.AsyncState as UdpClient;

            if (client == null)
                return;
            var ep = new IPEndPoint(IPAddress.Any, 21073);

            var rec = client.EndReceive(ar, ref ep);

            Log.Write("Received broadcast from client");
        }

        /// <summary>
        /// Initializes the <see cref="GShell"/> used by the <see cref="ServerEngine"/>.
        /// </summary>
        private void InitGShell()
        {
            //this.Shell.Register(new GShellCommand("sv_maxplayers", this.
        }
        #endregion
    }
}
