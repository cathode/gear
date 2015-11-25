using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Model.Generators
{
    public class PlanetGenerator : IGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetGenerator"/> class.
        /// </summary>
        public PlanetGenerator()
        {


        }

        public PlanetGeneratorParameters ParametersMinimum { get; set; }
        public PlanetGeneratorParameters ParametersMaximum { get; set; }

        public Chunk GenerateChunk(ChunkLocation location)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        ///  Generates a new world based on the parameters configured in the current
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public World GenerateWorld(int seed)
        {
            var rng = new Random(seed);

            var pmin = this.ParametersMinimum;
            var pmax = this.ParametersMaximum;
            var p = new PlanetGeneratorParameters();

            // Calculate the diameter of the new planet.
            double factor = (double)int.MaxValue / rng.Next();
            var diameterKm = factor * (pmax.DiameterKm - pmin.DiameterKm) + pmin.DiameterKm;

            // Calculate the average density of the new planet.
            factor = (double)int.MaxValue / rng.Next();
            p.AverageDensity = factor * (pmax.AverageDensity - pmin.AverageDensity) + pmin.AverageDensity;

            // Calculate axial tilt (clamped between 0 and 180 degrees)
            factor = ((double)int.MaxValue / rng.Next());
            p.AxialTilt = factor * (pmax.AxialTilt - pmin.AxialTilt) + pmin.AxialTilt;

            // Calculate rotational velocity
            factor = ((double)int.MaxValue / rng.Next());
            double rotationalVelocity = factor * (pmax.RotationalVelocity - pmin.RotationalVelocity) + pmin.RotationalVelocity;

            // Calculate planet eccentricity (oblateness)
            factor = ((double)int.MaxValue / rng.Next());
            double eccentricity = factor * (pmax.Oblateness - pmin.Oblateness) + pmin.Oblateness;

            // Calculate mean atmosphere height
            factor = ((double)int.MaxValue / rng.Next());
            double atmosphereHeight = factor * (pmax.AtmosphereHeight - pmin.AtmosphereHeight) + pmin.AtmosphereHeight;


            // Calculate orbital speed
            factor = ((double)int.MaxValue / rng.Next());
            double orbitalVelocity = factor * (0);

            // Calculate orbital eccentricity

            // Calculate orbital inclination



            return null;
        }

        /// <summary>
        /// Calculates the average altitude of the 'surface' at the specified chunk location.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public double GetSurfaceAltitude(ChunkLocation location)
        {
            throw new NotImplementedException();
        }
    }
}
