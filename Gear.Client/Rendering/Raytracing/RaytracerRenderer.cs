/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using Gear.Geometry;
using Gear.Client.SceneGraph;

namespace Gear.Client.Rendering.Raytraced
{
    public class RaytracedRenderer : ManagedRenderer
    {
        #region Fields
        private ManagedBuffer fb;
        #endregion
        #region Constructors
        public RaytracedRenderer()
        {
            this.fb = new ManagedBuffer(512, 512);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the maximum depth of ray tracing calculations.
        /// </summary>
        public int TraceDepth
        {
            get;
            set;
        }
        public ManagedBuffer FrameBuffer
        {
            get
            {
                return this.fb;
            }
        }
        #endregion
        #region Methods
        protected override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);

            var buffer = this.FrameBuffer.Color;
            for (int y = 0; y < this.fb.Height; y++)
                for (int x = 0; x < this.fb.Width; x++)
                {
                    Ray3 ray = new Ray3()
                    {
                        Origin = new Vector3(x, y, double.NegativeInfinity),
                        Normal = new Vector3(0.0, 0.0, 1.0),
                    };
                    buffer[x, y] = this.TraceRay(ray);
                }
        }

        protected virtual Vector3 ClosestIntersection(Ray3 ray)
        {
            throw new NotImplementedException();
        }

        protected virtual Vector3 ScreenToWorld(int x, int y)
        {
            throw new NotImplementedException();
        }

        protected virtual Vector4f TraceRay(Ray3 ray)
        {
            return this.BackgroundColor;
        }
        #endregion
    }
}
