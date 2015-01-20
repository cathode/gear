using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Server
{
    public class ServerConfiguration
    {
        /// <summary>
        /// Gets or sets the cluster id that the server will join.
        /// </summary>
        public Guid ClusterId { get; set; }

        /// <summary>
        /// Gets or sets the filesystem path which holds the log file for the server.
        /// </summary>
        public string LogFile { get; set; }

        /// <summary>
        /// Gets or sets a collection of configuration objects that describe the services provided by this server instance.
        /// </summary>
        public ServiceConfiguration[] Services { get; set; }
    }

    public class ServiceConfiguration
    {
        public string ServiceClass { get; set; }

        public ushort ListenPort { get; set; }
    }
}
