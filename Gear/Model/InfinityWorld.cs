using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Client.Geometry;

namespace Gear.Model
{
    public class InfinityWorld : World
    {
        public InfinityWorld(int seed) : base(seed)
        {

        }
        public override Vector3 GetGravityVector(Vector3 location)
        {
            return Vector3.Down;

        }
        public override Chunk GenerateChunk(Vector3 location)
        {
            throw new NotImplementedException();
        }

        public override double GetSurfaceElevation(Vector3 location)
        {
            throw new NotImplementedException();
        }
    }
}
