/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
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
