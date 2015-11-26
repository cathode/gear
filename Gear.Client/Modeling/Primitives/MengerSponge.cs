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
using System.Collections.ObjectModel;

namespace Gear.Modeling.Primitives
{
    /// <summary>
    /// Provides a solid primitive that implements the Menger sponge algorithm.
    /// </summary>
    public class MengerSponge : Mesh3
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MengerSponge"/> class.
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="iterations"></param>
        public MengerSponge(double radius, int iterations)
        {
        }
        #endregion
    }
}
