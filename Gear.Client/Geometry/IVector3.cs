/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;

namespace Gear.Geometry
{
    /// <summary>
    /// A three-dimensional double-precision floating point vector.
    /// </summary>
    [Obsolete]
    public interface Vector3 : Gear.Geometry.Vector2
    {
        #region Properties
        /// <summary>
        /// Gets the z-component of the vector.
        /// </summary>
        double Z
        {
            get;
        }
        #endregion
        #region Methods
        Vector3 ToVector3();
        #endregion
    }
}
