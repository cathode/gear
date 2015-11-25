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

namespace Gear.Services
{
    /// <summary>
    /// Implements a service that brokers connections from 
    /// </summary>
    public class ConnectionBrokerService : ServiceBase
    {

        public static readonly ushort DefaultServicePort = 14122;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionBrokerService"/> class.
        /// </summary>
        /// <param name="port"></param>
        public ConnectionBrokerService(ushort port)
        {
            if (port == 0)
                port = ConnectionBrokerService.DefaultServicePort;

            this.ListenPort = port;
        }
    }
}
