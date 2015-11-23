﻿/******************************************************************************
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
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics.Contracts;

namespace Gear.Client.Rendering
{
    public sealed class ManagedRendererHostForm : Form, IManagedRendererTarget
    {
        #region Fields
        private readonly ManagedRenderer renderer;
        private ManagedRendererHostControl hostControl;
        private DateTime lastCheck;
        private int framesRendered;
        private double lastFps;
        #endregion
        #region Constructors
        public ManagedRendererHostForm(ManagedRenderer renderer)
        {
            Contract.Requires(renderer != null);

            this.renderer = renderer;
            this.renderer.PostRender += new EventHandler<RenderEventArgs>(renderer_PostRender);
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            this.hostControl = new ManagedRendererHostControl(this.renderer);
            this.hostControl.scene = renderer.Scene;
            this.Controls.Add(hostControl);
        }
        #endregion

        void renderer_PostRender(object sender, RenderEventArgs e)
        {
            this.framesRendered++;

            var elapsed = DateTime.Now - this.lastCheck;
            if (elapsed.TotalMilliseconds > 500)
            {
                this.lastFps = framesRendered / elapsed.TotalSeconds;
                this.lastCheck = DateTime.Now;
                this.framesRendered = 0;

                try
                {
                    this.Invoke(new Action(this.UpdateText));
                }
                catch
                {
                }
            }
        }

        void UpdateText()
        {
            this.Text = string.Format("{0} frames/second", this.lastFps.ToString("F3"));
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            this.renderer.Stop();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            new Thread((ThreadStart)delegate
            {
                renderer.AttachTarget(this);
                renderer.Start();
            }).Start();

            this.hostControl.Invalidate();
        }
        public string Title
        {
            get;
            set;
        }

        public void UpdateDisplayProfile(Platform.DisplayProfile profile)
        {
            Contract.Requires(profile != null);

            if (!this.IsDisposed && !this.hostControl.IsDisposed)
            {
                this.hostControl.Invoke((Action)delegate
                {
                    this.hostControl.UpdateDisplayProfile(profile);
                });
            }
        }

        public void ConsumeFrameBuffer(ManagedBuffer buffer)
        {
            Contract.Requires(buffer != null);

            if (!this.IsDisposed && !this.hostControl.IsDisposed)
            {
                try
                {
                    this.hostControl.Invoke((Action)delegate
                    {
                        if (!this.IsDisposed && !this.hostControl.IsDisposed)
                            this.hostControl.ConsumeFrameBuffer(buffer);
                    });
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Invariant contracts for the <see cref="ManagedRendererHostForm"/> class.
        /// </summary>
        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.renderer != null);
            Contract.Invariant(this.hostControl != null);
        }
    }
}
