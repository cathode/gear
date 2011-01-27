/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/

namespace Rust
{
    /// <summary>
    /// Indicates the byte ordering of a value that is stored as a series of bytes.
    /// </summary>
    public enum ByteOrder
    {
        /// <summary>
        /// Indicates the data buffer will use the system's native endianness.
        /// </summary>
        System = 0x0,

        /// <summary>
        /// Indicates the data buffer will read/write values as little-endian.
        /// </summary>
        LittleEndian,

        /// <summary>
        /// Indicates the data buffer will read/write values as big-endian.
        /// </summary>
        BigEndian,

        /// <summary>
        /// Indicates the data buffer will read/write values as network byte order (big-endian).
        /// </summary>
        NetworkByteOrder = BigEndian,

        /// <summary>
        /// Indicates the data buffer will read/write values as host byte order (system endianness).
        /// </summary>
        HostByteOrder = System,
    }
}
