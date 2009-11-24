/* Copyright © 2009 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using Gear.Commands;

namespace Gear.Client.Commands
{
    internal sealed class ConnectCommand : ShellCommand
    {
        public override void ParseData(string data)
        {
            throw new NotImplementedException();
        }

        public override string Name
        {
            get
            {
                return "Connect";
            }
        }

        public override ushort Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void Execute(GameEngine engine)
        {
            throw new NotImplementedException();
        }
    }
}
