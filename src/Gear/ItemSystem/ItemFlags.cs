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
    [Flags]
    public enum ItemFlags
    {
        None = 0x0,
        Equippable = 0x1,
        Upgradable = 0x2,
    }
}
