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

namespace Gear.Modeling
{
    /// <summary>
    /// Represents a bounding volume, in other words an axis-aligned rectangular volume.
    /// </summary>
    public class BoundingVolume
    {
        #region Properties
        /// <summary>
        /// Gets or sets the width (measurement on the X-axis) of the volume.
        /// </summary>
        public double Width
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the length (measurement on the Y-axis) of the volume.
        /// </summary>
        public double Length
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height (measurement on the Z-axis) of the volume.
        /// </summary>
        public double Height
        {
            get;
            set;
        }

        public double X
        {
            get;
            set;
        }

        public double Y
        {
            get;
            set;
        }

        public double Z
        {
            get;
            set;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Determines if the specified point is contained by the bounding volume.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(Vector3 point)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
