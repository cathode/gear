/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
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
    /// Represents an immutable, low-overhead 3D mesh that can be streamed directly to graphics hardware.
    /// </summary>
    public class FastMesh
    {
        #region Fields
        private FastVertex[] vertices;
        private int[] indices;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FastMesh"/> class.
        /// </summary>
        /// <param name="vertexCount"></param>
        /// <param name="faceCount"></param>
        /// <param name="edgeCount"></param>
        internal FastMesh(int vertexCount, int faceCount)
        {
            this.vertices = new FastVertex[vertexCount];
            this.indices = new int[faceCount * 3];
        }
        #endregion
        #region Properties

        #endregion
        #region Methods
        #endregion
    }

    public struct FastVertex
    {
        #region Fields
        private Vector3d position;
        private Vector3d normal;
        private Vector2d texCoords;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FastVertex"/> struct.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public FastVertex(double x, double y, double z)
        {
            this.position = new Vector3d(x, y, z);
            this.normal = Vector3d.Zero;
            this.texCoords = Vector2d.Zero;
        }

        #endregion
        #region Properties
        public Vector3d Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }
        #endregion
    }

    public struct FastFace : IRenderableFace
    {
        #region Fields
        private uint a;
        private uint b;
        private uint c;

        #endregion
        #region Constructors
        public FastFace(uint p1, uint p2, uint p3)
        {
            this.a = p1;
            this.b = p2;
            this.c = p3;
        }

        #endregion
        #region Properties
        public uint A
        {
            get
            {
                return this.a;
            }

            set
            {
                this.a = value;
            }
        }

        public uint B
        {
            get
            {
                return this.b;
            }

            set
            {
                this.b = value;
            }
        }

        public uint C
        {
            get
            {
                return this.c;
            }

            set
            {
                this.c = value;
            }
        }
        #endregion
    }

    public struct FastEdge : IRenderableEdge
    {
        #region Fields
        private uint p;
        private uint q;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FastEdge"/> class.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        public FastEdge(uint p, uint q)
        {
            this.p = p;
            this.q = q;
        }

        #endregion
        #region Properties
        public uint P
        {
            get
            {
                return this.p;
            }

            internal set
            {
                this.p = value;
            }
        }

        public uint Q
        {
            get
            {
                return this.q;
            }

            set
            {
                this.q = value;
            }
        }
        #endregion
    }
}
