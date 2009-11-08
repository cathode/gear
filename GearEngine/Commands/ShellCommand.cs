// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearEngine.Commands
{
    /// <summary>
    /// Provides shared functionality for commands that can be invoked from the game shell.
    /// </summary>
    public abstract class ShellCommand : Command
    {
        #region Properties
        /// <summary>
        /// When overridden in a derived class, gets the name of the current command.
        /// </summary>
        public abstract string Name
        {
            get;
        }
        /// <summary>
        /// Gets the localized help string that describes the operation of the current command to the user.
        /// </summary>
        public virtual string HelpInfo
        {
            get
            {
                return EngineResources.ResourceManager.GetString("ShellCmdHelp_" + this.Name);
            }
        }
        #endregion
    }
}
