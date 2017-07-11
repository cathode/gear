/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear
{
    public enum LogMessageGroup
    {
        None = 0x00,
        Debug = 0x01,
        Info = 0x02,
        Warning = 0x04,
        Error = 0x08,

        All = 0xFF,
    }
}
