// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using GearEngine.Commands;

namespace GearEngine
{
    /// <summary>
    /// Enumerates built in commands.
    /// </summary>
    public enum CommandId
    {
        /// <summary>
        /// If a command id is unknown. Usually indicates an error.
        /// </summary>
        Unknown = 0x0,
        /// <summary>
        /// See <see cref="HelpCommand"/>.
        /// </summary>
        Help = 0x1,
        /// <summary>
        /// See <see cref="SetCommand"/>.
        /// </summary>
        Set = 0x2,
        /// <summary>
        /// See <see cref="QuitCommand"/>.
        /// </summary>
        Quit = 0x3,
    }
}
