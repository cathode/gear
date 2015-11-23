/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using Gear.Client.Geometry;

namespace Gear.Client.Geometry
{
    /// <summary>
    /// Represents an three-dimensional mesh combined with additional rendering information such as color or texture.
    /// </summary>
    public class Model
    {
        #region Constructors
        public Model()
        {
        }
        #endregion
        #region Fields
        private Mesh3 geometry;
        #endregion
        #region Properties
        public Mesh3 Geometry
        {
            get
            {
                return this.geometry;
            }
            set
            {
                this.geometry = value;
            }
        }
        #endregion
    }
}
