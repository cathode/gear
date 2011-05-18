/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX.Direct3D11;
using System.Runtime.InteropServices;
using SlimDX.DXGI;
using SlimDX;
using Device = SlimDX.Direct3D11.Device;
using Resource = SlimDX.Direct3D11.Resource;
using Buffer = SlimDX.Direct3D11.Buffer;
using System.Windows.Forms;
using SlimDX.Windows;

namespace Gear
{
    public sealed class ClientEngine : EngineBase
    {
        #region Fields
        private RenderForm form;
        private Device device;
        private SwapChain swapChain;
        #endregion
        #region Constructors

        #endregion
        #region Methods
        public override void Run()
        {
            if (!this.IsInitialized)
                this.Initialize();

            Application.Run(form);
        }
        protected override void OnInitializing(EventArgs e)
        {
            base.OnInitializing(e);
            
            Log.Write("Initializing DirectX");

            this.form = new SlimDX.Windows.RenderForm();

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

            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, description, out this.device, out this.swapChain);

            var context = this.device.ImmediateContext;

            // prevent DXGI handling of alt+enter, which doesn't work properly with Winforms
            using (var factory = this.swapChain.GetParent<Factory>())
                factory.SetWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAltEnter);

            // TODO: handle alt+enter fullscreen

            var viewport = new Viewport(0.0f, 0.0f, form.ClientSize.Width, form.ClientSize.Height);
            context.Rasterizer.SetViewports(viewport);

            RenderTargetView renderTarget;
            using (var resource = Resource.FromSwapChain<Texture2D>(this.swapChain, 0))
                renderTarget = new RenderTargetView(this.device, resource);

            context.OutputMerger.SetTargets(renderTarget);
        }
        #endregion

        [StructLayout(LayoutKind.Sequential)]
        internal struct Message
        {
            public IntPtr hWnd;
            public uint msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point p;
        }

        internal static class NativeMethods
        {
            [System.Security.SuppressUnmanagedCodeSecurity] // We won't use this maliciously
            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            public static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);
        }
    }
}
