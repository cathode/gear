/* Gear - A Steampunk Action-RPG --- http://trac.gearedstudios.com/gear/
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved. */
using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using Gear;
using Gear.Winforms;
using OpenTK;
using NetSgl.OpenGLProvider;
using NetSgl.SceneGraph;
using NetSgl.Geometry3;
using NetSgl.Geometry4;
using NetSgl.Geometry3.Primitives;

namespace Gear.Client
{
    internal static class Client
    {
        #region Fields
        internal static ClientEngine Engine;
        internal static GameShellForm ShellUI;
        internal static OpenGLRasterizer Rasterizer;
        #endregion
        #region Methods
        /// <summary>
        /// GearClient application entry point.
        /// </summary>
        /// <param name="args"></param>
        internal static void Main(string[] args)
        {
            // Initialize client engine
            //Client.Engine = new ClientEngine();
            //Client.ShellUI = new GameShellForm()
            //{
            //    Shell = Client.Engine.Shell
            //};

            var rasterizer = new OpenGLRasterizer();

            var scene = rasterizer.Scene;


            scene.Root.Add(new GeometryNode(new Hexahedron(0.5)));
            scene.BackgroundColor = System.Drawing.Color.Blue;

            Client.Rasterizer = rasterizer;

            ClientWindow window = new ClientWindow();

            window.Run(100.0, 30.0);
        }
        #endregion
    }
    internal class ClientWindow : GameWindow
    {
        private Vector2 clickStart;
        bool mouseDown;
        public const double sensitivity = 2.0;
        internal ClientWindow()
        {
            this.Mouse.ButtonDown += new EventHandler<OpenTK.Input.MouseButtonEventArgs>(Mouse_ButtonDown);
            this.Mouse.ButtonUp += new EventHandler<OpenTK.Input.MouseButtonEventArgs>(Mouse_ButtonUp);
            this.Mouse.Move += new EventHandler<OpenTK.Input.MouseMoveEventArgs>(Mouse_Move);
            this.Mouse.WheelChanged += new EventHandler<OpenTK.Input.MouseWheelEventArgs>(Mouse_WheelChanged);
        }

        void Mouse_WheelChanged(object sender, OpenTK.Input.MouseWheelEventArgs e)
        {
            var camera = Client.Rasterizer.Scene.ActiveCamera;

            var pos = camera.Position;
            var deltaZ = (e.DeltaPrecise / 10 * sensitivity);
            camera.Position = new NetSgl.Geometry3.Vector3(pos.X, pos.Y, pos.Z + deltaZ);
        }

        void Mouse_ButtonUp(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {

            if (e.Button == OpenTK.Input.MouseButton.Left)
                this.mouseDown = false;
        }

        void Mouse_Move(object sender, OpenTK.Input.MouseMoveEventArgs e)
        {
            if (this.mouseDown)
            {
                var camera = Client.Rasterizer.Scene.ActiveCamera;

                var pos = camera.Position;
                var deltaX = ((double)e.XDelta / this.Width) * sensitivity;
                var deltaY = ((double)e.YDelta / this.Height) * sensitivity;
                camera.Position = new NetSgl.Geometry3.Vector3(pos.X + deltaX, pos.Y + deltaY, pos.Z);
            }
        }

        void Mouse_ButtonDown(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            if (e.Button == OpenTK.Input.MouseButton.Left)
                this.mouseDown = true;

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Client.Rasterizer.Initialize();
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            Client.Rasterizer.RenderFrame();
            this.SwapBuffers();
        }

    }
}
