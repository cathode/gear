using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear
{
    /// <summary>
    /// Metadata for players
    /// </summary>
    public class PlayerInfo
    {
        public string Name { get; set; }

        public Guid PlayerId { get; set; }
    }
}
