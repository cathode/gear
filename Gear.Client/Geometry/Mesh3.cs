/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Gear.Geometry
{
    /// <summary>
    /// Represents an unstructured grid in 3d-space comprised of polygons.
    /// </summary>
    public class Mesh3 : IEnumerable<Polygon3>, IRenderable
    {
        #region Fields
        internal int VertexBuffer;

        private Polygon3[] polygons;
        private Vertex3[] vertices;

       
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Mesh3"/> class.
        /// </summary>
        public Mesh3()
        {
            this.polygons = new Polygon3[0];
            this.vertices = new Vertex3[0];
        }

        public Mesh3(params Polygon3[] polygons)
        {
            this.polygons = polygons;
            //TODO: fixme
            this.vertices = new Vertex3[0];
        }
        #endregion
        #region Properties
        public Polygon3[] Polygons
        {
            get
            {
                return this.polygons;
            }
            set
            {
                this.polygons = value;
                this.UpdateVBO();
            }
        }

        #endregion
        #region Methods
        public IEnumerator<Polygon3> GetEnumerator()
        {
            foreach (var poly in this.polygons)
                yield return poly;
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void UpdateVBO()
        {
            GL.GenBuffers(1, out this.VertexBuffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.VertexBuffer);
        }
        #endregion

        public IList<IRenderableVertex> Vertices
        {
            get { return this.vertices; }
        }

        public IEnumerable<IRenderableFace> Faces
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IRenderableEdge> Edges
        {
            get { throw new NotImplementedException(); }
        }
    }
}
