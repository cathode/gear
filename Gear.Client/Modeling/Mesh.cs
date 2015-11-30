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
using System.Threading.Tasks;
using Gear.Geometry;


namespace Gear.Modeling
{
    public class Mesh
    {
        #region Fields
        private Vertex3f[] vertices;
        #endregion
        #region Constructors
        public Mesh()
        {
            this.vertices = new Vertex3f[0];
        }

        //public Mesh(IEnumerable<Triangle3f> )

        #endregion
        #region Properties

        public Vertex3f[] Vertices
        {
            get
            {
                return this.vertices;
            }
            protected set
            {
                this.vertices = value;
            }
        }
        #endregion

        #region Methods
        public static Mesh NewCube(float size = 1.0f)
        {
            var mesh = new Mesh();

            var r = size * 0.5f;
            var verts = new Vertex3f[24];
            verts[0] = new Vertex3f(r, r, r);
            verts[1] = new Vertex3f(r, -r, r);
            verts[2] = new Vertex3f(-r, -r, r);
            verts[3] = new Vertex3f(-r, r, r);

            verts[4] = new Vertex3f(r, r, -r);
            verts[5] = new Vertex3f(r, -r, -r);
            verts[6] = new Vertex3f(-r, -r, -r);
            verts[7] = new Vertex3f(-r, r, -r);

            // indices for triangles to draw
            var ts = new byte[12][];

            // top
            ts[0] = new byte[] { 0, 1, 2 };
            ts[1] = new byte[] { 2, 3, 0 };



            //var edges = new Edge3[]
            //{
            //    new Edge3(verts, 0, 1),
            //    new Edge3(verts, 1, 2),
            //    new Edge3(verts, 2, 3),
            //    new Edge3(verts, 3, 0),

            //    new Edge3(verts, 4, 5),
            //    new Edge3(verts, 5, 6),
            //    new Edge3(verts, 6, 7),
            //    new Edge3(verts, 7, 4),

            //    new Edge3(verts, 0, 6),
            //    new Edge3(verts, 1, 5),
            //    new Edge3(verts, 2, 4),
            //    new Edge3(verts, 3, 7),
            //};

            //this.Polygons = new Quad3d[] { 
            //    // Top and bottom
            //    new Quad3d(edges, 0, 1, 2, 3),
            //    new Quad3d(edges, 4, 5, 6, 7),
            //    //new Quad3(edges,  
            //    new Quad3d(verts, 0, 3, 7, 4),
            //    new Quad3d(verts, 1, 0, 4, 5),
            //    new Quad3d(verts, 2, 1, 5, 6),
            //    new Quad3d(verts, 3, 2, 6, 7),
                
            //};

            return mesh;
        }

        public static Mesh NewIcosahedron(float radius)
        {
            var verts = new Vertex3f[12];

            float phiaa = 26.56505f;

            float r = radius;

            float phia = (float)(Math.PI * phiaa / 180.0);
            float theb = (float)(Math.PI * 36.0 / 180.0);
            float the72 = (float)(Math.PI * 72.0 / 180.0);

            verts[0] = new Vertex3f(0.0f, 0.0f, r);
            verts[11] = new Vertex3f(0.0f, 0.0f, -r);
            float the = 0.0f;

            for (int i = 1; i < 6; i++)
            {
                float x = (float)(r * Math.Cos(the) * Math.Cos(phia));
                float y = (float)(r * Math.Sin(the) * Math.Cos(phia));
                float z = (float)(r * Math.Sin(phia));
                verts[i] = new Vertex3f(x, y, z);
                the += the72;
            }

            the = theb;
            for (int i = 6; i < 11; i++)
            {
                float x = (float)(r * Math.Cos(the) * Math.Cos(-phia));
                float y = (float)(r * Math.Sin(the) * Math.Cos(-phia));
                float z = (float)(r * Math.Sin(-phia));
                verts[i] = new Vertex3f(x, y, z);
                the += the72;
            }
            var polys = new Triangle3f[20];

            polys[0] = new Triangle3f(verts[0], verts[1], verts[2]);
            polys[1] = new Triangle3f(verts[0], verts[2], verts[3]);
            polys[2] = new Triangle3f(verts[0], verts[3], verts[4]);
            polys[3] = new Triangle3f(verts[0], verts[4], verts[5]);
            polys[4] = new Triangle3f(verts[0], verts[5], verts[1]);
            polys[5] = new Triangle3f(verts[11], verts[6], verts[7]);
            polys[6] = new Triangle3f(verts[11], verts[7], verts[8]);
            polys[7] = new Triangle3f(verts[11], verts[8], verts[9]);
            polys[8] = new Triangle3f(verts[11], verts[9], verts[10]);
            polys[9] = new Triangle3f(verts[11], verts[10], verts[6]);
            polys[10] = new Triangle3f(verts[1], verts[2], verts[3]);
            polys[11] = new Triangle3f(verts[2], verts[3], verts[7]);
            polys[12] = new Triangle3f(verts[3], verts[4], verts[8]);
            polys[13] = new Triangle3f(verts[4], verts[5], verts[9]);
            polys[14] = new Triangle3f(verts[5], verts[1], verts[10]);
            polys[15] = new Triangle3f(verts[6], verts[7], verts[2]);
            polys[16] = new Triangle3f(verts[7], verts[8], verts[3]);
            polys[17] = new Triangle3f(verts[8], verts[9], verts[4]);
            polys[18] = new Triangle3f(verts[9], verts[10], verts[5]);
            polys[19] = new Triangle3f(verts[10], verts[6], verts[1]);

            var result = new Mesh();
            result.vertices = verts;

            return result;
        }

        public void Optimize()
        {

        }
        #endregion
    }
}
