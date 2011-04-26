/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear
{
    /// <summary>
    /// Represents the kind of data that a <see cref="Field"/> holds.
    /// </summary>
    public enum FieldKind : byte
    {
        /// <summary>
        /// Indicates the <see cref="Field"/> contains some custom type of data.
        /// </summary>
        Custom = 0,

        /// <summary>
        /// Indicates the <see cref="Field"/> contains a Globally Unique Identifier (GUID).
        /// </summary>
        Guid,
        
        /// <summary>
        /// Indicates the <see cref="Field"/> contains a string.
        /// </summary>
        String,

        /// <summary>
        /// Indicates the <see cref="Field"/> contains a <see cref="Version"/>.
        /// </summary>
        Version,
    }
}
