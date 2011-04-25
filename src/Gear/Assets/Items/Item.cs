/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Gear.Assets.Items
{
    /// <summary>
    /// Represents a game item.
    /// </summary>
    public class Item 
    {
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
        #endregion
    }
}
