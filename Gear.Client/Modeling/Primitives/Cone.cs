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
using Gear.Geometry;
using System.Diagnostics.Contracts;

namespace Gear.Modeling.Primitives
{
    public class Cone : Mesh3
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Cone"/> class.
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="height"></param>
        public Cone(double radius, double height)
            : this(radius, height, 12)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cone"/> class.
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="height"></param>
        /// <param name="vertexCount"></param>
        public Cone(double radius, double height, int vertexCount)
        {
            Contract.Requires(vertexCount > 2);

            var alpha = Math.PI * (2.0 / vertexCount);
            var vertices = new Vertex3[vertexCount + 2];

            int tipIndex = vertexCount;
            int baseIndex = vertexCount + 1;
            vertices[tipIndex] = new Vertex3(0, height, 0); // Tip of cone
            vertices[baseIndex] = new Vertex3(0, 0, 0); // Center of base

            for (int i = 0; i < vertexCount; ++i)
            {
                var theta = alpha * i;
                vertices[i] = new Vertex3(Math.Cos(theta) * radius, 0, Math.Sin(theta) * radius);
            }

            var polys = new List<Polygon3>();
            polys.Add(new Triangle3(vertices[tipIndex], vertices[0], vertices[tipIndex - 1]));
            polys.Add(new Triangle3(vertices[0], vertices[tipIndex - 1], vertices[baseIndex]));

            for (int i = 1; i < vertexCount; ++i)
            {
                polys.Add(new Triangle3(vertices[tipIndex], vertices[i - 1], vertices[i]));
                polys.Add(new Triangle3(vertices[i], vertices[i - 1], vertices[baseIndex]));
            }

            this.Polygons = polys.ToArray();
        }
        #endregion
    }
}
