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
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Gear.Services
{
    /// <summary>
    /// Provides a class that manages node services.
    /// </summary>
    public class ServiceManager
    {
        private ServiceAnnouncer announcer;
        private ServiceFinder finder;

        private Task announcerTask;
        private Task finderTask;


        private List<ServiceInfo> remoteServices;
        private List<ServiceInfo> localServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceManager"/> class.
        /// </summary>
        /// <param name="clusterId"></param>
        public ServiceManager(Guid clusterId)
        {
            this.ClusterId = clusterId;
            this.announcer = new ServiceAnnouncer(this);
            this.finder = new ServiceFinder();

            this.remoteServices = new List<ServiceInfo>();
            this.localServices = new List<ServiceInfo>();
        }

        /// <summary>
        /// Gets or sets the unique id of the cluster that managed services participate in.
        /// </summary>
        public Guid ClusterId { get; set; }

        /// <summary>
        /// Gets a collection of <see cref="ServiceInfo"/> objects describing services running on the local (in-process) node.
        /// </summary>
        public List<ServiceInfo> LocalServices { get { return this.localServices; } }

        /// <summary>
        /// Gets a collection of <see cref="ServiceInfo"/> objects describing discovered services running on remote nodes (or out-of-process nodes)
        /// </summary>
        public List<ServiceInfo> RemoteServices { get { return this.remoteServices; } }

        /// <summary>
        /// Gets the <see cref="ServiceManager"/> for the specified cluster id.
        /// </summary>
        /// <param name="clusterId"></param>
        /// <returns></returns>
        public static ServiceManager ForCluster(Guid clusterId)
        {
            throw new NotImplementedException();
        }

        public void StartService(ServerService serviceType, ushort port)
        {
            this.announcer.AddServiceAnnouncement(new ServiceInfo { ListenPort = port, Service = serviceType });

            this.announcerTask = new Task(this.announcer.Run, CancellationToken.None, TaskCreationOptions.LongRunning);
            this.finderTask = new Task(this.finder.Run, CancellationToken.None, TaskCreationOptions.LongRunning);
            this.finder.ServiceDiscovered += finder_ServiceDiscovered;
            this.finderTask.Start();
            Thread.Sleep(100);
            this.announcerTask.Start();
        }

        void finder_ServiceDiscovered(object sender, ServiceDiscoveredEventArgs e)
        {

        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.announcer != null);
            Contract.Invariant(this.finder != null);

        }
    }
}
