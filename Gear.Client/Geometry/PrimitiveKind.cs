/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
namespace Gear.Modeling
{
    public enum PrimitiveKind
    {
        Point = 0x0,
        Line = 0x1,
        LineStrip = 0x3,
        LineLoop = 0x2,
        Triangle = 0x4,
        TriangleStrip = 0x5,
        TriangleFan = 0x6,
        Quad = 0x7,
        QuadStrip = 0x8,
        Polygon = 0x9,
    }
}
