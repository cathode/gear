﻿/******************************************************************************
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
        public static long msgRecvCount;
        public static long msgRecvCountAtLastUpdate;
        public static DateTime perfCountStart;
        public static DateTime perfCountLastUpdate;

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

            Net.MessageSerializationHelper.AddMessageSubtypes();
            //var engine = new ServerEngine();
            //engine.Run();


            //var manager = new ServiceManager(clusterId);

            //manager.EnsureServiceLocatorIsRunning();

            //manager.StartService(ServerService.ConnectionBroker, 14122);
            //manager.StartService(ServerService.ClusterManager, 14123);
            //manager.StartService(ServerService.ZoneNode, 14124);

            var listener = new Net.ConnectionListener(9888);
            listener.ChannelConnected += listener_ChannelConnected;
            listener.Start();


            while (true)
            {
                var k = Console.ReadKey();

                if (k.KeyChar == 'q')
                {
                    return;
                }
            }
        }

        static void listener_ChannelConnected(object sender, Net.ChannelEventArgs e)
        {
            perfCountStart = DateTime.Now;
            perfCountLastUpdate = DateTime.Now;

            e.Channel.MessageReceived += Channel_MessageReceived;

        }

        static void Channel_MessageReceived(object sender, Net.MessageEventArgs e)
        {
            // Update stats:
            var count = Interlocked.Increment(ref Program.msgRecvCount);
            
            if (count % 100 == 0)
            {
                var periodTime = (DateTime.Now - perfCountLastUpdate);
                if (periodTime.TotalSeconds > 1)
                {
                    Program.perfCountLastUpdate = DateTime.Now;
                    var last = Program.msgRecvCountAtLastUpdate;
                    Program.msgRecvCountAtLastUpdate = count;
                    var countPeriod = count - last;
                    var msgsPerSec = countPeriod / periodTime.TotalSeconds;
                    

                    Log.Write(string.Format("Recieved {0} messages in the last {1}ms. {2} msg/s. {3} total messages received.", countPeriod, periodTime.TotalMilliseconds, msgsPerSec, count));
                }
            }

            var ch = sender as Net.ConnectedChannel;
            ch.Send(new Net.Messages.BlockUpdateMessage { X = 1, Y = 2, Z = 3, NewBlockId = int.MaxValue });


        }

    }
}
