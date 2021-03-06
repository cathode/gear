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
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using Gear.Modeling;

namespace Gear.Geometry
{
    public static class VertexArrayExtensions
    {
        public static Triangle3d Triangle(this Vertex3d[] array, int a, int b, int c)
        {
            Contract.Requires(array.Length >= 3);
            Contract.Requires(a < array.Length);
            Contract.Requires(b < array.Length);
            Contract.Requires(c < array.Length);
            Contract.Requires(array[a] != null);
            Contract.Requires(array[b] != null);
            Contract.Requires(array[c] != null);

            return new Triangle3d(array[a], array[b], array[c]);
        }

        public static Quad3d Quad(this Vertex3d[] array, int a, int b, int c, int d)
        {
            Contract.Requires(array.Length >= 4);
            Contract.Requires(a < array.Length);
            Contract.Requires(b < array.Length);
            Contract.Requires(c < array.Length);
            Contract.Requires(d < array.Length);

            return new Quad3d(array[a], array[b], array[c], array[d]);
        }

        public static Polygon3d Polygon(this Vertex3d[] array, params int[] indices)
        {
            Contract.Requires(array != null);
            Contract.Requires(indices != null);
            Contract.Requires(indices.Length > 2);
            Contract.Requires(indices.All(i => i < array.Length));
            Contract.Requires(indices.Distinct().Count() == indices.Length);

            return new Polygon3d(array, indices);
        }
    }
}
