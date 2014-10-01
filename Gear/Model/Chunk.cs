/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2014 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Gear.Model
{
    /// <summary>
    /// Represents a portion of 3d terrain. A chunk is a 16x16x16 collection of blocks.
    /// </summary>
    public unsafe class Chunk
    {
        public static int ChunkSize = 16;

        public const int DataLength = 1 << 15;
        private Block[] data;

        /// <summary>
        /// Initializes a new instance of the <see cref="Chunk"/> class.
        /// </summary>
        internal Chunk()
        {
            this.data = new Block[Chunk.DataLength];
        }

        /// <summary>
        /// Gets or sets the block data 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public Block this[byte x, byte y, byte z]
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
}
