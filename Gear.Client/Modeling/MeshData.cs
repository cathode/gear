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
    public class MeshData
    {
        #region Constructors
        public MeshData(Polygon3[] polygons)
        {

        }
        #endregion
        #region Properties
        public List<Vector3> Positions
        {
            get;
            set;
        }

        public List<Vector3> Normals
        {
            get;
            set;
        }

        public List<Vector2> TextureCoordinates
        {
            get;
            set;
        }

        public List<int> Indices
        {
            get;
            set;
        }
        #endregion
    }
}
