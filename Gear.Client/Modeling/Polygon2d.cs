﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Gear.Geometry;

namespace Gear.Modeling
{
    /// <summary>
    /// Represents a polygon in two-dimensional space.
    /// </summary>
    public class Polygon2d : IEnumerable<Vertex2d>
    {
        #region Fields

        /// <summary>
        /// Holds the actual vertices of the polygon.
        /// </summary>
        private readonly Vertex2d[] vertices;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon2d"/> class.
        /// </summary>
        /// <param name="vertices">The number of sides of the new polygon.</param>
        public Polygon2d(int vertices)
        {
            Contract.Requires(vertices > 2);

            this.vertices = new Vertex2d[vertices];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon2d"/> class.
        /// </summary>
        /// <param name="sides">The number of sides of the new polygon.</param>
        /// <param name="radius">The radius of the new polygon.</param>
        public Polygon2d(int sides, double radius)
            : this(sides, radius, RadiusMode.Vertex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon2d"/> class.
        /// </summary>
        /// <remarks>
        /// Creates a regular polygon.
        /// </remarks>
        /// <param name="sides">The number of sides of the new polygon.</param>
        /// <param name="radius">The radius of the new polygon.</param>
        /// <param name="mode">The <see cref="RadiusMode"/> that indicates how the <paramref name="radius"/> parameter is interpreted.</param>
        public Polygon2d(int sides, double radius, RadiusMode mode)
        {
            this.vertices = new Vertex2d[sides];
            for (int s = 0; s < sides; s++)
            {
                double a = ((2 * Math.PI) / sides) * s;
                this[s] = new Vertex2d(Math.Sin(a) * radius, Math.Cos(a) * radius);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon2d"/> class.
        /// </summary>
        /// <param name="vertices"></param>
        public Polygon2d(params Vertex2d[] vertices)
        {
            if (vertices.Length < 3)
            {
                throw new ArgumentException("Polygons must contain at least 3 vertices.", "vertices");
            }

            this.vertices = vertices;
        }
        #endregion
        #region Properties

        /// <summary>
        /// Gets the number of sides of the current polygon.
        /// </summary>
        public int Sides
        {
            get
            {
                return this.vertices.Length;
            }
        }
        #endregion
        #region Indexers

        /// <summary>
        /// Gets or sets a <see cref="Vector2d"/> representing the vertex with the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Vertex2d this[int index]
        {
            get
            {
                return this.vertices[index];
            }

            set
            {
                this.vertices[index] = value;
            }
        }
        #endregion
        #region Methods

        /// <summary>
        /// Gets the enumerator for the current instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Vertex2d> GetEnumerator()
        {
            for (int i = 0; i < this.vertices.Length; i++)
            {
                yield return this.vertices[i];
            }
        }

        /// <summary>
        /// Gets the enumerator for the current instance.
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
