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
using System.Threading.Tasks;
using Gear.Client.Platform;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics.Contracts;


namespace Gear.Client.Rendering
{
    public class ManagedRendererBitmapTarget : IManagedRendererTarget
    {
        #region Fields
        private Bitmap output;
        #endregion
        #region Constructors
        public ManagedRendererBitmapTarget()
        {
            this.UpdateDisplayProfile(DisplayProfile.Default);
        }
        #endregion
        #region Properties
        public Bitmap Output
        {
            get
            {
                return this.output;
            }
        }
        #endregion
        #region Methods
        public void UpdateDisplayProfile(DisplayProfile profile)
        {
            if (this.output != null)
                this.output.Dispose();

            this.output = new Bitmap(profile.Width, profile.Height, PixelFormat.Format32bppPArgb);
        }

        public void ConsumeFrameBuffer(ManagedBuffer buffer)
        {
            buffer.Color.WriteToBitmap(this.output);
        }
        #endregion
    }
}
