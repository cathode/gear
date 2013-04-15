using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Gear
{
    /// <summary>
    /// A game world.
    /// </summary>
    public class World
    {

        /// <summary>
        /// Gets or sets the name of the world.
        /// </summary>
        public string Name
        {
            get;
            set;
        }


        public WorldDimension GetDimension(string name)
        {
            throw new NotImplementedException();
        }
    }


    public class WorldDimension
    {
        public string Name
        {
            get;
            set;
        }

        public Chunk PrimaryChunk
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Represents a portion of 3d terrain. A chunk is a 32 x 32 x 32 collection of blocks.
    /// </summary>
    public unsafe class Chunk
    {
        public const int DataLength = 1 << 15;
        private BlockState[] data;

        /// <summary>
        /// Initializes a new instance of the <see cref="Chunk"/> class.
        /// </summary>
        internal Chunk()
        {
            this.data = new BlockState[Chunk.DataLength];
        }

        /// <summary>
        /// Gets or sets the block data 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public BlockState this[byte x, byte y, byte z]
        {
            get
            {
                Contract.Requires(x < 32);
                Contract.Requires(y < 32);
                Contract.Requires(z < 32);

                return this.data[x | y << 10 | z << 5];
            }
            set
            {
                Contract.Requires(x < 32);
                Contract.Requires(y < 32);
                Contract.Requires(z < 32);

                this.data[x | y << 10 | z << 5] = value;
            }
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.data != null);
            Contract.Invariant(this.data.Length == Chunk.DataLength);
        }
    }

    public struct BlockState
    {
        public ushort TypeId;
        public byte Metadata;
        public byte Lighting;
    }

    /// <summary>
    /// Represents the implementation of a given type of block.
    /// </summary>
    public class BlockDefinition
    {
        /// <summary>
        /// Gets or sets the numerical id of the block definition.
        /// </summary>
        public ushort TypeId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the block.
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
}