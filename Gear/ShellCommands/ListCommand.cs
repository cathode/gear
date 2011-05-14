using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.ShellCommands
{
    public sealed class ListCommand : GShellCommand
    {
        #region Constructors
        
        #endregion
        public override int MaxArgs
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinArgs
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public override bool Execute(GShell shell, string data, UserCredentials credentials)
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("Commands:");

            foreach (var cmd in shell.RegisteredCommands)
            {
                output.AppendLine(string.Format("- {0}: Usage: {1}", cmd.Name, cmd.Usage));
            }

            return true;
        }
    }
}
