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

namespace Gear.Client.Platform.Microsoft
{
    /// <summary>
    /// Represents a <see cref="DisplayAdapter"/> on a PC running Microsoft Windows operating system.
    /// </summary>
    public sealed class WindowsDisplayAdapter : DisplayAdapter
    {
        public override DisplayDevice PrimaryDevice
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override DisplayDevice[] GetDevices()
        {
            throw new NotImplementedException();
        }
    }
}
