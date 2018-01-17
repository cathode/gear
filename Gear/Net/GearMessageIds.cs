using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    public static class GearMessageIds
    {
        #region Game Resources - 0x1000-0x10FF
        public static readonly int BlockUpdate = 0x1000;
        public static readonly int ZoneDataRequest = 0x1001;
        #endregion

        #region Game Engine - 0x1100-0x11FF
        public static readonly int LocatorRequest = 0x1100;
        public static readonly int LocatorResponse = 0x1101;
        #endregion
    }
}
