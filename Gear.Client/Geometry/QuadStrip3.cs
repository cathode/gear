﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Gear.Modeling
{
    /// <summary>
    /// Represents a strip of connected quads in 3d space.
    /// </summary>
    public class QuadStrip3 : Polygon3
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="QuadStrip3"/> class.
        /// </summary>
        /// <param name="quads">The number of quads in the strip.</param>
        public QuadStrip3(int quads)
            : base(2 + (2 * quads))
        {
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the <see cref="PrimitiveKind"/> of the current primitive.
        /// </summary>
        public override PrimitiveKind Kind
        {
            get
            {
                return PrimitiveKind.QuadStrip;
            }
        }
        #endregion
    }
}
