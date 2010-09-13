/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
