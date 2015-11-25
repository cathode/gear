using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Client.Geometry;
using Gear.Model.Generators;

namespace Gear.Model
{
    public class InfinityWorld : World
    {
        public InfinityWorld(InfinityGenerator generator) : base(generator)
        {

        }

        protected InfinityWorld(IGenerator generator) : base(generator)
        {
        }
        public override Vector3 GetGravityVector(Vector3 location)
        {
            return Vector3.Down;

        }

        public override double GetSurfaceElevation(Vector3 location)
        {
            throw new NotImplementedException();
        }
    }
}
