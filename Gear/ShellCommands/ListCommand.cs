/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
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
        public override bool Execute(GShell shell, string data, PlayerCredentials credentials)
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
