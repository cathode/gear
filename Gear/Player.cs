/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
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
