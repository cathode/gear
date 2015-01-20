using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gear.Model
{
    /// <summary>
    /// Represents shared functionality used by types which implement terrain generation algorithms.
    /// </summary>
    public abstract class TerrainGenerator
    {
        private Queue<ChunkGenerationRequest> requestedChunks;


        /// <summary>
        /// Generates the terrain comprising the chunk at the specified coordinates. This method is thread-safe.
        /// </summary>
        /// <param name="zoneX"></param>
        /// <param name="zoneY"></param>
        /// <param name="zoneZ"></param>
        /// <param name="chunkId"></param>
        /// <returns>If terrain is already being generated, this method will add the specified chunk coordinates to the generation queue and then return immediately.</returns>
        public void GenerateTerrain(ChunkLocation location, Func<Chunk> callback = null)
        {
            // Note: no checking is done for existing chunk data inside the terrain generation pipeline. That responsibility lies outside terrain generation.
            lock (this.requestedChunks)
            {
                var req = this.requestedChunks.FirstOrDefault(e => e.Location == location);
                // Create a generation request if it isn't already queued
                if (req == null)
                    req = new ChunkGenerationRequest { Location = location, Callbacks = new List<Func<Chunk>>() };

                // Add the callback to the generation request
                req.Callbacks.Add(callback);
            }
        }

        /// <summary>
        /// Generates non-terrain items on the specified chunk.
        /// </summary>
        /// <param name="zoneX"></param>
        /// <param name="zoneY"></param>
        /// <param name="zoneZ"></param>
        /// <param name="chunkId"></param>
        public void PopulateDoodads(ChunkLocation location)
        {

        }

        protected abstract void FillChunk(Chunk chunk);

        protected virtual double ElevationFunction(ChunkLocation location, byte chunkX, byte chunkY)
        {
            return 0.0;
        }

        private class ChunkGenerationRequest
        {
            internal ChunkLocation Location;

            internal List<Func<Chunk>> Callbacks;
        }
    }
}
