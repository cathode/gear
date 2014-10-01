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

            // Log to console.
            Log.BindOutput(Console.OpenStandardOutput());

            if (true)
                Log.BindOutput(File.Open("Gear.Server.log", FileMode.Append, FileAccess.Write, FileShare.None));

            Log.Write("Message log initialized", "system", LogMessageGroup.Info);

            //var engine = new ServerEngine();
            //engine.Run();
            var clusterId = Guid.NewGuid();

            //var manager = new ServiceManager(clusterId);

            var listener = new Gear.Net.ConnectionListener(8820);
            listener.StartInBackground();

            Thread.Sleep(2000);

            var client = Gear.Net.Channel.ConnectTo(new System.Net.IPEndPoint(System.Net.IPAddress.Loopback, 8820));

            //manager.StartService(ServerService.ConnectionBroker, 4122);

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
