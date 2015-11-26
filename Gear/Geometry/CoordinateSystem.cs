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

namespace Gear.Geometry
{
    /// <summary>
    /// Represents a coordinate system in three-dimensional space.
    /// </summary>
    public sealed class CoordinateSystem
    {
        #region Fields
        private Vector3 scale;
        private Vector3 position;
        private Quaternion rotation;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateSystem"/> class.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        public CoordinateSystem(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
        #endregion
        #region Properties
        public Vector3 Position
        {
            get
            {
                return this.position;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return this.rotation;
            }
        }

        public Vector3 Scale
        {
            get
            {
                return this.scale;
            }
        }
        #endregion
        #region Methods
        public void Apply(CoordinateSystem system)
        {
            this.scale += system.scale;
            this.position += system.position;
            this.rotation *= system.rotation;
        }

        public Vector3 Transform(Vector3 v)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
