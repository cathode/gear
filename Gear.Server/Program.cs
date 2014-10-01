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
using System.IO;
using System.Threading;
using Gear.Services;

namespace Gear.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            // Fixed cluster id for debugging/testing
            var clusterId = new Guid("{FC000000-0000-0000-0000-000000000000}");
#else
            // TODO: Read cluster id from configuration file
            var clusterId = Guid.NewGuid();
#endif
            // Log to console.
            Log.BindOutput(Console.OpenStandardOutput());

            if (true)
                Log.BindOutput(File.Open("Gear.Server.log", FileMode.Append, FileAccess.Write, FileShare.None));

            Log.Write("Message log initialized", "system", LogMessageGroup.Info);

            //var engine = new ServerEngine();
            //engine.Run();


            var manager = new ServiceManager(clusterId);

            manager.StartService(ServerService.ConnectionBroker, 4122);
            manager.StartService(ServerService.ClusterManager, 4123);
            manager.StartService(ServerService.ZoneNode, 4124);

            while (true)
            {
                var k = Console.ReadKey();

                if (k.KeyChar == 'q')
                {
                    return;
                }
            }
        }
    }
}
