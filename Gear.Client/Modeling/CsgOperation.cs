/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Modeling
{
    /// <summary>
    /// Represents supported constructive solid geometry operations.
    /// </summary>
    public enum CsgOperation
    {
        /// <summary>
        /// Represents the merger of two objects into one.
        /// </summary>
        Union,

        /// <summary>
        /// Subtraction of the operator from the operand.
        /// </summary>
        Difference,

        /// <summary>
        /// 
        /// </summary>
        Intersection,
    }
}
