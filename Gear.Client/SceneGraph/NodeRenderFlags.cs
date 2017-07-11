/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;

namespace Gear.Client.SceneGraph
{
    /// <summary>
    /// Represents flags that control how (or if) a scene node is rendered.
    /// </summary>
    [Flags]
    public enum NodeRenderFlags
    {
        /// <summary>
        /// The node will not be rendered.
        /// </summary>
        None = 0x0,
        /// <summary>
        /// The node is visible and can be rendered.
        /// </summary>
        Visible = 0x1,
        /// <summary>
        /// Edges of the object are highlighted.
        /// </summary>
        EdgeHighlight = 0x2,
        /// <summary>
        /// Vertices of the object are highlighted.
        /// </summary>
        VertexHighlight = 0x4,
    }
}
