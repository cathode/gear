// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearEngine
{
    /// <summary>
    /// Represnts an action that alters the game session.
    /// </summary>
    public abstract class Command
    {
        #region Methods
        //public abstract bool Execute(GameSession session);
        #endregion
        #region Properties
        /// <summary>
        /// When implemented in a derived class, gets the <see cref="CommandId"/> of the current <see cref="Command"/>.
        /// </summary>
        public abstract CommandId Id
        {
            get;
        }
        #endregion
    }
}
