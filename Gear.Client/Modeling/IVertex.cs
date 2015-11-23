using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gear.Client.Geometry;

namespace Gear.Client.Modeling
{
    public interface IVertex
    {
        #region Properties
        Vector3 Position { get; set; }
        Vector3 Normal { get; set; }
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
