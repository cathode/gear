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

namespace Gear.Geometry
{
    public struct Vector2i
    {
        private readonly int x;
        private readonly int y;

        public Vector2i(int x, int y)
        {
            this.x = x;
            this.y = y;
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
    }
}
