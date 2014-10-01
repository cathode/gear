using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Model
{
    /// <summary>
    /// Represents an individual block.
    /// </summary>
    public struct Block
    {
        public ushort TypeId;
        public BlockFlags Flags;
        public byte Lighting;
    }
}
