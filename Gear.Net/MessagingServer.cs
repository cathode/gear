using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    /// <summary>
    /// Implements a low-level framework for coordinating connections from clients.
    /// </summary>
    public class MessagingServer
    {
        #region Fields
        private ConnectionListener listener;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingServer"/> class.
        /// </summary>
        public MessagingServer()
        {
        }

        #endregion

        #region Events

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the internet protocol port number that the messaging server will accept connections on.
        /// </summary>
        public ushort ListenPort { get; set; }
        #endregion
        #region Methods

        /// <summary>
        /// Starts the messaging server.
        /// </summary>
        public void Start()
        {
            if (this.listener == null)
            {
                this.listener = new ConnectionListener(this.ListenPort);
            }
        }

        /// <summary>
        /// Stops all messaging activity related to the current messaging server.
        /// </summary>
        /// <param name="closeExistingConnections">If true, existing peer connections are stopped; otherwise, only new connections are stopped.</param>
        /// <param name="immediate">If true, existing peer connections are simply dropped without warning; otherwise peer connections are shut down gracefully.</param>
        /// <remarks>
        /// This method does not block, even if the <paramref name="immediate"/> parameter is passed as true.
        /// </remarks>
        public void Stop(bool closeExistingConnections = true, bool immediate = false)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
