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

namespace Gear.Client.Rendering
{
    /// <summary>
    /// Enumerates general styles of displaying 3D objects.
    /// </summary>
    public enum RenderStyle
    {
        /// <summary>
        /// Indicates that edges of faces are rendered explicitly.
        /// </summary>
        Wireframe = 0,

        /// <summary>
        /// Indicates that faces of objects are rendered using a solid color filling algorithm.
        /// </summary>
        Solid = 1,

        /// <summary>
        /// Indicates that faces of objects are rendered using a smooth shading algorithm.
        /// </summary>
        Smooth = 2,

        /// <summary>
        /// Indicates that faces of objects are rendered using an image-based material or texture.
        /// </summary>
        Textured = 3,
    }
}
