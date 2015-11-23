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

namespace Gear.Client.Geometry
{
    /// <summary>
    /// Describes the absolute minimum requirements for a 3d mesh to be rendered.
    /// Essentially this is a vertex table.
    /// </summary>
    public interface IRenderable
    {
        #region Properties
       
        /// <summary>
        /// Gets a collection of vertices to render.
        /// </summary>
        IList<IRenderableVertex> Vertices
        {
            get;
        }

        IEnumerable<IRenderableFace> Faces
        {
            get;
        }

        IEnumerable<IRenderableEdge> Edges
        {
            get;
        }
        #endregion
    }

    public interface IRenderableVertex
    {
        #region Properties
        Vector3f Position { get; }
        Vector3f Normal { get; }
        Vector2f TexCoords { get; }
        #endregion
    }

    public interface IRenderableFace
    {
        uint A
        {
            get;
        }

        uint B
        {
            get;
        }

        uint C
        {
            get;
        }
    }

    public interface IRenderableEdge
    {
        uint P
        {
            get;
        }

        uint Q
        {
            get;
        }
    }
}
