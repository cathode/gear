/******************************************************************************
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

namespace Gear.Client.SceneGraph
{
    /// <summary>
    /// Indicates the order that camera transformations are applied to the view matrix.
    /// </summary>
    public enum TransformOrder
    {
        /// <summary>
        /// Rotation, then translation, then scale.
        /// </summary>
        RotateTranslateScale,
        /// <summary>
        /// Translation, then rotation, then scale.
        /// </summary>
        TranslateRotateScale,
    }
}
