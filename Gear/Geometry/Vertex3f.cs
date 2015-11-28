using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Geometry
{
    public struct Vertex3f : IRenderableVertex
    {
        private Vector3f position;
        private Vector3f normal;
        private Vector2f texCoords;

        public Vertex3f(float x, float y, float z)
        {
            this.position = new Vector3f(x, y, z);
            this.normal = Vector3f.Zero;
            this.texCoords = Vector2f.Zero;
        }

        public Vertex3f(Vector3f position, Vector3f normal, Vector2f texCoords)
        {
            this.position = position;
            this.normal = normal;
            this.texCoords = texCoords;

        }

        public Vector3f Normal
        {
            get
            {
                return this.normal;
            }
            set
            {
                this.normal = value;
            }
        }

        public Vector3f Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public Vector2f TexCoords
        {
            get
            {
                return this.texCoords;
            }
            set
            {
                this.texCoords = value;
            }
        }
    }
}
