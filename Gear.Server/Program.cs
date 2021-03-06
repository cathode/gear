﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gear.Net;
using Gear.Services;
using Newtonsoft.Json;
using GSCore;
using Gear.Net.ChannelPlugins.StreamTransfer;

namespace Gear.Server
{
    public class Program
    {
        public static long msgRecvCount;
        public static long msgRecvCountAtLastUpdate;
        public static DateTime perfCountStart;
        public static DateTime perfCountLastUpdate;

        private static Gear.Net.Collections.NetworkedCollection<DateTime> netStrings = new Net.Collections.NetworkedCollection<DateTime>();

        public static void Main(string[] args)
        {
            // Init logging first:
            Log.Write(LogMessageGroup.Important, "Initializing logging...");
            Log.Write(LogMessageGroup.Important, "Gear Server - v{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            //Console.WriteLine("Gear Server - v" + Assembly.GetExecutingAssembly().GetName().Version.ToString());
            // Read configuration.

            var ser = new JsonSerializer();
            ServerConfiguration config;

            using (var reader = new JsonTextReader(File.OpenText("./Configuration/Server.json")))
            {
                config = ser.Deserialize<ServerConfiguration>(reader);
            }

            StreamTransferPlugin.MaxGlobalActiveWorkers = 2;

            var clusterId = config.ClusterId;

            if (!string.IsNullOrEmpty(config.LogFile))
            {
                Log.BindOutput(File.Open(config.LogFile, FileMode.Append, FileAccess.Write, FileShare.None));
            }

            Gear.Net.MessageSerializationHelper.AddMessageSubtypes(typeof(Gear.Net.Channel).Assembly);

            // var engine = new ServerEngine();
            // engine.Run();

            // var manager = new ServiceManager(clusterId);

            // manager.EnsureServiceLocatorIsRunning();

            // manager.StartService(ServerService.ConnectionBroker, 14122);
            // manager.StartService(ServerService.ClusterManager, 14123);
            // manager.StartService(ServerService.ZoneNode, 14124);
            var listener = new ConnectionListener(9888);
            listener.ChannelConnected += Listener_ChannelConnected;
            listener.StartInBackground();

            while (true)
            {
                var k = Console.ReadKey();

                if (k.KeyChar == 'q')
                {
                    return;
                }
            }
        }

        private static void Listener_ChannelConnected(object sender, ChannelEventArgs e)
        {
            e.Channel.InvokeHandlersAsync = true;

            var stp = new Gear.Net.ChannelPlugins.StreamTransfer.StreamTransferPlugin();
            stp.Attach(e.Channel);
            stp.CanHostActiveTransfers = true;
           

            perfCountStart = DateTime.Now;
            perfCountLastUpdate = DateTime.Now;

            e.Channel.MessageReceived += Channel_MessageReceived;
        }

        static void Channel_MessageReceived(object sender, Net.MessageEventArgs e)
        {
            //// Update stats:
            //var count = Interlocked.Increment(ref Program.msgRecvCount);

            //if (count % 100 == 0)
            //{
            //    var periodTime = DateTime.Now - perfCountLastUpdate;
            //    if (periodTime.TotalSeconds > 1)
            //    {
            //        Program.perfCountLastUpdate = DateTime.Now;
            //        var last = Program.msgRecvCountAtLastUpdate;
            //        Program.msgRecvCountAtLastUpdate = count;
            //        var countPeriod = count - last;
            //        var msgsPerSec = countPeriod / periodTime.TotalSeconds;

            //        Log.Write(string.Format("Recieved {0} messages in the last {1}ms. {2} msg/s. {3} total messages received.", countPeriod, periodTime.TotalMilliseconds, msgsPerSec, count));
            //    }
            //}

            //var ch = sender as Net.ConnectedChannel;
            //ch.Send(new Net.Messages.BlockUpdateMessage { X = 1, Y = 2, Z = 3, NewBlockId = int.MaxValue });
        }
    }
}
