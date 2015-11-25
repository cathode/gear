using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Model.Generators
{
    /// <summary>
    /// Implements a world generator which produces traditional 'flat' worlds  that extend infinitely horizontally.
    /// </summary>
    public class InfinityGenerator : IGenerator
    {
        public Chunk GenerateChunk(ChunkLocation location)
        {
            throw new NotImplementedException();
        }

        public World GenerateWorld(int seed)
        {
            throw new NotImplementedException();
        }

        public double GetSurfaceAltitude(ChunkLocation location)
        {
            throw new NotImplementedException();
        }
    }
}
