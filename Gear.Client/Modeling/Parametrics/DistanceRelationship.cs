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

namespace Gear.Modeling.Parametrics
{
    /// <summary>
    /// Represents a relationship between two entities that preserves distance between them.
    /// </summary>
    public class DistanceRelationship : RelationshipBase
    {
        /// <summary>
        /// Gets or sets a value indicating the distance between the two referenced entities.
        /// </summary>
        /// <remarks>
        /// If the relationship is broken, this property returns 0.
        /// </remarks>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the distance relationship is being driven by the entities it references.
        /// </summary>
        public bool IsDriving { get; set; }

        public override bool IsSolvable()
        {
            throw new NotImplementedException();
        }
    }
}
