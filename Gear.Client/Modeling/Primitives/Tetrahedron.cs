/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Gear.Geometry;

namespace Gear.Modeling.Primitives
{
    public sealed class Tetrahedron : ProceduralMesh
    {
        #region Fields
        private double radius;
        private RadiusMode mode;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tetrahedron"/> class.
        /// </summary>
        public Tetrahedron()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tetrahedron"/> class.
        /// </summary>
        /// <param name="radius"></param>
        public Tetrahedron(double radius)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods
        protected override IEnumerable<ProcedureParameter> InitializeParameters()
        {
            yield return new ProcedureParameter("radius");
            yield return new ProcedureParameter("mode");
        }
        #endregion

        public override void RecalculateMesh()
        {
            throw new NotImplementedException();
        }

        protected override void ApplyParameters()
        {
            this.radius = (double)this.GetParameterValue("radius");
            this.mode = (RadiusMode)this.GetParameterValue("mode");
        }
    }
}
