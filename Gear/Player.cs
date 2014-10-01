/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2014 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear
{
    /// <summary>
    /// Represents a player.
    /// </summary>
    public class Player
    {
        #region Properties
        public string Name
        {
            get;
            set;
        }
        public bool IsLocalPlayer
        {
            get;
            set;
        }
        public bool IsBot
        {
            get;
            set;
        }
        public Guid ClientID
        {
            get;
            set;
        }
        #endregion
    }
}
