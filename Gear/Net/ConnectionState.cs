﻿/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/

namespace Gear.Net
{
    /// <summary>
    /// Represents states of a client-to-server or server-to-client network connection.
    /// </summary>
    public enum ConnectionState
    {
        Disconnected = 0x0,
        Connecting,
        Connected,
        Unresponsive,
        Closed,
    }
}
