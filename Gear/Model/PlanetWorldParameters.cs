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

        /// <summary>
        /// Gets or sets a value indicating the height (in kilometers) of the planet's atmosphere.
        /// </summary>
        public double AtmosphereHeightKm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating how oblate (flat) the planet is.
        /// </summary>
        /// <remarks>
        /// A value of 0.0 indicates a perfect sphere, a value of 1.0 indicates
        /// an ellipsoid whose equatorial diameter is twice it's polar diameter,
        /// and a value of -1.0 indicates an ellipsoid whose equatorial diameter
        /// is half of it's polar diameter.
        /// </remarks>
        public double Oblateness { get; set; }

        public double AxialTilt { get; set; }

        public double RotationalVelocity { get; set; }

        public double OrbitalVelocity { get; set; }

        public double AverageOrbit { get; set; }

        public double OrbitalEccentricity { get; set; }
    }
}
