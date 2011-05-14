/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        public const string DefaultFileExtension = ".gp";

        /// <summary>
        /// Holds the four-character-code that is the first four bytes of a valid package stream.
        /// </summary>
        public const int FourCC = 'R' << 24 | 'U' << 16 | 'S' << 8 | 'T';

        /// <summary>
        /// Holds the size in bytes of the file header.
        /// </summary>
        public const int HeaderSize = 128;

        /// <summary>
        /// Holds the list of other packages that the current package depends on.
        /// </summary>
        private readonly List<PackageReference> references;

        /// <summary>
        /// Backing field for the <see cref="Package.Version"/> property.
        /// </summary>
        private Version version;

        /// <summary>
        /// Backing field for the <see cref="Package.Id"/> property.
        /// </summary>
        private Guid id;

        /// <summary>
        /// Holds the underlying stream that package data is written to or read from.
        /// </summary>
        private Stream stream;

        /// <summary>
        /// Holds the absolute offset in the data stream that marks the first byte of the metadata block.
        /// </summary>
        private long metaBlockOffset;

        /// <summary>
        /// Holds the absolute offset in the data stream that marks the first byte of the index table.
        /// </summary>
        private long indexTableOffset;
        
        #endregion
        #region Constructors
        /// <summary>
        /// Prevents a default instance of the <see cref="Package"/> class from being created.
        /// </summary>
        private Package()
        {
            this.references = new List<PackageReference>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets  the unique identifier of the current <see cref="Package"/>.
        /// </summary>
        public Guid Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// Gets or sets a set of flags associated with the current <see cref="Package"/>.
        /// </summary>
        public PackageFlags Flags
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="Version"/> of the current <see cref="Package"/>.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether the current <see cref="Package"/> is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the human-readable name of the current <see cref="Package"/>.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the human-readable description or summary of the contents of the current <see cref="Package"/>.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the copyright information of the current <see cref="Package"/>.
        /// </summary>
        public string Copyright
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the author of the current <see cref="Package"/>.
        /// </summary>
        public string Author
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the company (if any) of the current <see cref="Package"/>.
        /// </summary>
        public string Company
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="Uri"/> that identifies the website where information can be obtained about the current <see cref="Package"/>.
        /// </summary>
        public Uri Homepage
        {
            get;
            set;
        }
        #endregion
        #region Methods
        public static Package Create(string path)
        {
            Package pkg = new Package();
            pkg.id = Guid.NewGuid();
            pkg.stream = File.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            pkg.WriteHeader();

            return pkg;
        }
        public static Package Open(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            else if (!stream.CanRead || !stream.CanWrite || !stream.CanSeek)
                throw new ArgumentException("Supplied stream must support reading, writing, and seeking.");

            var package = new Package();
            package.stream = stream;
            package.ReadHeader();
            return package;
        }

        public static Package Open(string path)
        {
            return Package.Open(File.Open(path, FileMode.Open, FileAccess.ReadWrite));
        }

        public void Close()
        {
            this.WriteHeader();
            this.WriteReferenceList();

            this.stream.Flush();
            this.stream.Close();
        }

        private void ReadHeader()
        {
            /* Package header (big-endian):
             * 
             * Offset | Field Name          | Size
             * -------+---------------------+------
             *   0x00 | FourCC              | 4
             *   0x04 | Format Version      | 1
             *   0x05 | Reserved            | 3
             *   0x08 | Id (GUID)           | 16
             *   0x18 | Version             | 16
             *   0x28 | Index table offset  | 8
             *   0x30 | Package flags       | 4
             *   0x34 | Reserved            | 72
             *   0x3C | Header CRC32        | 4
             */

            var buffer = new DataBuffer(Package.HeaderSize, ByteOrder.BigEndian);

            this.stream.Read(buffer.Contents, 0, Package.HeaderSize);

            var fourCC = buffer.ReadInt32();
            var format = buffer.ReadByte();
            buffer.Position += 3;
            var id = buffer.ReadGuid();
            var version = buffer.ReadVersion();
            var indexTableOffset = buffer.ReadInt64();
            var metaBlockOffset = buffer.ReadInt64();
            buffer.Position = 124;
            var crc32 = buffer.ReadInt32();

            if (fourCC != Package.FourCC)
                throw new NotImplementedException("FourCC Mismatch");
            if (format != 1)
                throw new NotImplementedException("Format Mismatch");
            this.Id = id;
            this.Version = version;
            this.indexTableOffset = indexTableOffset;
            this.metaBlockOffset = metaBlockOffset;
            
        }

        private void WriteHeader()
        {
            DataBuffer buffer = new DataBuffer(Package.HeaderSize, ByteOrder.BigEndian);

            buffer.WriteInt32(Package.FourCC);
            buffer.WriteByte(1); // Package format 1
            buffer.Position += 3; // Reserved 3 bytes
            buffer.WriteGuid(this.id);
            buffer.WriteVersion(this.version);
            buffer.WriteInt64(this.indexTableOffset); // absolute offset to the start of the index table.
            buffer.WriteInt64(this.metaBlockOffset); // absolute offset to the start of the package metadata.
            buffer.Position = 124; // Skip reserved space.
            buffer.WriteInt32(0); // CRC calculated after remaining values are written.

            this.stream.Write(buffer.Contents, 0, buffer.Contents.Length);
            this.stream.Flush();
        }

        private void ReadReferenceList()
        {
            /* Reference List Header
             * Offset | Field Name          | Size
             * -------+---------------------+------
             *   0x00 | List Length (bytes) | 2
             *   0x02 | References          | 0-65535
             *   
             * 
             * Reference List Entry
             * Offset | Field Name          | Size
             * -------+---------------------+------
             *   0x00 | GUID (Target)       | 16
             *   0x10 | Version (Target)    | 16
             *   0x20 | Flags               | 1
             *   0x21 | Name (length)       | 1
             *   0x22 | Name                | 0-224
             * 0x22+N | Signature (length)  | 1
             * 0x23+N | Signature           | 0-224
             *  
             * Version and signature are optional.
             */
        }

        private void WriteReferenceList()
        {

        }
        #endregion
    }
}
