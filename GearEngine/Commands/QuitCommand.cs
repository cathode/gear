// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearEngine.Commands
{
    /// <summary>
    /// Quits the game/server.
    /// </summary>
    public sealed class QuitCommand : Command
    {
        public override bool Execute(GameSession session)
        {
            session.Dispose();

            return true;
        }

        public override string Name
        {
            get
            {
                return "quit";
            }
        }

        public override string Description
        {
            get
            {
                return EngineResources.CommandQuitDescription;
            }
        }
    }
}
