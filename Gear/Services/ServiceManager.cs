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
        private ServiceLocator locator;

        private Task announcerTask;
        private Task locatorTask;

        private List<ServiceBase> managedServices;

        private List<ServiceInfo> remoteServices;
        //private List<ServiceInfo> localServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceManager"/> class.
        /// </summary>
        /// <param name="clusterId"></param>
        public ServiceManager(Guid clusterId)
        {
            this.ClusterId = clusterId;
            this.announcer = new ServiceAnnouncer(this);
            this.locator = new ServiceLocator();

            this.managedServices = new List<ServiceBase>();
            this.remoteServices = new List<ServiceInfo>();
            //this.localServices = new List<ServiceInfo>();
        }

        /// <summary>
        /// Gets or sets the unique id of the cluster that managed services participate in.
        /// </summary>
        public Guid ClusterId { get; set; }

        /// <summary>
        /// Gets a collection of <see cref="ServiceInfo"/> objects describing services running on the local (in-process) node.
        /// </summary>
        public IEnumerable<ServiceInfo> LocalServices
        {
            get
            {
                return this.managedServices.Select(e => e.GetServiceInfo());
            }
        }

        /// <summary>
        /// Gets a collection of <see cref="ServiceInfo"/> objects describing discovered services running on remote nodes (or out-of-process nodes).
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

        /// <summary>
        /// Starts a service of the specified type, with that service listening on the specified port number.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="port"></param>
        public void StartService(ServerService serviceType, ushort port = 0)
        {
            // Attempt to find an already managed service on the specified port.
            ServiceBase svc = this.managedServices.FirstOrDefault(e => e.ListenPort == port);

            if (svc == null)
            {
                // No managed service running on the specified port, create new instead.
                switch (serviceType)
                {
                    case ServerService.ClusterManager:
                        svc = new ClusterSupervisorService(port);
                        break;

                    case ServerService.ConnectionBroker:
                        svc = new ConnectionBrokerService(port);
                        break;

                    case ServerService.ZoneNode:
                        svc = new ZoneNodeService();
                        break;

                    default:
                        throw new NotImplementedException();
                }

                this.managedServices.Add(svc);
            }

            Log.Write("Attempting to start service: " + svc.GetType().Name);

            svc.Run();

            //this.localServices.Add(new ServiceInfo { ServiceType = serviceType, ListenPort = port });

            //this.EnsureServiceAnnouncerIsRunning();
        }


        public void EnsureServiceLocatorIsRunning()
        {
            if (!this.locator.Running)
                lock (this.locator)
                    if (!this.locator.Running)
                    {
                        this.locatorTask = new Task(this.locator.Run, CancellationToken.None, TaskCreationOptions.LongRunning);
                        this.locator.ServiceDiscovered += finder_ServiceDiscovered;
                        this.locatorTask.Start();
                    }
        }

        public void EnsureServiceAnnouncerIsRunning()
        {
            if (!this.announcer.Running)
                lock (this.announcer)
                    if (!this.announcer.Running)
                    {
                        this.announcerTask = new Task(this.announcer.Run, CancellationToken.None, TaskCreationOptions.LongRunning);
                        this.announcerTask.Start();
                    }
        }

        void finder_ServiceDiscovered(object sender, ServiceDiscoveredEventArgs e)
        {

        }

        
        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.announcer != null);
            Contract.Invariant(this.locator != null);
        }
    }
}
