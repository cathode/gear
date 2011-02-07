/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Rust.Assets
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
        public const string DefaultFileExtension = ".rust";

        /// <summary>
        /// Holds the four-character-code that is the first four bytes of a valid package stream.
        /// </summary>
        public const int FourCC = 'R' << 24 | 'U' << 16 | 'S' << 8 | 'T';

        /// <summary>
        /// Holds the size in bytes of the file header.
        /// </summary>
        public const int HeaderSize = 80;

        public static readonly byte FormatVersion = 1;

        public static readonly byte CommonApplicationCode = 0;

        /// <summary>
        /// Holds the list of other packages that the current package depends on.
        /// </summary>
        private readonly List<PackageReference> references;

        /// <summary>
        /// Backing field for the <see cref="Package.Version"/> property.
        /// </summary>
        private Version version;

        /// <summary>
        /// Backing field for the <see cref="Package.UniqueID"/> property.
        /// </summary>
        private Guid id;

        /// <summary>
        /// Holds the underlying stream that package data is written to or read from.
        /// </summary>
        private Stream stream;

        /// <summary>
        /// Holds the absolute offset in the data stream that marks the first byte of the metadata block.
        /// </summary>
        private long metadataOffset;

        /// <summary>
        /// Holds the absolute offset in the data stream that marks the first byte of the index table.
        /// </summary>
        private long indexTableOffset;
        private long referenceListOffset;
        private readonly Queue<PendingWrite> pendingWrites = new Queue<PendingWrite>();

        private bool headerModified = true;
        private bool referenceListModified = true;
        private bool metadataModified = true;
        private bool indexTableModified = true;
        private Uri homepage;
        private string author;
        private string name;
        private string copyright;
        private string company;
        private string summary;
        private PackageFlags flags;
        #endregion
        #region Constructors
        /// <summary>
        /// Prevents a default instance of the <see cref="Package"/> class from being created.
        /// </summary>
        private Package()
        {
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets  the unique identifier of the current <see cref="Package"/>.
        /// </summary>
        public Guid UniqueID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                this.headerModified = true;
            }
        }

        /// <summary>
        /// Gets or sets a set of flags associated with the current <see cref="Package"/>.
        /// </summary>
        public PackageFlags Flags
        {
            get
            {
                return this.flags;
            }
            set
            {
                this.flags = value;
                this.headerModified = true;
            }
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
                this.headerModified = true;
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
            get
            {
                return this.name ?? string.Empty;
            }
            set
            {
                this.name = value;
                this.metadataModified = true;
            }
        }

        /// <summary>
        /// Gets or sets the human-readable description or summary of the contents of the current <see cref="Package"/>.
        /// </summary>
        public string Summary
        {
            get
            {
                return this.summary ?? string.Empty;
            }
            set
            {
                this.summary = value;
                this.metadataModified = true;
            }
        }

        /// <summary>
        /// Gets or sets the copyright information of the current <see cref="Package"/>.
        /// </summary>
        public string Copyright
        {
            get
            {
                return this.copyright ?? string.Empty;
            }
            set
            {
                this.copyright = value;
                this.metadataModified = true;
            }
        }

        /// <summary>
        /// Gets or sets the name of the author of the current <see cref="Package"/>.
        /// </summary>
        public string Author
        {
            get
            {
                return this.author ?? string.Empty;
            }
            set
            {
                this.author = value;
                this.metadataModified = true;
            }
        }

        /// <summary>
        /// Gets or sets the name of the company (if any) of the current <see cref="Package"/>.
        /// </summary>
        public string Company
        {
            get
            {
                return this.company ?? string.Empty;
            }
            set
            {
                this.company = value;
                this.metadataModified = true;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Uri"/> that identifies the website where information can be obtained about the current <see cref="Package"/>.
        /// </summary>
        public Uri Homepage
        {
            get
            {
                return this.homepage;
            }
            set
            {
                this.homepage = value;
                this.metadataModified = true;
            }
        }

        public long MetadataOffset
        {
            get
            {
                return this.metadataOffset;
            }
            private set
            {
                this.metadataOffset = value;
                this.metadataModified = true;
            }
        }

        public long IndexTableOffset
        {
            get
            {
                return this.indexTableOffset;
            }
            private set
            {
                this.indexTableOffset = value;
                this.indexTableModified = true;
            }
        }

        public long ReferenceListOffset
        {
            get
            {
                return this.referenceListOffset;
            }
            private set
            {
                this.referenceListOffset = value;
                this.referenceListModified = true;
            }
        }
        #endregion
        #region Methods
        public static Package Create(string path)
        {
            Package pkg = new Package();
            pkg.id = Guid.NewGuid();
            pkg.stream = File.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            pkg.WriteHeader();
            pkg.Flush();

            return pkg;
        }

        /// <summary>
        /// Causes all pending writes to be executed. Ensures that the header, metadata, and index blocks are updated.
        /// </summary>
        public void Flush()
        {


            // Execute pending writes
            while (this.pendingWrites.Count > 0)
                this.ExecuteWrite(this.pendingWrites.Dequeue());

            if (this.indexTableModified)
                this.WriteIndexTable(true);

            this.indexTableModified = false;

            if (this.metadataModified)
                this.WriteMetadata(true);

            this.metadataModified = false;

            if (this.referenceListModified)
                this.WriteReferenceList(true);

            this.referenceListModified = false;

            if (this.headerModified)
                this.WriteHeader(true);

            this.headerModified = false;

            // Flush underlying stream.
            this.stream.Flush();
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
            this.Flush();
            this.stream.Close();
        }
        private void ExecuteWrite(PendingWrite write)
        {
            long offset;
            if (write.Offset < 0)
                offset = this.stream.Seek(0, SeekOrigin.End);
            else
                offset = this.stream.Seek(write.Offset, SeekOrigin.Begin);
            this.stream.Write(write.Data, 0, write.Data.Length);

            if (write.Callback != null)
                write.Callback(offset);
        }

        /// <summary>
        /// Returns the absolute offset where a block of the specified length can be written to without overwriting any existing data.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private long GetNextContiguousBlock(long length)
        {
            return stream.Length;
        }

        public void ReadHeader()
        {
            DataBuffer buffer = new DataBuffer(Package.HeaderSize, ByteOrder.BigEndian);
            this.stream.Seek(0, SeekOrigin.Begin);
            this.stream.Read(buffer.Bytes, 0, buffer.Length);

            // Offset | Field Name          | Size
            // -------+---------------------+------
            //   0x00 | FourCC              | 4
            var fourCC = buffer.ReadInt32();
            //   0x04 | Format Version      | 1
            var formatVersion = buffer.ReadByte();
            //   0x05 | Application Code    | 1
            var applicationCode = buffer.ReadByte();
            //   0x06 | Package Flags       | 2
            var flags = (PackageFlags)buffer.ReadUInt16();
            //   0x08 | Package UniqueID    | 16
            var uniqueID = buffer.ReadGuid();
            //   0x18 | Version             | 16
            var version = buffer.ReadVersion();
            //  0x28 | Index Offset         | 8
            var indexOffset = buffer.ReadInt64();
            //  0x30 | Metadata Offset      | 8
            var metaOffset = buffer.ReadInt64();
            //  0x38 | Ref List Offset      | 8
            var refListOffset = buffer.ReadInt64();
            //  0x40 | Package MD5          | 16
            var md5sum = buffer.ReadBytes(16);

            // TODO: validate decoded values.
        }

        private void ReadIndex()
        {

        }

        private void ReadMetadata()
        {
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
             *   0x20 | Flags               | 2
             *   0x21 | Name (length)       | 1
             *   0x22 | Name                | 0-255
             * 0x22+N | Signature (length)  | 1
             * 0x23+N | Signature           | 0-255
             */
        }

        private void WriteHeader()
        {
            this.WriteHeader(false);
        }

        private void WriteHeader(bool immediate)
        {
            DataBuffer buffer = new DataBuffer(Package.HeaderSize, ByteOrder.BigEndian);

            // Offset | Field Name          | Size
            // -------+---------------------+------
            //   0x00 | FourCC              | 4
            buffer.WriteInt32(Package.FourCC);
            //   0x04 | Format Version      | 1
            buffer.WriteByte(1);
            //   0x05 | Application Code    | 1
            buffer.WriteByte(0);
            //   0x06 | Package Flags       | 2
            buffer.WriteUInt16((ushort)this.Flags);
            //   0x08 | Package UniqueID    | 16
            buffer.WriteGuid(this.id);
            //   0x18 | Package Version     | 16
            buffer.WriteVersion(this.version);
            //   0x28 | Offset to Index     | 8
            buffer.WriteInt64(this.indexTableOffset);
            //   0x30 | Offset to Metadata  | 8
            buffer.WriteInt64(this.metadataOffset);
            //   0x38 | Ref List Offset     | 8
            buffer.WriteInt64(this.referenceListOffset);
            //   0x40 | Package MD5         | 16
            buffer.WriteBytes(new byte[16]);

            var write = new PendingWrite(buffer.Bytes, 0);
            if (immediate)
                this.ExecuteWrite(write);
            else
                this.pendingWrites.Enqueue(write);
        }

        private void WriteIndexTable()
        {
            this.WriteIndexTable(false);
        }

        private void WriteIndexTable(bool immediate)
        {


            var header = new DataBuffer(8, ByteOrder.BigEndian);
            header.WriteInt64(this.indexTableOffset);

            var write = new PendingWrite(header.Bytes, 0x28);
            if (immediate)
                this.ExecuteWrite(write);
            else
                this.pendingWrites.Enqueue(write);
        }

        private void WriteMetadata()
        {
            this.WriteMetadata(false);
        }

        private void WriteMetadata(bool immediate)
        {
            var nameBytes = Encoding.UTF8.GetBytes(this.Name);
            var authorBytes = Encoding.UTF8.GetBytes(this.Author);
            var copyrightBytes = Encoding.UTF8.GetBytes(this.Copyright);
            var companyBytes = Encoding.UTF8.GetBytes(this.Company);
            var summaryBytes = Encoding.UTF8.GetBytes(this.Summary);
            byte[] homepageBytes;
            if (this.Homepage != null)
                homepageBytes = Encoding.UTF8.GetBytes(this.Homepage.ToString());
            else
                homepageBytes = new byte[0];

            var length = 8;

            length += Math.Min(nameBytes.Length, 255);
            length += Math.Min(authorBytes.Length, 255);
            length += Math.Min(copyrightBytes.Length, 255);
            length += Math.Min(companyBytes.Length, 255);

            length += Math.Min(summaryBytes.Length, 65535);
            length += Math.Min(homepageBytes.Length, 65535);

            DataBuffer buffer = new DataBuffer(length, ByteOrder.BigEndian);

            buffer.WriteByte((byte)nameBytes.Length);
            buffer.WriteBytes(nameBytes, 0, (byte)nameBytes.Length);

            buffer.WriteByte((byte)authorBytes.Length);
            buffer.WriteBytes(authorBytes, 0, (byte)authorBytes.Length);

            buffer.WriteByte((byte)copyrightBytes.Length);
            buffer.WriteBytes(copyrightBytes, 0, (byte)copyrightBytes.Length);

            buffer.WriteByte((byte)companyBytes.Length);
            buffer.WriteBytes(companyBytes, 0, (byte)companyBytes.Length);

            buffer.WriteUInt16((ushort)summaryBytes.Length);
            buffer.WriteBytes(summaryBytes, 0, (ushort)summaryBytes.Length);

            buffer.WriteUInt16((ushort)homepageBytes.Length);
            buffer.WriteBytes(homepageBytes, 0, (ushort)homepageBytes.Length);

            var write = new PendingWrite(buffer.Bytes, -1);
            write.Callback = delegate(long offset)
            {
                this.MetadataOffset = offset;
            };

            if (immediate)
                this.ExecuteWrite(write);
            else
                this.pendingWrites.Enqueue(write);
        }

        private void WriteReferenceList()
        {
            this.WriteReferenceList(false);
        }

        private void WriteReferenceList(bool immediate)
        {
            // TODO: Add support for package references/dependencies.
            //var length = 0;

            //foreach (var entry in this.references)
            //    length += 36 + Encoding.UTF8.GetByteCount(entry.Name) + entry.Signature.Length;

            //var buffer = new DataBuffer(length, ByteOrder.BigEndian);
        }


        #endregion
        #region Types
        internal struct Block
        {
            internal Block(long offset, long length)
            {

            }
        }

        internal sealed class PendingWrite
        {
            internal PendingWrite(byte[] data)
                : this(data, -1)
            {

            }
            internal PendingWrite(byte[] data, long offset)
            {
                this.Data = data;
                this.Offset = offset;
            }
            public byte[] Data
            {
                get;
                set;
            }
            public long Offset
            {
                get;
                set;
            }
            public WriteCallback Callback
            {
                get;
                set;
            }
        }

        internal delegate void WriteCallback(long offset);
        #endregion
    }
}
