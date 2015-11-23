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

namespace Gear.Model
{

    [Flags]
    public enum BlockFlags : byte
    {
        /// <summary>
        /// Indicates the block has no flags specified.
        /// </summary>
        None = 0x00,

        /// <summary>
        /// Indicates the b
        /// </summary>
        SmoothX = 0x01,
        SmoothY = 0x02,
        SmoothZ = 0x04,
    }
}
