using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    /// <summary>
    /// Represents a network connection target as a hostname and tcp/udp port number combination.
    /// </summary>
    public class IPTarget
    {
        #region Fields

        /// <summary>
        /// The default number of milliseconds to wait when determining if an endpoint is reachable.
        /// </summary>
        public const int DefaultReachableTimeout = 500;
        private string hostname;
        private ushort port;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="IPTarget"/> class.
        /// </summary>
        /// <param name="hostname">The DNS host to connect to.</param>
        /// <param name="port">The port number to connect to.</param>
        public IPTarget(string hostname, ushort port)
        {
            Contract.Requires<ArgumentNullException>(hostname != null);

            this.hostname = hostname;
            this.port = port;
        }

        /// <summary>
        /// Gets the DNS host name string that will be used to connect to.
        /// </summary>
        public string Hostname
        {
            get
            {
                return this.hostname;
            }
        }

        /// <summary>
        /// Gets the numeric TCP port number that will be used to connect to.
        /// </summary>
        public ushort Port
        {
            get
            {
                return this.port;
            }
        }

        /// <summary>
        /// Creates and returns a new <see cref="IPTarget"/> based off the specified <see cref="IPAddress"/> and port number.
        /// </summary>
        /// <remarks>
        /// This method of creating an <see cref="IPTarget"/> performs a reverse DNS lookup on the specified address, and attempts
        /// to create the new <see cref="IPTarget"/> using the hostname resulting from the reverse lookup.
        /// </remarks>
        /// <param name="address"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static IPTarget FromIPAddress(IPAddress address, ushort port)
        {
            Contract.Requires<ArgumentNullException>(address != null);
            Contract.Ensures(Contract.Result<IPTarget>() != null);

            return IPTarget.FromIPEndPoint(new IPEndPoint(address, port));
        }

        /// <summary>
        /// Creates and returns a new <see cref="IPTarget"/> based off the specified <see cref="IPEndPoint"/>.
        /// </summary>
        /// <remarks>
        /// This method of creating an <see cref="IPTarget"/> performs a reverse DNS lookup on the specified address, and attempts
        /// to create the new <see cref="IPTarget"/> using the hostname resulting from the reverse lookup.
        /// </remarks>
        /// <param name="ep"></param>
        /// <returns></returns>
        public static IPTarget FromIPEndPoint(IPEndPoint ep)
        {
            Contract.Requires<ArgumentNullException>(ep != null);
            Contract.Ensures(Contract.Result<IPTarget>() != null);

            var hostEntry = Dns.GetHostEntry(ep.Address);

            return new IPTarget(hostEntry.HostName, (ushort)ep.Port);
        }

        public IPEndPoint[] ResolveEndPoints()
        {
            var hostEntry = Dns.GetHostEntry(this.Hostname);
            var addresses = hostEntry.AddressList.ToArray();

            return addresses.Select(e => new IPEndPoint(e, this.Port)).ToArray();
        }

        /// <summary>
        /// Returns an <see cref="IPEndPoint"/> resolved from the hostname of the current <see cref="IPTarget"/>.
        /// </summary>
        /// <remarks>
        /// The returned <see cref="IPEndPoint"/> is considered reachable because this method attempts to open a TCP
        /// socket connection to the remote address and port number.
        /// </remarks>
        /// <param name="reachableTimeout">The number of seconds to wait when determining if a remote endpoint is reachable.</param>
        /// <returns>A new <see cref="IPEndPoint"/> instance if a reachable endpoint was found; otherwise null.</returns>
        public IPEndPoint GetNextReachableEndPoint(int reachableTimeout = IPTarget.DefaultReachableTimeout)
        {
            var hostEntry = Dns.GetHostEntry(this.Hostname);

            // Rely on ordering of DNS query result.
            var addresses = hostEntry.AddressList.ToArray();

            for (int i = 0; i < addresses.Length; ++i)
            {
                var addr = addresses[i];

                using (TcpClient tc = new TcpClient())
                {
                    try
                    {
                        // Try to open TCP connection to remote
                        var result = tc.ConnectAsync(addr, this.Port);

                        // Allow for connection attempt
                        if (result.Wait(reachableTimeout))
                        {
                            return new IPEndPoint(addr, this.Port);
                        }
                    }
                    catch
                    {
                    }
                }
            }

            return new IPEndPoint(addresses[0], this.Port);
        }
    }
}
