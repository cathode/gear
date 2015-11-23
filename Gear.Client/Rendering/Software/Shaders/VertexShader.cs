/******************************************************************************
 * Gear.Client: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gear.Client.Geometry;

namespace Gear.Client.Rendering.Software.Shaders
{
    /// <summary>
    /// Represents a vertex shader.
    /// </summary>
    public class VertexShader
    {
        #region Methods
        public virtual Vertex3 Shade(Vertex3 v)
        {
            return v;
        }
        #endregion
    }
}
