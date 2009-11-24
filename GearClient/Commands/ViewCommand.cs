using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearEngine.Commands;

namespace GearClient.Commands
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

        public override void Execute(GearEngine.GameEngine engine)
        {
            throw new NotImplementedException();
        }
    }
}
