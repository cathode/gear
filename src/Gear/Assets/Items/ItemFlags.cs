/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Gear.Assets.Items
{
    [Flags]
    public enum ItemFlags
    {
        None = 0x0,
        Equippable = 0x1,
        Upgradable = 0x2,
    }
}
