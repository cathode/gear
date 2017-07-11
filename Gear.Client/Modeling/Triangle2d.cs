/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Gear.Geometry;

namespace Gear.Modeling
{
    /// <summary>
    /// Represents a triangle in two-dimensional space.
    /// </summary>
    public class Triangle2d : Polygon2d
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle2d"/> class.
        /// </summary>
        public Triangle2d()
            : base(3)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle2d"/> class.
        /// </summary>
        /// <param name="radius"></param>
        public Triangle2d(double radius)
            : base(3, radius)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the <see cref="Triangle2d"/> class.
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="mode"></param>
        public Triangle2d(double radius, RadiusMode mode)
            : base(3, radius, mode)
        {
        }
        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the first vertex.
        /// </summary>
        public Vertex2d A
        {
            get
            {
                return this[0];
            }

            set
            {
                this[0] = value;
            }
        }

        /// <summary>
        /// Gets or sets the second vertex.
        /// </summary>
        public Vertex2d B
        {
            get
            {
                return this[1];
            }

            set
            {
                this[1] = value;
            }
        }

        /// <summary>
        /// Gets or sets the third vertex.
        /// </summary>
        public Vertex2d C
        {
            get
            {
                return this[2];
            }

            set
            {
                this[2] = value;
            }
        }
        #endregion
    }
}
