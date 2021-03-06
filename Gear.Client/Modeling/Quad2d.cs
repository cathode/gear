﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using Gear.Geometry;

namespace Gear.Modeling
{
    /// <summary>
    /// Represents a quadrilateral polygon in two-dimensional space.
    /// </summary>
    public class Quad2d : Polygon2d
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Quad2d"/> class.
        /// </summary>
        public Quad2d()
            : base(4)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quad2d"/> class.
        /// </summary>
        /// <param name="size">The width and height of the new quad.</param>
        public Quad2d(double size)
            : this(size, size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quad2d"/> class.
        /// </summary>
        /// <param name="width">The width of the quad.</param>
        /// <param name="height">The height of the quad.</param>
        public Quad2d(double width, double height)
            : base(4)
        {
            var w = width / 2.0;
            var h = height / 2.0;
            var xa = -w;
            var xb = w;
            var ya = -h;
            var yb = h;            this[0] = new Vertex2d(xa, ya);
            this[1] = new Vertex2d(xb, ya);
            this[2] = new Vertex2d(xb, yb);
            this[3] = new Vertex2d(xa, yb);
        }
        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the upper-left vertex of the quad.
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
        /// Gets or sets the upper-right vertex of the quad.
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
        /// Gets or sets the lower-right vertex of the quad.
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

        /// <summary>
        /// Gets or sets the lower-left vertex of the quad.
        /// </summary>
        public Vertex2d D
        {
            get
            {
                return this[3];
            }

            set
            {
                this[3] = value;
            }
        }
        #endregion
    }
}
