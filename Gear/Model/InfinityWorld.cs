using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Geometry;

namespace Gear.Model
{
    public class InfinityWorld : World
    {
        public InfinityWorld(int seed) : base(seed)
        {

        }
        public override Vector3d GetGravityVector(Vector3d location)
        {
            return Vector3d.Down;

        }
        public override Chunk GenerateChunk(Vector3d location)
        {
            throw new NotImplementedException();
        }

        public override double GetSurfaceElevation(Vector3d location)
        {
            throw new NotImplementedException();
        }
    }
}
