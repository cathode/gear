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
    public class Player
    {
        #region Fields
        private Guid clientID;
        #endregion
        #region Properties
        public Guid ClientID
        {
            get
            {
                return this.clientID;
            }
            set
            {
                this.clientID = value;
            }
        }
        #endregion
    }
}
