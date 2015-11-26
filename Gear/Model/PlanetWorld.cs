using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Geometry;

namespace Gear.Model
{
    /// <summary>
    /// Represents a planetary (spheroid) world.
    /// </summary>
    public class PlanetWorld : World
    {
        public PlanetWorld(int seed) : base(seed)
        {
            //this.Generator = generator;
        }

        /// <summary>
        /// Gets the 
        /// </summary>
        public PlanetWorldParameters GeneratedParameters { get; set; }

        /// <summary>
        ///  Generates a new world based on the parameters configured in the current
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public void Initialize(PlanetWorldParameters pmin, PlanetWorldParameters pmax)
        {
            var rng = new Random(this.Seed);

            var p = new PlanetWorldParameters();

            // Calculate the diameter of the new planet.
            double factor = (double)int.MaxValue / rng.Next();
            p.DiameterKm = factor * (pmax.DiameterKm - pmin.DiameterKm) + pmin.DiameterKm;

            // Calculate the average density of the new planet.
            factor = (double)int.MaxValue / rng.Next();
            p.AverageDensity = factor * (pmax.AverageDensity - pmin.AverageDensity) + pmin.AverageDensity;

            // Calculate axial tilt (clamped between 0 and 180 degrees)
            factor = ((double)int.MaxValue / rng.Next());
            p.AxialTilt = factor * (pmax.AxialTilt - pmin.AxialTilt) + pmin.AxialTilt;

            // Calculate rotational velocity
            factor = ((double)int.MaxValue / rng.Next());
            p.RotationalVelocity = factor * (pmax.RotationalVelocity - pmin.RotationalVelocity) + pmin.RotationalVelocity;

            // Calculate planet eccentricity (oblateness)
            factor = ((double)int.MaxValue / rng.Next());
            p.Oblateness = factor * (pmax.Oblateness - pmin.Oblateness) + pmin.Oblateness;

            // Calculate mean atmosphere height
            factor = ((double)int.MaxValue / rng.Next());
            p.AtmosphereHeight = factor * (pmax.AtmosphereHeight - pmin.AtmosphereHeight) + pmin.AtmosphereHeight;


            // Calculate orbital speed
            //factor = ((double)int.MaxValue / rng.Next());
            //double orbitalVelocity = factor * (0);

            // Calculate orbital eccentricity

            // Calculate orbital inclination


            this.GeneratedParameters = p;

        }

        public override Chunk GenerateChunk(Vector3 location)
        {
            throw new NotImplementedException();
        }

        public override double GetSurfaceElevation(Vector3 location)
        {
            return 0;
        }

        public override Vector3 GetGravityVector(Vector3 location)
        {
            var gv = location.Invert().Normalize();

            return gv;
        }
    }
}
