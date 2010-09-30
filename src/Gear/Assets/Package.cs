/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
        public const string DefaultFileExtension = ".gpak";

        public const int FourCC = 'G' << 24 | 'P' << 16 | 'A' << 8 | 'K';

        /// <summary>
        /// Backing field for the <see cref="Package.IsDisposed"/> property.
        /// </summary>
        private bool isDisposed;

        private Stream stream;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="Package"/> class.
        /// </summary>
        /// <param name="stream"></param>
        internal Package(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            this.stream = stream;
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
        #region Methods
        /// <summary>
        /// Releases any unmanaged resources held by the current <see cref="Package"/>.
        /// </summary>
        public void Dispose()
        {
            if (this.IsDisposed)
                return;

            this.isDisposed = true;
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

        }

        internal Stream OpenAssetStream(Guid assetId)
        {
            throw new NotImplementedException();
        }
        #endregion

        [StructLayout(LayoutKind.Explicit)]
        internal unsafe struct PackageHeader
        {
            internal const int CorrectFourCC = 'G' << 24 | 'P' << 16 | 'A' << 8 | 'K';
            [FieldOffset(0)]
            internal fixed byte Buffer[20];
            [FieldOffset(0)]
            internal int FourCC;
            [FieldOffset(4)]
            internal Version Version;
        }
    }
}
