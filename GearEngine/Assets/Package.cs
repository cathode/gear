/* Gear - A Steampunk Action-RPG. http://trac.gearedstudios.com/gear/
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved. */
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

        internal long AllocateBlocks(Guid assetId, int count)
        {
            throw new NotImplementedException();
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
        /// Gets the block size for datablocks in the package.
        /// </summary>
        public ushort BlockSize
        {
            get
            {
                return this.blockSize;
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
