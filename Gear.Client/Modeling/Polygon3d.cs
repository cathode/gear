﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics.Contracts;
using Gear.Geometry;

namespace Gear.Modeling
{
    /// <summary>
    /// Represents a polygon in three-dimensional space. A polygon is a shape made up of vertices.
    /// </summary>
    public class Polygon3d : IEnumerable<Vertex3d>, IRenderableFace
    {
        #region Fields

        /// <summary>
        /// Holds the vertices of the current <see cref="Polygon3d"/>.
        /// </summary>
        private readonly Vertex3d[] vertices;
        protected readonly Edge3[] edges;

        private int vertexCount;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon3d"/> class.
        /// </summary>
        /// <param name="vertexCount">The number of vertices of the new polygon.</param>
        public Polygon3d(int vertexCount)
        {
            Contract.Requires(vertexCount > 2);

            Contract.Ensures(this.Vertices.Length == vertexCount);

            this.vertices = new Vertex3d[vertexCount];

            this.vertexCount = this.vertices.Length;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon3d"/> class.
        /// </summary>
        /// <param name="vertexCount">The number of sides of the new polygon.</param>
        /// <param name="radius">The radius of the new polygon.</param>
        /// <remarks>
        /// Assumes <see cref="RadiusMode.Vertex"/>.
        /// </remarks>
        public Polygon3d(int vertexCount, double radius)
            : this(vertexCount, radius, RadiusMode.Vertex)
        {
            Contract.Requires(vertexCount > 2);
            Contract.Ensures(this.Vertices.Length == vertexCount);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon3d"/> class as a regular polygon with the specified radius.
        /// </summary>
        /// <param name="vertexCount">The number of sides of the new polygon.</param>
        /// <param name="radius">The radius of the new polygon.</param>
        /// <param name="mode">The <see cref="RadiusMode"/> that describes how the radius value is interpreted.</param>
        public Polygon3d(int vertexCount, double radius, RadiusMode mode)
        {
            Contract.Requires(vertexCount > 2);
            Contract.Ensures(this.Vertices.Length == vertexCount);

            var m = Matrix4.CreateRotationMatrix(new Vector3d(0, 1, 0), Angle.FromDegrees(360.0 / vertexCount));

            Vector3d v = new Vector3d(radius, 0, 0);

            if (mode == RadiusMode.Edge)
            {
                var rm = Matrix4.CreateRotationMatrix(new Vector3d(0, 1, 0), Angle.FromDegrees(360.0 / (vertexCount * 2)));
                v = rm * v;
            }

            this.vertices = new Vertex3d[vertexCount];
            this.edges = new Edge3[vertexCount];

            for (int i = vertexCount - 1; i >= 0; i--)
            {
                this.vertices[i] = new Vertex3d(v.X, v.Y, v.Z);
                v = m * v;
            }

            this.RegenerateEdges();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon3d"/> class from the specified vertices.
        /// </summary>
        /// <param name="vertices">A collection of vertices to use for the new polygon.</param>
        public Polygon3d(params Vertex3d[] vertices)
        {
            Contract.Requires(vertices != null);
            Contract.Requires(vertices.Length > 2);
            Contract.Ensures(this.Vertices.Length == vertices.Length);

            this.vertices = vertices;
            this.edges = new Edge3[vertices.Length];
            this.RegenerateEdges();
        }

        public Polygon3d(Vertex3d[] verts, params int[] indices)
        {
            Contract.Requires(verts != null);
            Contract.Requires(indices != null);
            Contract.Requires(indices.Length > 2);
            Contract.Requires(indices.All(i => i < verts.Length && i >= 0));
            Contract.Ensures(this.Vertices.Length == indices.Length);

            this.vertices = new Vertex3d[indices.Length];
            this.edges = new Edge3[indices.Length];

            for (int i = 0; i < indices.Length; ++i)
            {
                if (indices[i] < verts.Length)
                {
                    this.vertices[i] = verts[indices[i]];
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            this.RegenerateEdges();
        }

        public Polygon3d(Edge3[] edges, params int[] indices)
        {
            Contract.Requires(edges != null);
            Contract.Requires(edges.Length > 2);
            Contract.Requires(indices != null);
            Contract.Requires(indices.Length > 2);
            Contract.Requires(indices.All(i => i < edges.Length && i >= 0));
            Contract.Ensures(this.Vertices.Length == indices.Length);

            this.vertices = new Vertex3d[indices.Length];
            this.edges = new Edge3[indices.Length];

            for (int i = 0; i < indices.Length; ++i)
            {
                this.edges[i] = edges[indices[i]];
                this.vertices[i] = this.edges[i].P;
            }
        }
        #endregion
        #region Properties

        /// <summary>
        /// Gets a value indicating whether the vertices of the current <see cref="Polygon3d"/> lie on the same plane in 3d-space.
        /// </summary>
        public virtual bool IsPlanar
        {
            get
            {
                if (this.Vertices.Length == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="PrimitiveKind"/> of the current <see cref="Polygon3d"/>.
        /// </summary>
        public virtual PrimitiveKind Kind
        {
            get
            {
                return PrimitiveKind.Polygon;
            }
        }

        /// <summary>
        /// Gets the surface normal of the current <see cref="Polygon3d"/>.
        /// </summary>
        public virtual Vector3d Normal
        {
            get
            {
                return Vector3d.CrossProduct((Vector3d)this.vertices[1] - (Vector3d)this.vertices[0], (Vector3d)this.vertices[2] - (Vector3d)this.vertices[0]).Normalize();
            }
        }

        /// <summary>
        /// Gets an array containing the vertices of the polygon.
        /// </summary>
        public Vertex3d[] Vertices
        {
            get
            {
                return this.vertices;
            }
        }

        /// <summary>
        /// Gets an array containing the edges of the polygon.
        /// </summary>
        public Edge3[] Edges
        {
            get
            {
                return this.edges;
            }
        }

        public int VertexCount
        {
            get
            {
                return this.vertices.Length;
            }
        }

        public uint A
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public uint B
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public uint C
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #region Indexers

        /// <summary>
        /// Gets or sets the <see cref="Vertex3d"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="Vertex3d"/> to access.</param>
        /// <returns>The <see cref="Vertex3d"/> at the specified index.</returns>
        public Vertex3d this[int index]
        {
            get
            {
                Contract.Requires(index >= 0);
                Contract.Requires(index < this.VertexCount);
                Contract.Ensures(Contract.Result<Vertex3d>() != null);

                return this.vertices[index];
            }

            set
            {
                Contract.Requires(index >= 0);
                Contract.Requires(index < this.VertexCount);
                Contract.Requires(value != null);

                this.vertices[index] = value;
            }
        }

        #endregion
        #region Methods
        public void RegenerateEdges()
        {
            var m = this.vertices.Length;

            for (int i = 0; i < m; ++i)
            {
                this.edges[i] = new Edge3(this.vertices[i], this.vertices[(i + 1) % m]);
            }
        }

        /// <summary>
        /// Returns an enumerator for the current <see cref="Polygon3d"/>.
        /// </summary>
        /// <returns>An enumerator that allows enumeration of the vertices of the current <see cref="Polygon3d"/>.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator for the current <see cref="Polygon3d"/>.
        /// </summary>
        /// <returns>An enumerator that allows enumeration of the vertices of the current <see cref="Polygon3d"/>.</returns>
        public IEnumerator<Vertex3d> GetEnumerator()
        {
            for (int i = 0; i < this.vertices.Length; i++)
            {
                yield return this.vertices[i];
            }
        }

        /// <summary>
        /// Decomposes the current polygon into triangles.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Triangle3d> Decompose()
        {
            var a = (from v in this.Vertices
                     orderby v.X
                     select v).First();

            yield return new Triangle3d(this.vertices[0], this.vertices[1], this.vertices[2]);
            // throw new NotImplementedException();
        }

        /// <summary>
        /// Finds and returns a <see cref="Vector3d"/> that describes the position of the incenter of the current <see cref="Polygon3d"/>.
        /// </summary>
        /// <returns></returns>
        public Vector3d GetIncenter()
        {
            var sum = new Vector3d(0, 0, 0);
            foreach (var vt in this.vertices)
            {
                sum += vt.Position;
            }

            return sum / this.vertices.Length;
        }

        /// <summary>
        /// Translates all vertices of the polygon by the specified x, y, and z values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Translate(double x, double y, double z)
        {
            this.Translate(new Vector3d(x, y, z));
        }

        /// <summary>
        /// Translates all the vertices of the polygon by the specified vector amount.
        /// </summary>
        /// <param name="value"></param>
        public void Translate(Vector3d value)
        {
            foreach (var v in this.vertices)
            {
                v.Position += value;
            }
        }

        /// <summary>
        /// Inverts direction that the polygon faces by reversing the order of the vertex list.
        /// </summary>
        public void Flip()
        {
            Array.Reverse(this.vertices);
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Vertices != null);
            // Contract.Invariant(this.VertexCount >= 2);
            Contract.Invariant(Contract.ForAll(this.vertices, v => v != null));
            Contract.Invariant(this.Edges != null);
            Contract.Invariant(this.Edges.Length == this.VertexCount);
            Contract.Invariant(Contract.ForAll(this.edges, e => e != null));
        }
        #endregion
    }
}
