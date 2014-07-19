using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Gear.Server
{
    public class ServiceFinder
    {
        public ServiceFinder()
        {

        }

        public event EventHandler<ServiceDiscoveredEventArgs> ServiceDiscovered;

        public bool Running { get; set; }

        public void Run()
        {
            var client = new System.Net.Sockets.UdpClient(new IPEndPoint(IPAddress.Any, ServiceAnnouncer.AnnouncePort));

            this.Running = true;
            var ep = new IPEndPoint(IPAddress.Broadcast, ServiceAnnouncer.AnnouncePort);

            //.Client.Bind(ep);

            while (this.Running)
            {
                var buffer = client.Receive(ref ep);

                Log.Write("Received broadcast: " + Encoding.ASCII.GetString(buffer));


            }
        }

        protected virtual void OnServiceDiscovered(ServiceDiscoveredEventArgs e)
        {
            this.ServiceDiscovered(this, e);
        }
    }

    public class ServiceDiscoveredEventArgs : EventArgs
    {
        public Guid ClusterId { get; set; }
    }
}
