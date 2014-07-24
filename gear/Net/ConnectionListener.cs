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
            Contract.Requires(port > 1024);

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

        public void Start()
        {
            if (this.listener != null)
                return;

            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            this.listener.Listen(32);

            //this.listener.AcceptAsync(SocketAsyncEventArgs.Empty);

        }

        public object StartInBackground()
        {
            this.Start();
            return null;
        }

        public void Stop()
        {

        }

        public void OnConnectionEstablished(EventArgs e)
        {

        }
    }
}
