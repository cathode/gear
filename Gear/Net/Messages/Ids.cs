using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.Messages
{
    /// <summary>
    /// Contains static definitions of message dispatch id's for 1st party message types.
    /// </summary>
    public static class Ids
    {
        // Meta / infrastructure
        public static readonly ushort TeardownChannel = 0x1000;
        public static readonly ushort LocatorRequest = 0x1001;
        public static readonly ushort LocatorResponse = 0x1002;
        

        // Data exchange


        // Client->server updates
        public static readonly ushort ZoneDataRequest = 0x3000;

        // Server->client updates
        public static readonly ushort ZoneDataResponse = 0x4000;
        public static readonly ushort BlockUpdate = 0x4001;
    }
}
