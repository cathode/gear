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

namespace Gear.Client.Platform.Microsoft
{
    /// <summary>
    /// Indicates supported options of a <see cref="PixelFormatDescriptor"/>.
    /// </summary>
    [CLSCompliant(false)]
    [Flags]
    public enum PfdFlags : uint
    {
        None                = 0x00000000,
        DoubleBuffer        = 0x00000001,
        Stereo              = 0x00000002,
        DrawToWindow        = 0x00000004,
        DrawToBitmap        = 0x00000008,
        SupportGDI          = 0x00000010,
        SupportOpenGL       = 0x00000020,
        GenericFormat       = 0x00000040,
        NeedPalette         = 0x00000080,
        NeedSystemPalette   = 0x00000100,
        SwapExchange        = 0x00000200,
        SwapCopy            = 0x00000400,
        SwapLayerBuffers    = 0x00000800,
        GenericAccelerated  = 0x00001000,
        SupportDirectDraw   = 0x00002000,

        // For use with ChoosePixelFormat only.
        DepthDontCare           = 0x20000000,
        DoubleBufferDontCare    = 0x40000000,
        StereoDontCare          = 0x80000000,
    }
}
