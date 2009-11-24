// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Commands
{
    /// <summary>
    /// Represents a command that assigns or alters a session variable.
    /// </summary>
    public sealed class SetCommand : ShellCommand
    {
        #region Methods
        /// <summary>
        /// Overridden. Executes the current <see cref="SetCommand"/>.
        /// </summary>
        /// <param name="engine"></param>
        public override void Execute(GameEngine engine)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Overridden. Parses the data and assimilates it.
        /// </summary>
        /// <param name="data">The data to parse.</param>
        public override void ParseData(string data)
        {
            //if (string.IsNullOrEmpty(data))
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
                return CommandId.Set;
            }
        }

        /// <summary>
        /// Overridden. Gets the command name.
        /// </summary>
        public override string Name
        {
            get
            {
                return "Set";
            }
        }

        #endregion
    }
}
