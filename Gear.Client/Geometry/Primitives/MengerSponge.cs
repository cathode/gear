﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Gear.Client.Geometry.Primitives
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
