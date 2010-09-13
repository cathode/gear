/************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/   *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.   *
 * -------------------------------------------------------------------- *
 * Contributors:                                                        *
 * - Will 'cathode' Shelley <cathode@live.com>                          *
 ************************************************************************/
using System;
using System.IO;
using System.Collections.Generic;

namespace Gear.Assets
{
    /// <summary>
    /// Represents a collection of assets and provides methods to perform random reads of specific asset data.
    /// </summary>
    public sealed class Package : IDisposable
    {
        #region Fields
        /// <summary>
        /// Holds the default file extension for package files.
        /// </summary>
        public const string DefaultFileExtension = ".g";

        /// <summary>
        /// Backing field for the <see cref="Package.IsDisposed"/> property.
        /// </summary>
        private bool isDisposed;

        private Stream stream;

        ///// <summary>
        ///// Backing field for <see cref="PackageHeader.IndexOffset"/> property.
        ///// </summary>
        //private uint indexOffset;
        //private Queue<int> freeBlocks;
        //private Dictionary<long, Guid> blockAllocationTable;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Package"/> class.
        /// </summary>
        /// <param name="stream"></param>
        internal Package(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            this.stream = stream;
        }
        #endregion
        #region Methods
        public void Include(Asset asset)
        {
            throw new NotImplementedException();
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
        internal Stream OpenAssetStream(Guid assetId)
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

        public int Count
        {
            get;
            set;
        }
        #endregion
    }
}
