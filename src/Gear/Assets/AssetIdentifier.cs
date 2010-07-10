/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using System.Collections.Generic;

using System.Text;
using System.Security.Cryptography;

namespace Gear.Assets
{
    /// <summary>
    /// Identifies a specific asset without describing the location of the asset.
    /// </summary>
    public sealed class AssetIdentifier
    {
        #region Constructors - Private
        private AssetIdentifier(Guid uniqueId, AssetKind kind)
        {
            this.kind = kind;
            this.uniqueId = uniqueId;
        }
        #endregion
        #region Fields - Private
        private readonly AssetKind kind;
        private string name;
        private Version version = new Version(-1, -1, -1, -1);
        private readonly Guid uniqueId;
        #endregion
        #region Methods - Private
        public static AssetIdentifier NewIdentifier(AssetKind kind)
        {
            return AssetIdentifier.NewIdentifier(kind, null);
        }
        public static AssetIdentifier NewIdentifier(AssetKind kind, string name)
        {
            return new AssetIdentifier(Guid.NewGuid(), kind)
            {
                Name = name,
            };
        }
        private byte[] Serialize()
        {
            var buffer = new List<byte>();

            if (BitConverter.IsLittleEndian)
            {
                throw new NotImplementedException();
            }
            else
            {
                buffer.AddRange(BitConverter.GetBytes((ushort)this.Kind));
                buffer.AddRange(this.uniqueId.ToByteArray());
                buffer.AddRange(BitConverter.GetBytes(this.Version.Major));
                buffer.AddRange(BitConverter.GetBytes(this.Version.Minor));
                buffer.AddRange(BitConverter.GetBytes(this.Version.Build));
                buffer.AddRange(BitConverter.GetBytes(this.Version.Revision));
                var nameBytes = Encoding.UTF8.GetBytes(this.Name);
                buffer.AddRange(BitConverter.GetBytes((ushort)nameBytes.Length));
                buffer.AddRange(nameBytes);
            }

            return buffer.ToArray();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Calculates the MD5 checksum of the current <see cref="AssetIdentifier"/>.
        /// </summary>
        /// <returns></returns>
        public byte[] GetChecksum()
        {
            return MD5.Create().ComputeHash(this.Serialize());
        }
        #endregion
        #region Properties - Public
        public AssetKind Kind
        {
            get
            {
                return this.kind;
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
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public Guid UniqueId
        {
            get
            {
                return this.uniqueId;
            }
        }
        #endregion
    }
}
