using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.ShellCommands
{
    /// <summary>
    /// Represents a shell command that display information on other commands.
    /// </summary>
    public sealed class HelpCommand : GShellCommand
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpCommand"/> class.
        /// </summary>
        /// <param name="shell"></param>
        public HelpCommand()
        {
        }
        #endregion
        #region Properties

        public override int MaxArgs
        {
            get
            {
                return -1;
            }
        }

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
        /// Overridden. Executes the current shell command.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool Execute(GShell shell, string data, UserCredentials credentials)
        {
            var cmdName = (data ?? string.Empty).Trim();
            
            if (cmdName == string.Empty)
                cmdName = this.Name;

            var cmd = shell.GetRegisteredCommand(cmdName);
            if (cmd != null)
                shell.Write(string.Format("{0}: {1}\r\n - Usage: {2}", cmd.Name, cmd.Description, cmd.Usage));

            return true;
        }
        #endregion
    }
}
