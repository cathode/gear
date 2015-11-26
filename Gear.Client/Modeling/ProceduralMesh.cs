﻿/******************************************************************************
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
using Gear.Client.SceneGraph;
using Gear.Geometry;

namespace Gear.Modeling
{
    /// <summary>
    /// Provides a base class for subtypes that represent procedurally created meshes.
    /// </summary>
    public abstract class ProceduralMesh : Mesh3
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Parameters"/> property.
        /// </summary>
        private readonly ProcedureParameterCollection parameters;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ProceduralMesh"/> class.
        /// </summary>
        protected ProceduralMesh()
        {
            this.parameters = new ProcedureParameterCollection();

            var pl = this.InitializeParameters();

            foreach (var p in pl)
                this.parameters.Add(p);
        }
        #endregion
        #region Properties
        protected ProcedureParameterCollection Parameters
        {
            get
            {
                return this.parameters;
            }
        }
        #endregion
        #region Methods
        public object GetParameterValue(string name)
        {
            return this.parameters[name];
        }

        public abstract void RecalculateMesh();

        /// <summary>
        /// When implemented in a derived class, applies any updated values of the procedure parameters for the current
        /// <see cref="ProceduralMesh"/>.
        /// </summary>
        protected abstract void ApplyParameters();

        /// <summary>
        /// When implemented in a derived class, sets up the procedure parameters for the current procedural mesh.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<ProcedureParameter> InitializeParameters();
        #endregion
    }
}
