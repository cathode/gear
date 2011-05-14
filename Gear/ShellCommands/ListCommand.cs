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
            var filter = data.Trim();
            if (filter == string.Empty)
            {
                shell.Write("Commands (all):");
                foreach (var cmd in shell.RegisteredCommands)
                {
                    shell.Write(cmd.Name);
                }
            }
            else
            {
                shell.Write("Commands (matching: \"" + filter + "\"):");
                foreach (var cmd in from c in shell.RegisteredCommands
                                    where c.Name.Contains(filter)
                                    select c)
                {
                    shell.Write(cmd.Name);
                }
            }
            return true;
        }
    }
}
