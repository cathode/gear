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
using System.Windows.Forms;
using Gear.Client.UI;

namespace Gear.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Gear Client main entry point.

             * Initialization process:
             * 
             * 1. Set up environment
             * 2. Set up logging
             * 3. ???
             * 4. PROFIT!!!
             */
            if (args.Length > 0)
            {
                // Check if the client is being invoked in 'shell-only' mode.
                if (args.Any(e => e == "--shell"))
                {
                    ShellProgram.Run();
                }

            }

            // DEBUG testing:
            Console.WriteLine("sleeping 5 seconds");
            System.Threading.Thread.Sleep(5000);

            var channel = Gear.Net.ConnectedChannel.ConnectTo(new System.Net.IPEndPoint(System.Net.IPAddress.Loopback, Services.ConnectionBrokerService.DefaultServicePort));
            //channel.QueueMessage()

            //channel.QueueMessage(new Gear.Net.Messages.ZoneDataRequestMessage { ChunkX = 1, ChunkY = 1, ChunkZ = 0, ZoneX = 2, ZoneY = 3, ZoneZ = 0 });

            System.Threading.Thread.Sleep(20000);

            channel.Teardown();

            // Start LAN discovery service:
            //var discovery = new Services.ServiceLocator();


            //var launcher = new GearLauncher();
            //launcher.Shown += delegate { Log.Write("test"); };
            //Application.Run(launcher);

            ////var chunk = new Chunk();

            //Console.Write("Press any key...");
            //Console.Read();
        }
    }
}
