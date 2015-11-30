using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace Gear.Geometry
{
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct Vertex3f : IRenderableVertex
    {
        //[FieldOffset(0)]
        private Vector3f position;
        //[FieldOffset(12)]
        private Vector3f normal;
        //[FieldOffset(24)]
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
