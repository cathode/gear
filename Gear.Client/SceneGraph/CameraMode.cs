/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Gear.Client.SceneGraph
{
    /// <summary>
    /// Enumerates supported camera view modes.
    /// </summary>
    public enum CameraMode
    {
        /// <summary>
        /// Indicates that the camera provides a perspective view of the scene.
        /// </summary>
        Perspective = 0x0,

        /// <summary>
        /// Indicates that the camera provides an orthographic view of the scene.
        /// </summary>
        Orthographic = 0x1,
    }
}
