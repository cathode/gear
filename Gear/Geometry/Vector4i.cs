﻿/******************************************************************************
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
    /// Represents a 4-dimensional integer vector.
    /// </summary>
    public struct Vector4i
    {
        private readonly int x;
        private readonly int y;
        private readonly int z;
        private readonly int w;

        public Vector4i(int x, int y, int z, int w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }

        public int Z
        {
            get
            {
                return this.z;
            }
        }

        public int W
        {
            get
            {
                return this.w;
            }
        }
    }
}
