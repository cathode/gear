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
    public sealed class SayCommand : GShellCommand
    {
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
                return 1;
            }
        }

        public override bool Execute(GShell shell, string data, PlayerCredentials credentials)
        {
            return false;
        }
    }
}
