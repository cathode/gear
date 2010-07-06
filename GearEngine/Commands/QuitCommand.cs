/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Commands
{
    /// <summary>
    /// Quits the game/server.
    /// </summary>
    public sealed class QuitCommand : ShellCommand
    {
        #region Methods
        /// <summary>
        /// Overridden. Executes the current <see cref="QuitCommand"/>.
        /// </summary>
        /// <param name="engine"></param>
        public override void Execute(GameEngine engine)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Overridden. Parses data for the current command.
        /// </summary>
        /// <param name="data"></param>
        public override void ParseData(string data)
        {
            // Do nothing.
        }

        #endregion
        #region Properties

        /// <summary>
        /// Overridden. Gets the command id.
        /// </summary>
        public override ushort Id
        {
            get
            {
                return CommandId.Quit;
            }
        }

        /// <summary>
        /// Overridden. Gets the command name.
        /// </summary>
        public override string Name
        {
            get
            {
                return "Quit";
            }
        }

        #endregion
    }
}
