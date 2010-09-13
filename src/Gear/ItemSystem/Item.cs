/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * -------------------------------------------------------------------------- *
 * Contributors:                                                              *
 * - Will 'cathode' Shelley <cathode@live.com>                                *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Gear.ItemSystem
{
    /// <summary>
    /// Represents a game item.
    /// </summary>
    public class Item 
    {
        #region Fields
        private Guid target;
        #endregion
        #region Properties
        public ItemFlags Flags
        {
            get;
            set;
        }

        public ItemKind Kind
        {
            get;
            set;
        }

        public int Cost
        {
            get;
            set;
        }
        public Guid Target
        {
            get
            {
                return this.target;
            }
            set
            {
                this.target = value;
            }
        }
        #endregion
    }
}
