// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearEngine.Commands
{
    /// <summary>
    /// Quits the game/server.
    /// </summary>
    public sealed class QuitCommand : ShellCommand
    {
        #region Methods

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
        public override CommandId Id
        {
            get
            {
                throw new NotImplementedException();
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
