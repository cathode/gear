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
using System.Diagnostics.Contracts;

namespace Gear.Client.Modeling.Primitives
{
    public class Box : ProceduralMesh
    {
        #region Fields
        //private
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Box(double x, double y, double z)
        {

        }
        #endregion
        #region Properties

        #endregion
        #region Methods

        #endregion

        public override void RecalculateMesh()
        {
            throw new NotImplementedException();
        }

        protected override void ApplyParameters()
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<ProcedureParameter> InitializeParameters()
        {
            throw new NotImplementedException();
        }
    }
}
