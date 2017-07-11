/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;

namespace Gear.Geometry
{
    [Flags]
    public enum VertexFlags : int
    {
        None = 0x0,
        VertexColor = 0x1,
        HideOutgoingEdges = 0x2,
    }
}
