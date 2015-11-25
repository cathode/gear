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

namespace Gear.Client.Geometry
{
    public struct Vector3f
    {
        #region Fields
        private readonly float x;
        private readonly float y;
        private readonly float z;
        #endregion
        public Vector3f(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public float X
        {
            get
            {
                return this.x;
            }
        }
        public float Y
        {
            get
            {
                return this.y;
            }
        }
        public float Z
        {
            get
            {
                return this.z;
            }
        }
    }
}
