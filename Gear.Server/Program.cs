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
using Newtonsoft.Json;
using System.Reflection;

namespace Gear.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Gear Server - v" + Assembly.GetExecutingAssembly().GetName().Version.ToString());
            // Read configuration.

            var ser = new JsonSerializer();
            ServerConfiguration config;

            using (var reader = new JsonTextReader(File.OpenText("./Configuration/Server.json")))
            {
                config = ser.Deserialize<ServerConfiguration>(reader);
            }
            var clusterId = config.ClusterId;

            // Log to console.
            Log.BindOutput(Console.OpenStandardOutput());

            if (!string.IsNullOrEmpty(config.LogFile))
                Log.BindOutput(File.Open(config.LogFile, FileMode.Append, FileAccess.Write, FileShare.None));

            Log.Write("Message log initialized", "system", LogMessageGroup.Info);

            //var engine = new ServerEngine();
            //engine.Run();


            var manager = new ServiceManager(clusterId);

            manager.EnsureServiceLocatorIsRunning();

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
