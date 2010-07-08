﻿/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */ using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Client
{
    public static class ClientCommandIds
    {
        // General client commands - starts at 0xC000
        public const ushort ConnectCommand = 0xC000;

        // Renderer commands - Starts at 0xC100
        public const ushort ViewCommand = 0xC100;
    }
}