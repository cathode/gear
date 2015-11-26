using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Client.Geometry
{
    public class Vertex3f : IRenderableVertex
    {
        public Vector3f Normal
        {
            get; set;
        }

        public Vector3f Position
        {
            get; set;
        }

        public Vector2f TexCoords
        {
            get; set;
        }
    }
}
