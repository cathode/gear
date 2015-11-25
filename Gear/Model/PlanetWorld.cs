using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Client.Geometry;
using Gear.Model.Generators;

namespace Gear.Model
{
    /// <summary>
    /// Represents a planetary (spheroid) world.
    /// </summary>
    public class PlanetWorld : World
    {
        public PlanetWorld(PlanetGenerator generator) : base(generator)
        {
            this.Generator = generator;
        }

        /// <summary>
        /// Gets the 
        /// </summary>
        public PlanetGeneratorParameters GeneratedParameters { get; set; }

        public override double GetSurfaceElevation(Vector3 location)
        {
            return 0;
        }

        public override Vector3 GetGravityVector(Vector3 location)
        {
            var gv = location.Invert().Normalize();

            return gv;
        }

        public new PlanetGenerator Generator { get; private set; }
    }
}
