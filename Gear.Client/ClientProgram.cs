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
using Gear.Net.ChannelPlugins.StreamTransfer;
using GSCore;

namespace Gear.Client
{
    public class ClientProgram
    {
        public static void Main(string[] args)
        {
            // Set up logging to console.
            Log.ConsoleOutputGroups = LogMessageGroup.All;
            Log.Write(LogMessageGroup.Important, "Initializing logging...");
            Log.Write(LogMessageGroup.Important, "Gear Client - v{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            // Add builtin message subtypes from Gear.Net:
            MessageSerializationHelper.AddMessageSubtypes();

            // Add message subtypes from main game library:
            MessageSerializationHelper.AddMessageSubtypes(typeof(EngineBase).Assembly);

            // For testing
            Thread.Sleep(1000);

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
    }
}
