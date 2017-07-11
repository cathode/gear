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
using System.Threading.Tasks;

using Gear.Net;
using Gear.Services;

namespace Gear
{
    /// <summary>
    /// Provides a state management object that allows commands to be provided
    /// by a user or admin and applied against a local or remote server.
    /// </summary>
    public class GearShell
    {
        /// <summary>
        /// Holds the connection opened to the server which commands will be executed against.
        /// </summary>
        public ConnectedChannel ServerConnection { get; set; }

        public bool Execute(string line)
        {
            return false;
        }
    }
}
