using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Model
{
    public class PlanetWorldParameters
    {
        /// <summary>
        /// Gets or sets a value indicating the planet size (diameter in kilometers) that is or will be generated.
        /// </summary>
        public double DiameterKm { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating the average density (in kilograms per cubic meter) of generated planets.
        /// </summary>
        /// <remarks>
        /// The average density of the planet is used in conjuction with other parameters to calculate the gravity at the planet's surface.
        /// </remarks>
        public double AverageDensity { get; set; }

        public double AtmosphereHeight { get; set; }

        public double Oblateness { get; set; }

        public double AxialTilt { get; set; }

        public double RotationalVelocity { get; set; }

        public double OrbitalVelocity { get; set; }
    }
}
