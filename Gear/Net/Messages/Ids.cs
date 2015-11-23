/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
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
        // System

        public static readonly ushort TeardownChannel = 0x9000;
        public static readonly ushort LocatorRequest = 0x9001;
        public static readonly ushort LocatorResponse = 0x9002;

        public static readonly int BlockUpdate = 0x1000;

        public static readonly int ZoneDataRequest = 0x2005;
    }
}
