using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Rust.Assets
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = Package.HeaderSize)]
    public struct PackageHeader
    {
        [FieldOffset(0x00)]
        public int FourCC;
        [FieldOffset(0x04)]
        public byte FormatVersion;
        [FieldOffset(0x08)]
        public Guid Id;
        [FieldOffset(0x18)]
        public Version PackageVersion;
        [FieldOffset(0x28)]
        public long IndexTableOffset;
        [FieldOffset(0x30)]
        public PackageFlags Flags;
        [FieldOffset(Package.HeaderSize - 4)]
        public int HeaderCRC32;
    }
}
