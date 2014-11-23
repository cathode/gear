/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2014 William 'cathode' Shelley. All Rights Reserved.      *
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

namespace Gear.Services
{
    /// <summary>
    /// Listens for service announcement broadcasts and notifies other components in the software of potential external cluster nodes on the network.
    /// </summary>
    public class ServiceLocator
    {
        

        public  event EventHandler<ServiceDiscoveredEventArgs> ServiceDiscovered;

        public  bool Running { get; set; }

        public  void Run()
        {
            var client = new System.Net.Sockets.UdpClient(new IPEndPoint(IPAddress.Any, ServiceAnnouncer.AnnouncePort));

            this.Running = true;
            var ep = new IPEndPoint(IPAddress.Broadcast, ServiceAnnouncer.AnnouncePort);


            //.Client.Bind(ep);

            while (this.Running)
            {
                var buffer = client.Receive(ref ep);

                try
                {
                    using (var stream = new MemoryStream(buffer))
                    {
                        var obj = Serializer.Deserialize<ServiceAnnouncement>(stream);
                        Log.Write("Received announcement from node in cluster: " + obj.ClusterId.ToString());



                        if (obj.Services != null)
                        {
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
                this.ServiceDiscovered(null, e);
        }
    }

    public class ServiceDiscoveredEventArgs : EventArgs
    {
        public Guid ClusterId { get; set; }

        public ServiceInfo Info { get; set; }
    }
}
