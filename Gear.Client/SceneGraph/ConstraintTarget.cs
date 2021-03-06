﻿/******************************************************************************
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

namespace Gear.Client.SceneGraph
{
    /// <summary>
    /// Enumerates supported targets for a graph node constraint.
    /// </summary>
    [Flags]
    public enum ConstraintTarget
    {
        None = 0x00,
        Position = 0x01,
        Orientation = 0x02,
        Scale = 0x04,
        All = Position | Orientation | Scale,
    }
}
