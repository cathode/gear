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

namespace Gear.Client.SceneGraph
{
    /// <summary>
    /// Enumerates spaces that objects are referenced against.
    /// </summary>
    public enum ReferenceSpace
    {
        /// <summary>
        /// Indicates the geometry of the node is relative to it's own object space.
        /// </summary>
        Object = 0x00,

        /// <summary>
        /// Indicates the geometry of the node has already been transformed into view space, or does not need to be transformed to view space.
        /// </summary>
        View = 0x01,

        /// <summary>
        /// Indicates the geometry of the node has already been transformed to world space, or does not need to be transformed further.
        /// </summary>
        World = 0x02,

        /// <summary>
        /// Indicates the geometry of the node has already been transformed to projection space or does not need to be transformed further.
        /// </summary>
        Projection = 0x04,
    }
}
