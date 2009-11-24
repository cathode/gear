// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearEngine;
using GearEngine.Commands;

namespace GearClient.Commands
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
