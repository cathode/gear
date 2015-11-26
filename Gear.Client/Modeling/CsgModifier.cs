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
using System.Threading.Tasks;
using Gear.Geometry;
using System.Diagnostics.Contracts;

namespace Gear.Modeling
{
    public class CsgModifier : ModifierNode
    {
        #region Fields

        #endregion
        #region Constructors

        #endregion
        #region Properties
        public CsgOperation Operation
        {
            get;
            set;
        }
        #endregion
        #region Methods
        public static Mesh3 GenerateSubtraction(Mesh3 mesh, BoundingVolume sub)
        {
            Contract.Requires(mesh != null);

            // For each of the six planes defined by the bounding volume's sides, we need to slice the mesh on those planes.

            // Find all edges that pass through the plane.

            var isec = from e in mesh.SelectMany(poly => poly.Edges).Distinct()
                       where e.P.X > sub.X
                       select e;


            var result = new Mesh3();
            return result;
        }
        #endregion
    }
}
