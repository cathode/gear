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
using Gear.Geometry;

namespace Gear.Client.Rendering.Software.Shaders
{
    /// <summary>
    /// Represents a vertex shader.
    /// </summary>
    public class VertexShader
    {
        #region Methods
        public virtual Vertex3d Shade(Vertex3d v)
        {
            return v;
        }
        #endregion
    }
}
