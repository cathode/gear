using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Gear.ShellCommands
{
    public sealed class ExitCommand : GShellCommand
    {
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

        public override bool Execute(GShell shell, string data, PlayerCredentials credentials)
        {
            // TODO: Graceful shutdown of engine.

            Process.GetCurrentProcess().Kill();

            return false;
        }
    }
}
