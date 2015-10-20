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

namespace Gear.Services
{
    /// <summary>
    /// Represents a service that orchestrates and supervises other services in the cluster.
    /// </summary>
    public class ClusterSupervisorService : ServiceBase
    {
        public static readonly ushort DefaultServicePort = 14123;

        public ClusterSupervisorService(ushort port)
        {
            if (port == 0)
                port = ClusterSupervisorService.DefaultServicePort;

            this.ListenPort = port;
        }
    }
}
