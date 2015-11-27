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

namespace Gear.Client.SceneGraph
{
    /// <summary>
    /// Represents a scene node constraint that can dynamically limit
    /// the position, rotation, and/or scale of a node.
    /// </summary>
    public class Constraint
    {
        #region Fields

        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Constraint"/> class.
        /// </summary>
        /// <param name="subject">The node that the constraint applies to.</param>
        /// <param name="target">The property of the node that is affected by the constraint.</param>
        public Constraint(ConstraintTarget target)
        {

        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="ConstraintTarget"/> that indicates the property or properties on the subject node which are constrained.
        /// </summary>
        public ConstraintTarget Target
        {
            get;
            set;
        }

        #endregion
        #region Methods
        public Vector3d ApplyPositionConstraint(Vector3d newPosition)
        {
            throw new NotImplementedException();
        }

        public Vector3d ApplyOrientationConstraint(Vector3d newOrientation)
        {
            throw new NotImplementedException();
        }

        public Vector3d ApplyScaleConstraint(Vector3d newScale)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
