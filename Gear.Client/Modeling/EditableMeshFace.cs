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
using Gear.Client.Geometry;

namespace Gear.Client.Modeling
{
    /// <summary>
    /// Represents an <see cref="EditableMesh"/> face (triangle).
    /// </summary>
    public class EditableMeshFace : IRenderableFace
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableMeshFace"/> class.
        /// </summary>
        public EditableMeshFace()
        {
        }
        #endregion
        #region Properties

        public EditableMeshEdge EdgeRing
        {
            get;
            internal set;
        }

        public EditableMeshFace Next
        {
            get;
            internal set;
        }

        public EditableMeshFace Previous
        {
            get;
            internal set;
        }

        public int Index
        {
            get;
            internal set;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Given an edge (which needs to be an edge of the current face), returns the face on the other side of the edge,
        /// relative to the current face.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public EditableMeshFace FaceAcrossEdge(EditableMeshEdge edge)
        {
            if (this == edge.AF)
                return edge.BF;
            else if (this == edge.BF)
                return edge.AF;
            else
                throw new NotImplementedException();
        }

        /// <summary>
        /// Allows enumeration over all the edges of the current face.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EditableMeshEdge> GetEdges()
        {
            var e = this.EdgeRing;

            if (e.AF == this)
            {
                yield return e;
                do
                {
                    e = e.ACW;
                    yield return e;
                } while (e != this.EdgeRing);
            }
            else if (e.BF == this)
            {
                yield return e;
                do
                {
                    e = e.BCW;
                    yield return e;
                } while (e != this.EdgeRing);
            }
            else
            {
                // edge ring is somehow not associated with the current face?!?
                throw new NotImplementedException();
            }
        }
        #endregion



        uint IRenderableFace.A
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        uint IRenderableFace.B
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        uint IRenderableFace.C
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
