// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using GearEngine.Commands;

namespace GearEngine
{
    /// <summary>
    /// Enumerates built in commands.
    /// </summary>
    public static class CommandId
    {
        /// <summary>
        /// If a command id is unknown. Usually indicates an error.
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
