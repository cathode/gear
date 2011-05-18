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
