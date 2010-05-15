/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
 * See the license.txt file for license information. */
using System;
using System.IO;

namespace Gear.Assets
{
    /// <summary>
    /// Represents a collection of assets and provides methods to perform random reads of specific asset data.
    /// </summary>
    public sealed class Package : IDisposable
    {
        #region Constructors
        internal Package(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            this.stream = stream;
        }
        #endregion
        #region Fields
        public const ushort BlockSizeMin = 512;
        public const ushort BlockSizeMax = 32768;
        private bool isDisposed;

        private Stream stream;

        /// <summary>
        /// Backing field for <see cref="PackageHeader.BlockSize"/> property.
        /// </summary>
        private ushort blockSize;

        /// <summary>
        /// Backing field for <see cref="PackageHeader.IndexOffset"/> property.
        /// </summary>
        private uint indexOffset;

        #endregion
        #region Methods

        public void Include(Asset asset)
        {
            this.Count += 1;
        }

        /// <summary>
        /// Releases any unmanaged resources held by the current <see cref="Package"/>.
        /// </summary>
        public void Dispose()
        {
            if (this.IsDisposed)
                return;

            this.isDisposed = true;
        }

        public static Package CreateInMemory()
        {
            MemoryStream ms = new MemoryStream();
            return new Package(ms);
        }

        public static Package Open(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            return new Package(stream);
        }

        public static Package Open(string path)
        {
            return Package.Open(File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite));
        }

        private void ReadPackageHeader()
        {
            stream.Position = 0;

            BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.UTF8);


        }

        #endregion
        #region Properties
        /// <summary>
        /// Gets a value indicating whether the current <see cref="Gear.Assets.Package"/> is disposed.
        /// </summary>
        public bool IsDisposed
        {
            get
            {
                return this.isDisposed;
            }
        }

        /// <summary>
        /// Gets or sets the block size for datablocks in the package.
        /// </summary>
        /// <remarks>
        /// All asset streams are stored using a number of uniformly sized octet-aligned blocks. This property specifies how big each block is.
        /// Block size must be between 512 and 16,777,216.
        /// </remarks>
        public ushort BlockSize
        {
            get
            {
                return this.blockSize;
            }
            set
            {
                if (value < Package.BlockSizeMin || value > Package.BlockSizeMax)
                    throw new ArgumentOutOfRangeException("value");

                this.blockSize = value;
            }
        }

        public int Count
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the block where the package index starts.
        /// </summary>
        private uint IndexOffset
        {
            get
            {
                return this.indexOffset;
            }
            set
            {
                this.indexOffset = value;
            }
        }
        #endregion
    }
}
