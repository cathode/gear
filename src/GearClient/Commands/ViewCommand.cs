/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using Gear.Commands;

namespace Gear.Client.Commands
{
    internal sealed class ViewCommand : ShellCommand
    {
        public override void ParseData(string data)
        {
            throw new NotImplementedException();
        }

        public override string Name
        {
            get
            {
                return "View";
            }
        }

        public override ushort Id
        {
            get
            {
                return ClientCommandIds.ViewCommand;
            }
        }

        public override void Execute(Gear.GameEngine engine)
        {
            throw new NotImplementedException();
        }
    }
}
