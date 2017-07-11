/******************************************************************************
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
using Gear.Geometry;

namespace Gear.Modeling
{
    public class Triangle3f
    {
        public Triangle3f(Vertex3f a, Vertex3f b, Vertex3f c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        public Vertex3f A { get; set; }

        public Vertex3f B { get; set; }

        public Vertex3f C { get; set; }

        public Vector3f Normal
        {
            get
            {
                return Vector3f.CrossProduct(this.B.Position - this.A.Position, this.C.Position - this.A.Position).Normalize();
            }
        }
    }
}
