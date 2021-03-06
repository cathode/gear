﻿/******************************************************************************
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
using Gear.Geometry;

namespace Gear.Modeling.Primitives
{
    internal static class __Extensions
    {
        internal static int Floor(this double value)
        {
            return (value > 0) ? (int)value : (int)(value - 1);
        }
    }

    public class SimplexNoise
    {
        #region Fields
        private byte[] perm;

        #endregion
        #region Constructors
        public SimplexNoise(object seed)
        {
            this.Seed = (seed ?? new object()).GetHashCode();
        }

        #endregion
        #region Properties
        public int Seed
        {
            get;
            set;
        }

        #endregion
        #region Methods
        public void Initialize()
        {
            var p = new byte[256];
            for (byte i = 255; i != 0; ++i)
            {
                p[i] = i;
            }

            var rng = new Random(this.Seed);
            int n = 256;

            while (n-- > 1)
            {
                var k = rng.Next(n - 1);
                var v = p[k];
                p[k] = p[n];
                p[n] = v;
            }

            this.perm = p;
        }

        public IEnumerable<Vector4d> Noise3(Vector3d p, Vector3d q)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
