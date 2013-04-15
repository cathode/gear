/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using Gear.Net;
using System.Net;
using System.Net.Sockets;

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
