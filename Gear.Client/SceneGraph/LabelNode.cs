﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gear.Geometry;
using System.Drawing.Imaging;
using System.Drawing;

namespace Gear.Client.SceneGraph
{
    /// <summary>
    /// Represents a node that contains a text label which is rendered directly on the framebuffer.
    /// </summary>
    public class LabelNode : SpriteNode
    {
        #region Fields
        private string text;
        private double fontSize;

        #endregion
        #region Constructors
        public LabelNode(string text)
        {
            this.fontSize = 12;
            this.text = text;

            this.RegenerateBitmap();
        }

        #endregion
        #region Properties
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.RegenerateBitmap();
            }
        }

        public double FontSize
        {
            get
            {
                return this.fontSize;
            }

            set
            {
                this.fontSize = value;
                this.RegenerateBitmap();
            }
        }

        public Vector4f TextColor
        {
            get;
            set;
        }

        #endregion
        #region Methods
        private unsafe void RegenerateBitmap()
        {
            var font = new Font("Consolas", (float)this.FontSize);
            var h = font.GetHeight(128f);
            var maxSize = this.text.Length * this.FontSize;

            var bmp = new Bitmap((int)maxSize, (int)h);
            var g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawString(this.text, font, new SolidBrush(Color.FromArgb(255, (byte)(this.TextColor.X * 255), (byte)(this.TextColor.Y * 255), (byte)(this.TextColor.Z * 255))), 0, 0);

            this.LoadBitmap(bmp);
        }
        #endregion
    }
}
