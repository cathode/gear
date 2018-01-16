/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using ProtoBuf;
using Gear.Net;
using GSCore;

namespace Gear.Services
{
    /// <summary>
    /// Listens for service announcement broadcasts and notifies other components in the software of potential external cluster nodes on the network.
    /// </summary>
    public class ServiceLocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocator"/> class.
        /// </summary>
        public ServiceLocator()
        {
            this.Timeout = TimeSpan.FromSeconds(15);
        }

        /// <summary>
        /// Notifies subscribers when a remote service is located.
        /// </summary>
        public event EventHandler<ServiceDiscoveredEventArgs> ServiceDiscovered;

        /// <summary>
        /// Gets a value indicating whether the locator is searching for services running on known subnets.
        /// </summary>
        public bool Running { get; private set; }

        public TimeSpan Timeout { get; set; }

        public void Run()
        {
            // Create a client that binds to a random local port.
            var client = new System.Net.Sockets.UdpClient();

            this.Running = true;
            var ep = new IPEndPoint(IPAddress.Broadcast, Ports.Locator);

            // Generate the location request
            var req = new Gear.Net.Messages.LocatorRequestMessage();

            return;

            while (this.Running)
            {
                var buffer = client.Receive(ref ep);

                try
                {
                    using (var stream = new MemoryStream(buffer))
                    {
                        var obj = Serializer.Deserialize<ServiceAnnouncement>(stream);

                        if (obj.Services != null)
                        {
                            Log.Write("Received announcement from node in cluster: " + obj.ClusterId.ToString());
                            foreach (var service in obj.Services)
                            {
                                this.OnServiceDiscovered(new ServiceDiscoveredEventArgs { ClusterId = obj.ClusterId, Info = service });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write("Exception while decoding broadcast message: " + ex.ToString());
                }
            }
        }

        private void OnServiceDiscovered(ServiceDiscoveredEventArgs e)
        {
            if (this.ServiceDiscovered != null)
            {
                this.ServiceDiscovered(null, e);
            }
        }
    }

    public class ServiceDiscoveredEventArgs : EventArgs
    {
        public Guid ClusterId { get; set; }

        public ServiceInfo Info { get; set; }
    }
}
