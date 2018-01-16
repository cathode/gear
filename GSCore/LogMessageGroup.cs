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

namespace GSCore
{
    public enum LogMessageGroup
    {
        None = 0x00,

        /// <summary>
        /// Indicates that the event is a critical failure. This is typically reserved for situations where the program execution must be halted.
        /// </summary>
        Critical = 0x01,

        /// <summary>
        /// Indicates a severe event or situation. This is typically used for situations where an in-progress action is halted or aborted,
        /// and where loss of data or work is likely.
        /// </summary>
        Severe = 0x02,

        /// <summary>
        /// Indiciates an important problem. This is typically used for situations where an action could not be carried out,
        /// although the program and/or user expected to be able to.
        /// </summary>
        Important = 0x04,

        /// <summary>
        /// Indicates that the event or message contains information useful to anyone.
        /// </summary>
        Normal = 0x08,

        /// <summary>
        /// Indicates that the event or message contains information useful to an administrator.
        /// </summary>
        Informational = 0x10,

        /// <summary>
        /// Indicates that the event or message contains debugging information useful only to developers.
        /// </summary>
        Debug = 0x20,

        All = 0xFF
    }
}
