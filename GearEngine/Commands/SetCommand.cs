// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearEngine.Commands
{
    /// <summary>
    /// Represents a command that assigns or alters a session variable.
    /// </summary>
    public sealed class SetCommand : ShellCommand
    {
        #region Methods

        public override void ParseData(string data)
        {
            throw new NotImplementedException();
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
