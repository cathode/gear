/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gear.Client.UI;
using Gear.Geometry;
using Gear.Model;
using Gear.Modeling.Primitives;
using Gear.Net;
using GSCore;

namespace Gear.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // Log to console.
            Log.Write(LogMessageGroup.Critical, "Initializing logging...");
            Log.Write(LogMessageGroup.Important, "Gear Client - v{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            //MessageSerializationHelper.AddMessageSubtypes();
            MessageSerializationHelper.AddMessageSubtypes(typeof(Channel).Assembly);

            // For testing
            Thread.Sleep(1000);

            //var target = new IPTarget("localhost", 9888);

            //var ep = target.GetNextReachableEndPoint();

            //var tgt = IPTarget.FromIPEndPoint(ep);

            var channel = ConnectedChannel.ConnectTo(new System.Net.IPEndPoint(IPAddress.Loopback, 9888));

            var stp = new Gear.Net.ChannelPlugins.StreamTransfer.StreamTransferPlugin();
            stp.Attach(channel);
            stp.CanHostActiveTransfers = false;

            stp.SendFile("Gear.Client.exe.config");
            //var ns = new Gear.Net.Collections.NetworkedCollection<DateTime>();
            //ns.Consume(1234, channel);

            //ns.ItemAdded += Ns_ItemAdded;

            while (true)
            {
                Thread.Sleep(10000);
            }
        }

        private static void Ns_ItemAdded(object sender, Gear.Net.Collections.NetworkedCollectionItemEventArgs<DateTime> e)
        {
            Log.Write(string.Format("Item added to collection: {0}", e.Items.FirstOrDefault()));
        }

        /*
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                // Check if the client is being invoked in 'shell-only' mode.
                if (args.Any(e => e == "--shell"))
                {
                    ShellProgram.Run();
                }
            }

            // test, generate world:
            var seed = 1234;
            // Init planet generator
            var world = new PlanetWorld(seed);
            // Setup generator settings
            var pmin = new PlanetWorldParameters();
            var pmax = new PlanetWorldParameters();
            pmax.AverageDensity = 6500; // Maximum 6500 kg/m3
            pmin.AverageDensity = 4000;
            pmax.DiameterKm = 14000;
            pmin.DiameterKm = 11000;

            world.Initialize(pmin, pmax);

            Gear.Net.MessageSerializationHelper.AddMessageSubtypes();

            var renderer = new Gear.Client.Rendering.GLRenderer();
            // var renderer = new Gear.Client.Rendering.Software.SoftwareRenderer();
            renderer.Initialize(new Rendering.RendererOptions());

            var scene = new SceneGraph.Scene();
            scene.Root = new SceneGraph.Node(Gear.Modeling.Mesh.NewIcosahedron(0.45f));
            scene.Root.Position = new Vector3d(0, 0, -0.2);
            var cube = new SceneGraph.Node(Gear.Modeling.Mesh.NewCube(0.1f));
            cube.Position = new Vector3d(0.5, 0, 0.25);
            scene.Root.Add(cube);

            renderer.Scene = scene;

            renderer.PreRender += (o, e) => { scene.Root.Orientation.RotateBy(20); };
            renderer.Start();
        }
    */
    }
}
