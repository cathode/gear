﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using Gear.Geometry;

namespace Gear.Modeling
{
    /// <summary>
    /// Represents an edge of a polygon.
    /// </summary>
    public class Edge3
    {
        #region Fields
        private EdgeFlags flags;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Edge3"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public Edge3(Vertex3 p, Vertex3 q)
        {
            Contract.Requires(p != null);
            Contract.Requires(q != null);

            this.P = p;
            this.Q = q;
        }
        public Edge3(Vertex3[] vertices, int p, int q)
        {
            Contract.Requires(vertices != null);
            Contract.Requires(p < vertices.Length);
            Contract.Requires(q < vertices.Length);
            Contract.Requires(vertices[p] != null);
            Contract.Requires(vertices[q] != null);

            this.P = vertices[p];
            this.Q = vertices[q];
        }
        #endregion
        #region Properties
        public Vertex3 P
        {
            get;
            set;
        }
        public Vertex3 Q
        {
            get;
            set;
        }

        public Polygon3 Left
        {
            get;
            set;
        }

        public Polygon3 Right
        {
            get;
            set;
        }

        public EdgeFlags Flags
        {
            get
            {
                return this.flags;
            }
            set
            {
                this.flags = value;
            }
        }
        #endregion
        #region Methods
        public Vector3 GetMidpoint()
        {
            return (this.P.Position + this.Q.Position) / 2.0;
        }

        /// <summary>
        /// Calculates the point at which the edge intersects the XY plane.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetIntersectionXY()
        {
            return this.GetIntersectionXY(0.0);
        }

        /// <summary>
        /// Calculates the point at which the edge intersects the XY plane.
        /// </summary>
        /// <param name="z">The offset of the XY plane along the Z axis.</param>
        /// <returns></returns>
        public Vector3 GetIntersectionXY(double z)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the point at which the edge intersects the XY plane.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetIntersectionYZ()
        {
            return this.GetIntersectionYZ(0.0);
        }

        /// <summary>
        /// Calculates the point at which the edge intersects the YZ plane.
        /// </summary>
        /// <param name="x">The offset of the YZ plane along the X axis.</param>
        /// <returns></returns>
        public Vector3 GetIntersectionYZ(double x)
        {
            var x1 = this.P.X;
            var x2 = this.Q.X;
            var y1 = this.P.Y;
            var y2 = this.Q.Y;
            var z1 = this.P.Z;
            var z2 = this.P.Z;

            var k1 = -x2 / (x1 - x2);
            var k2 = 1 - k1;
            var xf = 1.0 / (x2 - x1);

            //var xdist = 
            //k1 *= x;
            //k2 *= 1.0 - x;
            var isect = ((k1 * this.P.ToVector3()) + (this.Q.ToVector3() * k2)) * xf;

            return isect;
        }

        /// <summary>
        /// Calculates the edge's intersection with the ZX plane.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetIntersectionZX()
        {
            return this.GetIntersectionZX(0.0);
        }

        /// <summary>
        /// Calculates the point at which the edge intersects the ZX plane.
        /// </summary>
        /// <param name="y">The offset of the ZX plane along the Y axis.</param>
        /// <returns></returns>
        public Vector3 GetIntersectionZX(double y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the length of the edge.
        /// </summary>
        /// <returns></returns>
        public double GetLength()
        {
            // Calculate deltas
            var xd = Math.Abs(this.P.X - this.Q.X);
            var yd = Math.Abs(this.P.Y - this.Q.Y);
            var zd = Math.Abs(this.P.Z - this.Q.Z);

            // Pythagorean forumla in 3 dimensions
            var dist = Math.Sqrt(xd * xd + yd * yd + zd * zd);

            return dist;
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.P != null);
            Contract.Invariant(this.Q != null);
        }
        #endregion

    }
}
