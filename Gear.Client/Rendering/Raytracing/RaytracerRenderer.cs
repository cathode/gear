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
                    Ray3d ray = new Ray3d()
                    {
                        Origin = new Vector3d(x, y, double.NegativeInfinity),
                        Normal = new Vector3d(0.0, 0.0, 1.0),
                    };
                    buffer[x, y] = this.TraceRay(ray);
                }
        }

        protected virtual Vector3d ClosestIntersection(Ray3d ray)
        {
            throw new NotImplementedException();
        }

        protected virtual Vector3d ScreenToWorld(int x, int y)
        {
            throw new NotImplementedException();
        }

        protected virtual Vector4f TraceRay(Ray3d ray)
        {
            return this.BackgroundColor;
        }
        #endregion
    }
}
