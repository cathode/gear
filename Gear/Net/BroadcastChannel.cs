/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2014 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Gear.Net
{
    /// <summary>
    /// Provides a <see cref="Channel"/> implementation that sends and receives broadcast packets on the local LAN.
    /// </summary>
    public class BroadcastChannel : Channel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BroadcastChannel"/> class.
        /// </summary>
        /// <param name="port"></param>
        public BroadcastChannel(ushort port)
        {

        }

        private UdpClient endpoint;

        protected override System.IO.Stream GetMessageDestinationStream()
        {
            throw new NotSupportedException();
        }

        protected override void SendMessage(MessageContainer mc)
        {
            throw new NotImplementedException();
        }
    }
}
