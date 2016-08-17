/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gear.Client.SceneGraph;
using Gear.Geometry;
using System.Diagnostics.Contracts;
//using Gear.Client.Platform.Microsoft;
//using Gear.Client.Platform.Microsoft.OpenGL;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using Gear.Client.Platform;

namespace Gear.Client.Rendering
{
    /// <summary>
    /// Provides a real-time renderer that utilizes the OpenGL API.
    /// </summary>
    public class GLRenderer : IDisposable
    {
        #region Fields
        private bool isDisposed;
        private OpenTK.GameWindow gameWindow;

        /// <summary>
        /// Backing field for the <see cref="Renderer.ActiveCamera"/> property.
        /// </summary>
        private Camera activeCamera;

        /// <summary>
        /// Backing field for the <see cref="Renderer.IsInitialized"/> property.
        /// </summary>
        private bool isInitialized;

        /// <summary>
        /// Backing field for the <see cref="Renderer.IsRunning"/> property.
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// Backing field for the <see cref="Renderer.Profile"/> property.
        /// </summary>
        private DisplayProfile profile;

        /// <summary>
        /// Backing field for the <see cref="Renderer.Scene"/> property.
        /// </summary>
        private Scene scene;

        /// <summary>
        /// Backing field for the <see cref="Scene.BackgroundColor"/> property.
        /// </summary>
        private Vector4f backgroundColor;

        private int frameCount;

        private DateTime lastCheck;

        private double lastRate;
        private double currentRate;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GLRenderer"/> class.
        /// </summary>
        public GLRenderer()
        {
            this.activeCamera = new Camera()
            {
                FieldOfView = Angle.FromDegrees(45),
                FocalDistance = 1000,
                Mode = CameraMode.Perspective
            };

            this.Scene = new Scene();

            var gw = new GameWindow();
            this.gameWindow = gw;

            gw.Load += (sender, e) =>
            {
                gw.VSync = VSyncMode.On;
            };

            gw.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, gw.Width, gw.Height);
                };

            gw.UpdateFrame += (sender, e) =>
                {
                    // add game logic, input handling
                    if (gw.Keyboard[Key.Escape])
                    {
                        gw.Exit();
                    }
                };

            gw.RenderFrame += (sender, e) =>
                {
                    this.RenderFrame();
                };
        }

        ~GLRenderer()
        {
            this.Dispose(false);
        }
        #endregion
        #region Events
        /// <summary>
        /// Raised when the <see cref="Renderer"/> is performing it's initial set-up.
        /// </summary>
        public event EventHandler<RendererInitializationEventArgs> Initializing;

        /// <summary>
        /// Raised after a frame has been rendered.
        /// </summary>
        public event EventHandler<RenderEventArgs> PostRender;

        /// <summary>
        /// Raised prior to a frame being rendered.
        /// </summary>
        public event EventHandler<RenderEventArgs> PreRender;

        /// <summary>
        /// Raised when the active <see cref="DisplayProfile"/> is changed.
        /// </summary>
        public event EventHandler ProfileChanged;

        /// <summary>
        /// Raised when a frame is rendered.
        /// </summary>
        public event EventHandler<RenderEventArgs> Render;

        /// <summary>
        /// Raised when the active <see cref="Scene"/> is changed.
        /// </summary>
        public event EventHandler SceneChanged;

        /// <summary>
        /// Raised when the renderer is started.
        /// </summary>
        public event EventHandler Starting;

        /// <summary>
        /// Raised when the renderer is stopped.
        /// </summary>
        public event EventHandler Stopping;
        #endregion
        #region Properties
        public bool IsDisposed
        {
            get
            {
                return this.isDisposed;
            }
        }
        /// <summary>
        /// Gets or sets the background color of the scene.
        /// </summary>
        public Vector4f BackgroundColor
        {
            get
            {
                return this.backgroundColor;
            }
            set
            {
                this.backgroundColor = value;
            }
        }
        /// <summary>
        /// Gets a value indicating whether the current renderer has been initialized.
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return this.isInitialized;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current renderer is running (has been started).
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        /// <summary>
        /// Gets or sets the active <see cref="DisplayProfile"/> for the current renderer.
        /// </summary>
        public DisplayProfile Profile
        {
            get
            {
                return this.profile;
            }
            set
            {
                Contract.Requires(value != null);

                this.profile = value;
                this.OnProfileChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the active <see cref="Scene"/> for the current renderer.
        /// </summary>
        public Scene Scene
        {
            get
            {
                return this.scene;
            }
            set
            {
                Contract.Requires(value != null);

                this.scene = value;
                this.OnSceneChanged(EventArgs.Empty);
            }
        }

        public Camera ActiveCamera
        {
            get
            {
                return this.activeCamera;
            }
            set
            {
                Contract.Requires(value != null);

                this.activeCamera = value;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Sets up the renderer.
        /// </summary>
        /// <param name="options">The configuration data that describes how the renderer should be set up.</param>
        public void Initialize(RendererOptions options)
        {
            Contract.Requires(options != null);

            this.OnInitializing(new RendererInitializationEventArgs(options));

            this.Profile = options.Profile ?? DisplayProfile.Default;

            this.isInitialized = true;
        }

        /// <summary>
        /// Renders a frame.
        /// </summary>
        public void RenderFrame()
        {
            //var options = this.CreateFrameOptions();
            var e = new RenderEventArgs();

            this.OnPreRender(e);
            this.OnRender(e);
            this.OnPostRender(e);
        }

        /// <summary>
        /// Starts the frame rendering process.
        /// </summary>
        public virtual void Start()
        {
            if (this.isRunning)
                return;

            this.isRunning = true;
            this.Initialize(RendererOptions.Empty);

            this.OnStarting(EventArgs.Empty);

            this.gameWindow.Closed += gameWindow_Closed;
            this.gameWindow.Run(60.0);
        }

        /// <summary>
        /// Stops the frame rendering process.
        /// </summary>
        public virtual void Stop()
        {
            if (!this.isRunning)
                return;

            this.OnStopping(EventArgs.Empty);

            this.isRunning = false;
        }

        /// <summary>
        /// Raises the <see cref="Renderer.Initializing"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnInitializing(RendererInitializationEventArgs e)
        {
            if (this.Initializing != null)
                this.Initializing(this, e);

            gameWindow.Title = "Gear.Client Sample Application";

            float[] mat_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] mat_shininess = { 50.0f };
            float[] light_position = { 100.0f, 100.0f, 100.0f, 0.0f };
            float[] light_ambient = { 0.1f, 0.1f, 0.1f, 1.0f };

            GL.ShadeModel(ShadingModel.Smooth);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, mat_specular);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, mat_shininess);
            GL.Light(LightName.Light0, LightParameter.Position, light_position);
            GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, mat_specular);


            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.CullFace);

            var mesh = Gear.Modeling.Mesh.NewCube();
            uint[] vboId = new uint[2];


            GL.GenBuffers(2, vboId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboId[0]);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboId[1]);

            var indices = new ushort[mesh.Triangles.Length];

            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof(ushort)), indices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboId[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(mesh.Vertices.Length * 8 * sizeof(float)), mesh.Vertices, BufferUsageHint.StaticDraw);


            //GL.Translate(0, 0, -0.25);
            //GL.Scale(0.5, 0.5, 0.5);
        }

        /// <summary>
        /// Raises the <see cref="Renderer.PostRender"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnPostRender(RenderEventArgs e)
        {
            if (this.PostRender != null)
                this.PostRender(this, e);

            ++this.frameCount;

            var tdelta = DateTime.Now - this.lastCheck;
            if (tdelta.Milliseconds > 500)
            {
                this.lastRate = this.currentRate;
                this.currentRate = this.frameCount / tdelta.TotalSeconds;
                this.lastCheck = DateTime.Now;
            }
        }

        /// <summary>
        /// Raises the <see cref="Renderer.PreRender"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnPreRender(RenderEventArgs e)
        {
            if (this.PreRender != null)
                this.PreRender(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Renderer.ProfileChanged"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnProfileChanged(EventArgs e)
        {
            if (this.ProfileChanged != null)
                this.ProfileChanged(this, e);

            //GL.Viewport(0, 0, this.Profile.Width, this.Profile.Height);
            //GL.MatrixMode(MatrixMode.Projection);

            //GLU.Perspective(45.0, (double)this.Profile.Width / (double)this.Profile.Height, 0.1, 100.0);
            //GL.MatrixMode(MatrixMode.ModelView);
            //GL.LoadIdentity();
        }

        /// <summary>
        /// Raises the <see cref="Renderer.Render"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnRender(RenderEventArgs e)
        {
            if (this.Render != null)
                this.Render(this, e);


            // render graphics
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);



            /*
            GL.Begin(BeginMode.Quads);
            // Front Face
            GL.Normal3(0.0f, 0.0f, 0.5f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
            // Back Face
            GL.Normal3(0.0f, 0.0f, -0.5f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
            // Top Face
            GL.Normal3(0.0f, 0.5f, 0.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
            // Bottom Face
            GL.Normal3(0.0f, -0.5f, 0.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            // Right Face
            GL.Normal3(0.5f, 0.0f, 0.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
            // Left Face
            GL.Normal3(-0.5f, 0.0f, 0.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.End();*/


            //GL.sp
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Scale(0.01, 0.01, 0.01);

            var ct = this.ActiveCamera.Position;

            GL.Translate(-ct.X, -ct.Y, -ct.Z);

            var rt = this.ActiveCamera.Orientation;

            this.ActiveCamera.Orientation = rt.RotateBy(1.5);
            GL.Rotate(rt.W, rt.X, rt.Y, rt.Z);

            this.ProcessNode(this.Scene.Root);

            gameWindow.SwapBuffers();
        }

        /// <summary>
        /// Raises the <see cref="Renderer.SceneChanged"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnSceneChanged(EventArgs e)
        {
            if (this.SceneChanged != null)
                this.SceneChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Renderer.Starting"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnStarting(EventArgs e)
        {
            if (this.Starting != null)
                this.Starting(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Renderer.Stopping"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnStopping(EventArgs e)
        {
            if (this.Stopping != null)
                this.Stopping(this, e);
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Scene != null);
            Contract.Invariant(this.ActiveCamera != null);
            Contract.Invariant(this.Profile != null);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
            this.isDisposed = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            //WGL.MakeCurrent(this.deviceContext, IntPtr.Zero);
            //WGL.DeleteContext(this.renderingContext);
        }

        void gameWindow_Closed(object sender, EventArgs e)
        {
            this.Stop();

            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }


        private void Idle()
        {

        }


        double r = 10.0;

        protected virtual void ProcessNode(Node node)
        {
            var axis = node.Orientation.GetAxis();
            GL.PushMatrix();
            GL.Rotate(node.Orientation.GetAngle().Degrees, axis.X, axis.Y, axis.Z);
            GL.Translate(node.Position.X, node.Position.Y, node.Position.Z);


            //if (node.Renderable != null)
            //foreach (var poly in node.Renderable.Faces)
            //{
                //GL.Begin(PrimitiveType.Polygon);
                //var n = poly.Normal;

                //GL.Normal3(n.X, n.Y, n.Z);

                //foreach (var vert in poly.Vertices.Reverse())
                 //   GL.Vertex3(vert.X, vert.Y, vert.Z);

                //GL.End();
            //}


            foreach (var child in node.Children)
            {
                this.ProcessNode(child);
            }
            GL.PopMatrix();

        }

        private void IdleFunc_Callback()
        {
            //GLUT.PostRedisplay();
            return;
        }

        private void DisplayFunc_Callback()
        {
            this.RenderFrame();
            return;
        }

        #endregion
    }
}
