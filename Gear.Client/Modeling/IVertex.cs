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
using Gear.Geometry;

namespace Gear.Modeling
{
    public interface IVertex
    {
        #region Properties
        Vector3d Position { get; set; }
        Vector3d Normal { get; set; }
        object Tag { get; set; }
        #endregion
        #region Methods
        IEnumerable<IVertex> GetNeighboringVertices();
        IEnumerable<IHalfEdge> GetOutgoingEdges();
        IEnumerable<IHalfEdge> GetIncomingEdges();
        IEnumerable<IFace> GetNeighboringFaces();
        #endregion
    }
}
