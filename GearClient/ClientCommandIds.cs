using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearClient
{
    public static class ClientCommandIds
    {
        // General client commands - starts at 0xC000
        public const ushort ConnectCommand = 0xC000;

        // Renderer commands - Starts at 0xC100
        public const ushort ViewCommand = 0xC100;
    }
}
