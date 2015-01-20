using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.Messages
{
    /// <summary>
    /// Represents a request from one peer to fetch the block data of a zone.
    /// </summary>
    public class ZoneDataRequestMessage : IMessage
    {
        public int DispatchId
        {
            get { throw new NotImplementedException(); }
        }

        public long ZoneX { get; set; }
        public long ZoneY { get; set; }
        public long ZoneZ { get; set; }

        public byte ChunkX { get; set; }
        public byte ChunkY { get; set; }
        public byte ChunkZ { get; set; }
    }
}
