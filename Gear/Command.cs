/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;

using System.Text;
using Gear.Commands;

namespace Gear
{
    /// <summary>
    /// Represnts an action that alters the game session.
    /// </summary>
    public abstract class Command
    {
        #region Properties
        /// <summary>
        /// When implemented in a derived class, gets the <see cref="CommandId"/> of the current <see cref="Command"/>.
        /// </summary>
        public abstract ushort Id
        {
            get;
        }
        #endregion
        #region Methods
        /// <summary>
        /// When overridden in a derived class, performs the functionality associated with the current command.
        /// </summary>
        /// <param name="engine"></param>
        public abstract void Execute(GameEngine engine);
        #endregion
    }
}
