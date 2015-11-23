﻿/******************************************************************************
 * Gear.Client: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;

namespace Gear.Client.Geometry
{
    [Flags]
    public enum VertexFlags : int
    {
        None = 0x0,
        VertexColor = 0x1,
        HideOutgoingEdges = 0x2,
    }
}
