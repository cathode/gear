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
using System.Runtime.InteropServices;

namespace Gear.Client.Platform.Microsoft.OpenGL
{
    public static class WGL
    {
        #region Fields
        public const string DLL = "OpenGL32.dll";

        #endregion
        #region Methods
        [DllImport(WGL.DLL, EntryPoint = "wglCreateContext")]
        public static extern IntPtr CreateContext(IntPtr deviceContext);

        [DllImport(WGL.DLL, EntryPoint = "wglDeleteContext")]
        public static extern bool DeleteContext(IntPtr renderingContext);

        [DllImport(WGL.DLL, EntryPoint = "wglMakeCurrent")]
        public static extern bool MakeCurrent(IntPtr deviceContext, IntPtr renderingContext);
        #endregion
    }
}
