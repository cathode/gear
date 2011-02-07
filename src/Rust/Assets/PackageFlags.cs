/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;

namespace Rust.Assets
{
    /// <summary>
    /// Represents flags associated with a game package.
    /// </summary>
    [Flags]
    public enum PackageFlags : ushort
    {
        /// <summary>
        /// Indicates no flags are set.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Indicates the package contains generated content (for example a saved game).
        /// </summary>
        Generated = 0x1,

        /// <summary>
        /// Indicates the package is delay-signed.
        /// </summary>
        DelaySigned = 0x2,

        /// <summary>
        /// Indicates the index table for the package is located in an external file.
        /// </summary>
        ExternalIndex = 0x4
    }
}
