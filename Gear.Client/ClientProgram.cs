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
        private static SceneGraph.Scene activeScene;
        private static Gear.Client.Rendering.GLRenderer renderer;

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

            var renderer = ClientProgram.renderer = new Gear.Client.Rendering.GLRenderer();
            // var renderer = new Gear.Client.Rendering.Software.SoftwareRenderer();
            renderer.Initialize(new Rendering.RendererOptions());

            var scene = ClientProgram.activeScene = new SceneGraph.Scene();
            scene.Root = new SceneGraph.Node(Gear.Modeling.Mesh.NewIcosahedron(1.0f));
            scene.Root.Position = new Vector3d(0, 0, -0.2);
            scene.Root.Orientation = Quaternion.LookAt(new Vector3d(1, 0, 0));
            scene.Root.TransformOrder = SceneGraph.TransformOrder.TranslateRotateScale;
            renderer.Scene = scene;

            renderer.PreRender += (o, e) =>
            {
                scene.Root.Orientation = scene.Root.Orientation.RotateBy(1);
            };

            renderer.GameWindow.MouseDown += GameWindow_MouseDown;
            renderer.GameWindow.MouseUp += GameWindow_MouseUp;
            renderer.GameWindow.MouseMove += GameWindow_MouseMove;
            renderer.GameWindow.MouseWheel += GameWindow_MouseWheel;

            renderer.Start();
        }

        private static void GameWindow_MouseWheel(object sender, OpenTK.Input.MouseWheelEventArgs e)
        {
            //throw new NotImplementedException();
            ClientProgram.renderer.ActiveCamera.FocalDistance += e.DeltaPrecise;
        }

        private static void GameWindow_MouseMove(object sender, OpenTK.Input.MouseMoveEventArgs e)
        {
            if (ClientProgram.isMouseDown)
            {
                var pos = ClientProgram.renderer.ActiveCamera.Position;

                pos = pos + new Vector3d(e.XDelta * 0.1, e.YDelta * 0.1, 0);
                ClientProgram.renderer.ActiveCamera.Position = pos;
                ClientProgram.renderer.ActiveCamera.Orientation = ClientProgram.renderer.ActiveCamera.Orientation = Quaternion.LookAt(pos, Vector3d.Zero);
            }
        }

        private static bool isMouseDown = false;

        private static void GameWindow_MouseUp(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }

        private static void GameWindow_MouseDown(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            isMouseDown = true;
        }
    }
}
