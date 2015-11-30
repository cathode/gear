/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics.Contracts;
using Gear.Geometry;

namespace Gear.Modeling
{
    /// <summary>
    /// Represents a quad in 3D-space, in other words a four sided planar polygon.
    /// </summary>
    public class Quad3d : Polygon3d
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Quad3d"/> class.
        /// </summary>
        public Quad3d()
            : base(4)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quad3d"/> class.
        /// </summary>
        /// <param name="radius">The radius of the new <see cref="Quad3d"/>.</param>
        public Quad3d(double radius)
            : base(4, radius)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quad3d"/> class.
        /// </summary>
        /// <param name="radius">The radius of the new <see cref="Quad3d"/>.</param>
        /// <param name="mode">The <see cref="RadiusMode"/> that describes how the value of the radius parameter is interpreted.</param>
        public Quad3d(double radius, RadiusMode mode)
            : base(4, radius, mode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quad3d"/> class.
        /// </summary>
        /// <param name="a">The top-left vertex of the quad.</param>
        /// <param name="b">The top-right vertex of the quad.</param>
        /// <param name="c">The bottom-right vertex of the quad.</param>
        /// <param name="d">The bottom-left vertex of the quad.</param>
        public Quad3d(Vertex3d a, Vertex3d b, Vertex3d c, Vertex3d d)
            : base(a, b, c, d)
        {
            
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Quad3d"/> class.
        /// </summary>
        /// <param name="verts"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        public Quad3d(Vertex3d[] verts, int a, int b, int c, int d)
            : base(verts[a], verts[b], verts[c], verts[d])
        {
            Contract.Requires(verts != null);
            Contract.Requires(verts.Length > 3);
            Contract.Requires(a >= 0);
            Contract.Requires(a < verts.Length);
            Contract.Requires(b >= 0);
            Contract.Requires(b < verts.Length);
            Contract.Requires(c >= 0);
            Contract.Requires(c < verts.Length);
            Contract.Requires(d >= 0);
            Contract.Requires(d < verts.Length);
        }

        public Quad3d(Edge3[] edges, int a, int b, int c, int d)
            : base(edges, a, b, c, d)
        {
            Contract.Requires(edges != null);
            Contract.Requires(edges.Length > 3);
        }

        public Quad3d(double width, double height)
            : base(new Vertex3d(), new Vertex3d(), new Vertex3d(), new Vertex3d())
        {
            var x1 = width / -2.0;
            var x2 = width / 2.0;
            var y1 = height / -2.0;
            var y2 = height / 2.0;

            this.A.Position = new Vector3d(x1, y1, 0.0);
            this.B.Position = new Vector3d(x1, y2, 0.0);
            this.C.Position = new Vector3d(x2, y2, 0.0);
            this.D.Position = new Vector3d(x2, y1, 0.0);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the top-left vertex of the quad.
        /// </summary>
        public Vertex3d A
        {
            get
            {
                return this[0];
            }
            set
            {
                Contract.Requires(value != null);

                this[0] = value;
            }
        }

        /// <summary>
        /// Gets or sets the top-right vertex of the quad.
        /// </summary>
        public Vertex3d B
        {
            get
            {
                return this[1];
            }
            set
            {
                Contract.Requires(value != null);

                this[1] = value;
            }
        }

        /// <summary>
        /// Gets or sets the bottom-right vertex of the quad.
        /// </summary>
        public Vertex3d C
        {
            get
            {
                return this[2];
            }
            set
            {
                Contract.Requires(value != null);

                this[2] = value;
            }
        }

        /// <summary>
        /// Gets or sets the bottom-left vertex of the quad.
        /// </summary>
        public Vertex3d D
        {
            get
            {
                return this[3];
            }
            set
            {
                Contract.Requires(value != null);

                this[3] = value;
            }
        }

        public Edge3 AB
        {
            get
            {
                return this.edges[0];
            }
        }

        public Edge3 BC
        {
            get
            {
                return this.edges[1];
            }
        }

        public Edge3 CD
        {
            get
            {
                return this.edges[2];
            }
        }
        public Edge3 DA
        {
            get
            {
                return this.edges[3];
            }
        }

        /// <summary>
        /// Overridden. Returns the primitive kind.
        /// </summary>
        public override PrimitiveKind Kind
        {
            get
            {
                return PrimitiveKind.Quad;
            }
        }
        #endregion
        #region Methods
        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Vertices.Length == 4);
        }
        #endregion
    }
}
