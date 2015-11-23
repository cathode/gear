/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;

namespace Gear.Client.Geometry
{
    /// <summary>
    /// A two-dimensional double-precision floating point vector.
    /// </summary>
    public interface Vector2
    {
        #region Properties
        /// <summary>
        /// Gets the x-component of the vector.
        /// </summary>
        double X
        {
            get;
        }

        /// <summary>
        /// Gets the y-component of the vector.
        /// </summary>
        double Y
        {
            get;
        }
        #endregion
        #region Methods
        Vector2 ToVector2();
        #endregion
    }
}
