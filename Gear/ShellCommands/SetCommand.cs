/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.ShellCommands
{
    /// <summary>
    /// Represents a <see cref="GShellCommand"/> implementation that processes user requests to set engine runtime variables.
    /// </summary>
    public sealed class SetCommand : GShellCommand
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SetCommand"/> class.
        /// </summary>
        /// <param name="shell">The <see cref="GShell"/> instance that the new <see cref="GShellCommand"/> belongs to.</param>
        public SetCommand()
        {
        }
        #endregion
        #region Properties

        /// <summary>
        /// Overridden. Gets the maximum number of arguments that can be provided when the command is executed.
        /// </summary>
        public override int MaxArgs
        {
            get
            {
                return -1;
            }
        }

        /// <summary>
        /// Overridden. Gets the minimum number of arguments that need to be provided when the command is executed.
        /// </summary>
        public override int MinArgs
        {
            get
            {
                return 0;
            }
        }

        #endregion
        #region Methods
        /// <summary>
        /// Overridden. Executes the shell command using the specified data.
        /// </summary>
        /// <param name="data">Data relevant to the execution of the command.</param>
        /// <returns>true if the shell command executed successfully; otherwise false.</returns>
        public override bool Execute(GShell shell, string data, UserCredentials credentials)
        {
            Log.Write(string.Format("Set {0} to {1}", "placeholder", "placeholder"));

            return true;
        }
        #endregion
    }
}
