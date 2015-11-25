using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Model
{
    public interface IGenerator
    {
        World GenerateWorld(int seed);

        Chunk GenerateChunk(ChunkLocation location);

        double GetSurfaceAltitude(ChunkLocation location);
    }
}
