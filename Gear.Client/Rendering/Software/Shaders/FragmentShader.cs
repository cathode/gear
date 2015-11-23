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

namespace Gear.Client.Rendering.Software.Shaders
{
    /// <summary>
    /// Represents the basic functionality shared by fragment (aka pixel) shaders for the software renderer.
    /// </summary>
    public abstract class FragmentShader
    {
        #region Methods
        protected abstract Fragment Shade(Fragment input);
        #endregion
        #region Types
        /// <summary>
        /// Implements a basic fragment shader.
        /// </summary>
        public sealed class DefaultShader : FragmentShader
        {
            protected override Fragment Shade(Fragment input)
            {
                var frag = new Fragment();
                frag.Position = input.Position;
                frag.Color = new Geometry.Vector4f(1.0f, 0f, 0f, 1.0f);

                return frag;
            }
        }
        #endregion
    }
}
