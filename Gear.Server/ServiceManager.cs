using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gear.Server
{
    public class ServiceManager
    {
        private ServiceAnnouncer announcer;
        private ServiceFinder finder;

        private Task announcerTask;
        private Task finderTask;


        public ServiceManager(Guid clusterId)
        {
            this.announcer = new ServiceAnnouncer(clusterId);
            this.finder = new ServiceFinder();

        }

        public void StartService(ServerService serviceType, ushort port)
        {
            this.announcer.AddServiceAnnouncement(new ServiceAnnouncement { LocalPort = port, Service = serviceType });

            this.announcerTask = new Task(this.announcer.Run, CancellationToken.None, TaskCreationOptions.LongRunning);
            this.finderTask = new Task(this.finder.Run, CancellationToken.None, TaskCreationOptions.LongRunning);

            this.finderTask.Start();
            Thread.Sleep(100);
            this.announcerTask.Start();
        }
    }
}
