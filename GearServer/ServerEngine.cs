// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearEngine;

namespace GearServer
{
    internal sealed class ServerEngine : GameEngine
    {
        #region Constructors

        internal ServerEngine()
        {
            this.RegisterCommandProcessor(CommandId.Unknown, null);
        }

        #endregion
    }
}
