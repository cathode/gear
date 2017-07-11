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

namespace Gear.Modeling.Primitives
{
    /// <summary>
    /// Represents a cube primitive in 3d space. This class is immutable.
    /// </summary>
    public sealed class Cube : Mesh3
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Cube"/> class using the default size.
        /// </summary>
        public Cube()
            : this(Cube.DefaultSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cube"/> class using the specified size.
        /// </summary>
        /// <param name="size">The size of the new <see cref="Cube"/>.</param>
        public Cube(double size)
        {
            this.size = size;

            var r = size * 0.5;
            var verts = new Vertex3d[8];
            verts[0] = new Vertex3d(r, r, r);
            verts[1] = new Vertex3d(r, -r, r);
            verts[2] = new Vertex3d(-r, -r, r);
            verts[3] = new Vertex3d(-r, r, r);

            verts[4] = new Vertex3d(r, r, -r);
            verts[5] = new Vertex3d(r, -r, -r);
            verts[6] = new Vertex3d(-r, -r, -r);
            verts[7] = new Vertex3d(-r, r, -r);

            var edges = new Edge3[]
            {
                new Edge3(verts, 0, 1),
                new Edge3(verts, 1, 2),
                new Edge3(verts, 2, 3),
                new Edge3(verts, 3, 0),

                new Edge3(verts, 4, 5),
                new Edge3(verts, 5, 6),
                new Edge3(verts, 6, 7),
                new Edge3(verts, 7, 4),

                new Edge3(verts, 0, 6),
                new Edge3(verts, 1, 5),
                new Edge3(verts, 2, 4),
                new Edge3(verts, 3, 7),
            };

            this.Polygons = new Quad3d[] {
                // Top and bottom
                new Quad3d(edges, 0, 1, 2, 3),
                new Quad3d(edges, 4, 5, 6, 7),
                // new Quad3(edges,
                new Quad3d(verts, 0, 3, 7, 4),
                new Quad3d(verts, 1, 0, 4, 5),
                new Quad3d(verts, 2, 1, 5, 6),
                new Quad3d(verts, 3, 2, 6, 7),
            };
        }

        #endregion
        #region Fields
        private double size;

        /// <summary>
        /// Holds the default size of a new <see cref="Cube"/>, if none is specified when the object is created.
        /// </summary>
        public const double DefaultSize = 1.0;
        #endregion
        #region Properties

        /// <summary>
        /// Gets the size of the current <see cref="Cube"/>.
        /// </summary>
        public double Size
        {
            get
            {
                return this.size;
            }
        }
        #endregion
    }
}
