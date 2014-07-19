using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using ProtoBuf;
using System.IO;

namespace Gear.Server.NetServices
{
    /// <summary>
    /// Provides LAN service broadcast announcement to enable runtime discovery of available services.
    /// </summary>
    public class ServiceAnnouncer
    {
        public static readonly ushort AnnouncePort = 31900;

        private List<ServiceInfo> announcements;


        public ServiceAnnouncer(Guid clusterId)
        {
            this.announcements = new List<ServiceInfo>();
            this.ClusterId = clusterId;
        }

        public bool Running { get; set; }

        public Guid ClusterId { get; private set; }
        /// <summary>
        /// Adds a service announcement to the current announcer.
        /// </summary>
        /// <param name="announcement"></param>
        public void AddServiceAnnouncement(ServiceInfo announcement)
        {
            lock (this.announcements)
            {
                // Only allow one subscription per port
                if (!this.announcements.Any(e => e.LocalPort == announcement.LocalPort))
                {
                    this.announcements.Add(announcement);
                }
            }
        }

        public void Run()
        {
            // temporary hardcoded announce interval: 10sec;
            TimeSpan interval = TimeSpan.FromSeconds(10);

            this.Running = true;

            var client = new System.Net.Sockets.UdpClient();
            client.EnableBroadcast = true;
            //client.
            //client.Connect(new IPEndPoint(IPAddress.Broadcast, ServiceAnnouncer.AnnouncePort));
            ulong n = 0;
            while (this.Running)
            {
                Log.Write("Sending announce packet #" + (n++).ToString(), "ServiceAnnouncer", LogMessageGroup.Debug);
                var anc = new ServiceAnnouncement()
                {
                    Version = "1.0-alpha",
                    ClusterId = this.ClusterId,
                    AnnounceId = n++,
                    Services = this.announcements.ToArray()
                };

                using (var ms = new MemoryStream())
                {
                    Serializer.Serialize(ms, anc);
                    var buffer = ms.ToArray();
                    client.Send(buffer, buffer.Length,
                        new IPEndPoint(IPAddress.Broadcast, ServiceAnnouncer.AnnouncePort));
                }

                Thread.Sleep(interval);
            }
        }
    }
}
