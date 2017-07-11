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
using System.Threading;
using ProtoBuf;
using System.IO;

namespace Gear.Services
{
    /// <summary>
    /// Provides LAN service broadcast announcement to enable runtime discovery of available services.
    /// </summary>
    public class ServiceAnnouncer
    {
        public static readonly ushort AnnouncePort = 31900;

        private ServiceManager manager;

        private ulong packetSeq;

        public ServiceAnnouncer(ServiceManager manager)
        {
            this.manager = manager;
            //this.announcements = new List<ServiceInfo>();
            //this.ClusterId = manager.ClusterId; 
        }

        public bool Running { get; set; }

       
        /// <summary>
        /// Adds a service announcement to the current announcer.
        /// </summary>
        /// <param name="announcement"></param>
        public void AddServiceAnnouncement(ServiceInfo announcement)
        {
            //lock (this.announcements)
            //{
            //    // Only allow one subscription per port
            //    if (!this.announcements.Any(e => e.ListenPort == announcement.ListenPort))
            //    {
            //        this.announcements.Add(announcement);
            //    }
            //}
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
                //Log.Write("Sending announce packet #" + (n++).ToString(), "ServiceAnnouncer", LogMessageGroup.Debug);
                var anc = new ServiceAnnouncement()
                {
                    Version = "1.0-alpha",
                    ClusterId = this.manager.ClusterId,
                    AnnounceId = this.packetSeq++,
                    Services = this.manager.LocalServices.ToArray()
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

        public void EnsureServiceAnnouncerIsRunning()
        {

        }
    }
}
