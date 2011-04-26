/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Assets
{
    /// <summary>
    /// Represents an index of the assets contained within a <see cref="Package"/>.
    /// </summary>
    public sealed class PackageIndex
    {
        #region Fields
        public const int BlockSize = 512;
        private readonly Queue<int> freeBlocks = new Queue<int>();
        private readonly Dictionary<Guid, List<int>> allocatedBlocks = new Dictionary<Guid, List<int>>();
        private int lastAllocatedBlock;
        private int highestAllocatedBlock;
        #endregion
        public bool IsBlockAllocated(int block)
        {
            throw new NotImplementedException();
        }
        public bool IsIdAllocated(Guid assetId)
        {
            return this.allocatedBlocks.ContainsKey(assetId);
        }
        /// <summary>
        /// Allocates a new block for the specified <see cref="Guid"/>.
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        public int AllocateBlock(Guid assetId)
        {
            if (!this.allocatedBlocks.ContainsKey(assetId))
                this.allocatedBlocks.Add(assetId, new List<int>());

            this.lastAllocatedBlock = (this.freeBlocks.Count > 0) ? this.freeBlocks.Dequeue() : this.highestAllocatedBlock + 1;
            this.allocatedBlocks[assetId].Add(this.lastAllocatedBlock);

            if (this.lastAllocatedBlock > this.highestAllocatedBlock)
                this.highestAllocatedBlock = this.lastAllocatedBlock;

            return this.lastAllocatedBlock;
        }
        public void DeallocateBlock(int block)
        {
            throw new NotImplementedException();
        }
        public void DeallocateBlocksById(Guid assetId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scans the entire block allocation table and populates the free block queue with all free blocks.
        /// </summary>
        public void ScanForAllUnusedBlocks()
        {
            
        }
    }
}
