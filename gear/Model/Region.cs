using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Model
{
    /// <summary>
    /// Represents a collection of segments.
    /// </summary>
    public class Region
    {
        public Guid RegionId { get; set; }

        public List<Segment> Segments { get; set; }
    }
}
