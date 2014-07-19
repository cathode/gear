using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using ProtoBuf;

namespace Gear.Server
{
    /// <summary>
    /// Provides LAN service broadcast announcement to enable runtime discovery of available services.
    /// </summary>
    public class ServiceAnnouncer
    {
        public static readonly ushort AnnouncePort = 31900;

        private List<ServiceAnnouncement> announcements;

        public ServiceAnnouncer(Guid clusterId)
        {
            this.announcements = new List<ServiceAnnouncement>();
        }

        public bool Running { get; set; }

        /// <summary>
        /// Adds a service announcement to the current announcer.
        /// </summary>
        /// <param name="announcement"></param>
        public void AddServiceAnnouncement(ServiceAnnouncement announcement)
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
            int n = 0;
            while (this.Running)
            {
                Log.Write("Sending announce packet #" + (n++).ToString(), "ServiceAnnouncer", LogMessageGroup.Debug);

                client.Send(Encoding.ASCII.GetBytes("GEAR"), 4,
                    new IPEndPoint(IPAddress.Broadcast, ServiceAnnouncer.AnnouncePort));

                Thread.Sleep(interval);
            }
        }
    }

    public class ServiceAnnouncementContainer
    {
        public Guid ClusterId { get; set; }
        public ServiceAnnouncement[] Announcements { get; set; }
    }

    /// <summary>
    /// Represents the data in a service announcement which is broadcast over the LAN.
    /// </summary>
    public class ServiceAnnouncement
    {
        public ServerService Service { get; set; }
        public ushort LocalPort { get; set; }
    }
}
