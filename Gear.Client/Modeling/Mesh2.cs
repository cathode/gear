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

namespace Gear.Modeling
{
    /// <summary>
    /// An unstructured grid of two-dimensional polygons.
    /// </summary>
    public class Mesh2 : IEnumerable<Polygon2d>
    {
        #region Fields
        private Polygon2d[] polygons;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Mesh2"/> class.
        /// </summary>
        public Mesh2()
        {
            this.polygons = new Polygon2d[0];
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the enumerator for the current instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Polygon2d> GetEnumerator()
        {
            for (int i = 0; i < this.polygons.Length; i++)
            {
                yield return this.polygons[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
