/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace Gear.Assets
{
    /// <summary>
    /// Represents a collection of assets and provides methods to perform random reads of specific asset data.
    /// </summary>
    public sealed class Package
    {
        #region Fields
        /// <summary>
        /// Holds the default file extension for package files.
        /// </summary>
        public const string DefaultFileExtension = ".GPak";

        /// <summary>
        /// Holds the four-character-code that is the first four bytes of a valid package stream.
        /// </summary>
        public const int FourCC = 'G' << 24 | 'P' << 16 | 'A' << 8 | 'K';

        public const int HeaderSize = 64;

        private long blockTableOffset = Package.HeaderSize;

        private Version version;

        /// <summary>
        /// Backing field for the <see cref="Package.Id"/> property.
        /// </summary>
        private Guid id;

        private Stream stream;
        #endregion
        #region Constructors
        public Package()
        {
            this.id = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new current of the <see cref="Package"/> class.
        /// </summary>
        /// <param name="stream"></param>
        internal Package(Stream stream)
        {
            this.stream = stream;
        }
        #endregion
        #region Properties
        public int Count
        {
            get;
            set;
        }

        public Guid Id
        {
            get
            {
                return this.id;
            }
        }
        public Version Version
        {
            get
            {
                return this.version;
            }
            set
            {
                this.version = value;
            }
        }
        public bool IsReadOnly
        {
            get;
            set;
        }
        #endregion
        #region Methods
        public static Package Open(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            else if (!stream.CanRead || !stream.CanWrite || !stream.CanSeek)
                throw new ArgumentException("Supplied stream must support reading, writing, and seeking.");

            var package = new Package(stream);
            package.ReadHeader();
            return package;
        }

        public static Package Open(string path)
        {
            return Package.Open(File.Open(path, FileMode.Open, FileAccess.ReadWrite));
        }

        private void ReadHeader()
        {
            /* Package header (big-endian):
             * 
             * Offset | Field Name          | Size
             * -------+---------------------+------
             *   0x00 | FourCC              | 4
             *   0x04 | Id (GUID)           | 16
             *   0x14 | Version             | 16
             *   0x24 | Index table offset  | 8
             *   0x2C | Package flags       | 4
             *   0x30 | Signature SerialID  | 4
             *   0x3C | Header CRC32        | 4
             */
        }

        private void WriteHeader()
        {
            DataBuffer buffer = new DataBuffer(Package.HeaderSize, DataBufferMode.BigEndian);

            buffer.WriteInt32(Package.FourCC);
            buffer.WriteGuid(this.id);
            buffer.WriteVersion(this.version);
            buffer.WriteInt64(this.blockTableOffset);

        }
        #endregion
        #region Types
        public sealed class BlockAllocationTable
        {
            #region Fields
            public const int BlockAllocationTableId = 0x0;
            public const int AssetIndexId = 0x1;
            public const int PackageMetadataId = 0x2;
            public const int DigitalSignatureId = 0x3;
            public const int IdStart = 0x10;
            private readonly Collection<BlockAllocation> allocations = new Collection<BlockAllocation>();
            private readonly int blockSize;
            #endregion
            #region Constructors
            public BlockAllocationTable(int blockSize)
            {
                this.blockSize = blockSize;

                this.allocations.Add(new BlockAllocation()
                {
                    FirstBlock = 0,
                    EntryId = BlockAllocationTable.BlockAllocationTableId,
                    Size = 16,
                    BlockCount = 1
                });

                this.allocations.Add(new BlockAllocation()
                {
                    FirstBlock = 1,
                    EntryId = BlockAllocationTable.AssetIndexId,
                    Size = 0,
                    BlockCount = 1
                });

                this.allocations.Add(new BlockAllocation()
                {
                    FirstBlock = 2,
                    EntryId = BlockAllocationTable.PackageMetadataId,
                    Size = 0,
                    BlockCount = 1
                });

                this.allocations.Add(new BlockAllocation()
                {
                    FirstBlock = 3,
                    EntryId = BlockAllocationTable.DigitalSignatureId,
                    Size = 0,
                    BlockCount = 1
                });

                this.Extend(BlockAllocationTable.BlockAllocationTableId, 48);
            }
            #endregion
            #region Methods
            public bool Contains(int entryId)
            {
                foreach (var entry in this.allocations)
                    if (entry.EntryId == entryId)
                        return true;

                return false;
            }

            public BlockAllocation Allocate(long size)
            {
                return this.Allocate(size, false);
            }

            public BlockAllocation Allocate(long size, bool forceContigous)
            {
                throw new NotImplementedException();
            }

            public void Compact()
            {
                //throw new NotImplementedException();
            }

            /// <summary>
            /// Extends the allocation of the specified entry by a number of bytes.
            /// </summary>
            /// <param name="entryId"></param>
            /// <param name="extend"></param>
            public void Extend(int entryId, long extend)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<BlockAllocation> GetAllocatedBlocks(int entryId)
            {
                return from b in this.allocations
                       where b.EntryId == entryId
                       orderby b.FirstBlock
                       select b;
            }

            public bool IsFree(int block)
            {
                foreach (var alloc in this.allocations)
                    if (alloc.FirstBlock <= block && alloc.FirstBlock + alloc.BlockCount >= block)
                        return true;

                return true;
            }
            public void Shrink(int entryId, long shrink)
            {
                // How many blocks we need to de-allocate.
                var shrinkBlocks = shrink / this.blockSize;

                foreach (var alloc in this.GetAllocatedBlocks(entryId))
                {

                }
            }
            #endregion
        }
        public sealed class BlockAllocation
        {
            /// <summary>
            /// The number of the first block in the allocation entry.
            /// </summary>
            public int FirstBlock;

            /// <summary>
            /// The number of contigous block allocations in the entry.
            /// </summary>
            public int BlockCount;

            /// <summary>
            /// The number of bytes that are actually allocated.
            /// </summary>
            public int Size;

            /// <summary>
            /// The id of the allocated entry.
            /// </summary>
            public int EntryId;
        }
        #endregion
    }
}
