using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;


namespace Gear.Server
{
    public class ServiceAnnouncer
    {
        private List<ServiceAnnouncement> announcements;
        public ServiceAnnouncer()
        {
            this.announcements = new List<ServiceAnnouncement>();
        }

        /// <summary>
        /// Adds a service announcement to the current announcer.
        /// </summary>
        /// <param name="announcement"></param>
        public void AddServiceAnnouncement(ServiceAnnouncement announcement)
        {
            if (!this.announcements.Any(e => e.LocalPort == announcement.LocalPort))
            {

            }

            this.announcements.Add(announcement);

        }
    }

    public class ServiceAnnouncement
    {
        public ServerService Service { get; set; }

        public ushort LocalPort { get; set; }
    }
}
