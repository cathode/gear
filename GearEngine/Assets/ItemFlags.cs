using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Assets
{
    [Flags]
    public enum ItemFlags
    {
        None = 0x0,
        Equippable = 0x1,
        Upgradable = 0x2,
    }
}
