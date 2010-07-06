/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Commands
{
    /// <summary>
    /// Provides shared functionality for commands that can be invoked from the game shell.
    /// </summary>
    public abstract class ShellCommand : Command
    {
        #region Methods

        /// <summary>
        /// When overridden in a derived class, parses the data in string form and assimilates it into the current shell command.
        /// </summary>
        /// <param name="data">The data to parse.</param>
        public abstract void ParseData(string data);

        #endregion
        #region Properties

        /// <summary>
        /// Indicates if the current shell command is only used within the user-interactive shell.
        /// </summary>
        /// <remarks>
        /// Shell-only commands bypass the normal command queue target of the game shell.
        /// </remarks>
        public virtual bool IsShellOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Indicates if the results of the shell command should be displayed to the shell.
        /// </summary>
        public virtual bool GeneratesShellOutput
        {
            get
            {
                return true;
            }
        }

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
