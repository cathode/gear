using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Geometry
{
    public struct Vertex3f : IRenderableVertex
    {
        public Vertex3f(float x, float y, float z)
        {
            this.Position = new Vector3f(x, y, z);
            this.Normal = Vector3f.Zero;
            this.TexCoords = Vector2f.Zero;
        }

        public Vertex3f(Vector3f position, Vector3f normal, Vector2f texCoords)
        {
            this.Position = position;
            this.Normal = normal;
            this.TexCoords = texCoords;

        }

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
