﻿/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using Gear.Commands;

namespace Gear
{
    /// <summary>
    /// Enumerates built in commands.
    /// </summary>
    /// <remarks>
    /// Command ID is not implemented as an enumeration because it is an open set.
    /// </remarks>
    public static class CommandId
    {
        /// <summary>
        /// The command id is unknown. Usually indicates an error.
        /// </summary>
        public const ushort Unknown = 0x0;
        /// <summary>
        /// See <see cref="HelpCommand"/>.
        /// </summary>
        public const ushort Help = 0x1;
        /// <summary>
        /// See <see cref="SetCommand"/>.
        /// </summary>
        public const ushort Set = 0x2;
        /// <summary>
        /// See <see cref="QuitCommand"/>.
        /// </summary>
        public const ushort Quit = 0x3;
        /// <summary>
        /// See <see cref="CommentCommand"/>.
        /// </summary>
        public const ushort Comment = 0x4;
    }
}