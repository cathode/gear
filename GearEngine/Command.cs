/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gear.Commands;

namespace Gear
{
    /// <summary>
    /// Represnts an action that alters the game session.
    /// </summary>
    public abstract class Command
    {
        #region Methods
        /// <summary>
        /// When overridden in a derived class, performs the functionality associated with the current command.
        /// </summary>
        /// <param name="engine"></param>
        public abstract void Execute(GameEngine engine);
        #endregion
        #region Properties
        /// <summary>
        /// When implemented in a derived class, gets the <see cref="CommandId"/> of the current <see cref="Command"/>.
        /// </summary>
        public abstract ushort Id
        {
            get;
        }
        #endregion
    }
}
