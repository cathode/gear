/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.Windows;
using Buffer = SlimDX.Direct3D11.Buffer;
using Device = SlimDX.Direct3D11.Device;
using Resource = SlimDX.Direct3D11.Resource;
using Rust.Assets;

namespace Rust
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Hardcoded engine set-up.
            //Engine.RegisterGamePluginSearchPath("./plugins/");
            //GamePluginManager.RegisterGamePluginSearchPath("./");

            //var ids = GamePluginManager.ScanForGamePlugins();

            //var launcherUI = new LauncherUI();
            //Application.EnableVisualStyles();
            //Application.Run(launcherUI);

            //int adapterOrdinal = SlimDX.DXGI.

            //Engine.Tick += new EventHandler(Engine_Tick);
            //Engine.Start();

            //GameEngine engine = new GameEngine();
            //engine.Run();

            Console.WriteLine("Compiling SamplePackage.xml...");
            var compiler = new PackageCompiler();
            compiler.Compile("SamplePackage.xml", "Sample.package");

            Console.WriteLine("Compilation done.");

            Console.WriteLine("Verifying Package...");
            var package = Package.Open("Sample.package");

            Console.WriteLine("Verification done.");
            Console.Read();
        }

        static void Engine_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class GameEngine
    {
        #region Fields
        private RenderForm form;
        private DeviceContext context;
        private RenderTargetView renderTarget;
        private Device device;
        private SwapChain swapChain;
        #endregion
        #region Constructors
        public GameEngine()
        {
            this.form = new RenderForm("Battle Isle: Memories");
        }
        #endregion
        public void Run()
        {
            var description = new SwapChainDescription()
            {
                BufferCount = 1,
                Usage = Usage.RenderTargetOutput,
                OutputHandle = form.Handle,
                IsWindowed = true,
                ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                SampleDescription = new SampleDescription(1, 0),
                Flags = SwapChainFlags.AllowModeSwitch,
                SwapEffect = SwapEffect.Discard
            };

            Device.CreateWithSwapChain(SlimDX.Direct3D11.DriverType.Hardware, SlimDX.Direct3D11.DeviceCreationFlags.None, description, out device, out swapChain);

            this.context = device.ImmediateContext;
            using (var factory = swapChain.GetParent<Factory>())
                factory.SetWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAltEnter);
            form.KeyDown += (o, e) =>
            {
                if (e.Alt && e.KeyCode == System.Windows.Forms.Keys.Enter)
                {
                    bool state;
                    Output output;
                    swapChain.GetFullScreenState(out state, out output);
                    swapChain.SetFullScreenState(!state, null);
                }
            };

            var viewport = new Viewport(0.0f, 0.0f, form.ClientSize.Width, form.ClientSize.Height);
            context.Rasterizer.SetViewports(viewport);

            using (var resource = Resource.FromSwapChain<Texture2D>(swapChain, 0))
                this.renderTarget = new RenderTargetView(device, resource);
            context.OutputMerger.SetTargets(renderTarget);

            MessagePump.Run(this.form, this.RenderFrame);
        }
        public void RenderFrame()
        {
            this.context.ClearRenderTargetView(this.renderTarget, new Color4(0.219607843f, 0.219607843f, 0.219607843f));
            var vertices = this.CellGeometry(0.5f);

            var vertexBuffer = new Buffer(this.device, vertices, 12 * 6, ResourceUsage.Default,
                BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            ShaderSignature inputSignature;
            VertexShader vertexShader;
            using (var bytecode = ShaderBytecode.CompileFromFile("./Shaders/sample.fx", "VShader", "vs_4_0", ShaderFlags.None, EffectFlags.None))
            {
                inputSignature = ShaderSignature.GetInputSignature(bytecode);
                vertexShader = new VertexShader(device, bytecode);
            }

            PixelShader pixelShader;
            using (var bytecode = ShaderBytecode.CompileFromFile("./Shaders/sample.fx", "PShader", "ps_4_0", ShaderFlags.None, EffectFlags.None))
                pixelShader = new PixelShader(device, bytecode);

            var elements = new[] { new InputElement("POSITION", 0, Format.R32G32B32_Float, 0) };
            var layout = new InputLayout(device, inputSignature, elements);
            context.InputAssembler.InputLayout = layout;
            context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;
            context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, 12, 0));
            context.VertexShader.Set(vertexShader);
            context.PixelShader.Set(pixelShader);

            context.Draw(6, 0);

            this.swapChain.Present(0, PresentFlags.None);
        }
        private DataStream CellGeometry(float z)
        {
            var vertices = new DataStream(12 * 6, true, true);

            vertices.Write(new Vector3(-0.25f, 0.5f, z));
            vertices.Write(new Vector3(0.25f, 0.5f, z));
            vertices.Write(new Vector3(-0.5f, 0.0f, z));
            vertices.Write(new Vector3(0.5f, 0.0f, z));
            vertices.Write(new Vector3(-0.25f, -0.5f, z));
            vertices.Write(new Vector3(0.25f, -0.5f, z));
            vertices.Position = 0;
            return vertices;
        }
    }
}
