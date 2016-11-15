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
using System.Net.Sockets;

namespace Gear.Net
{
    /// <summary>
    /// Provides a <see cref="Channel"/> implementation that sends and receives broadcast packets on the local LAN.
    /// </summary>
    public class DatagramChannel : Channel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatagramChannel"/> class.
        /// </summary>
        /// <param name="port"></param>
        public DatagramChannel(ushort port)
        {

        }

        private UdpClient endpoint;

        protected override int SendMessages(Queue<IMessage> messages)
        {
            throw new NotImplementedException();
        }

        protected override void BeginBackgroundReceive()
        {
            throw new NotImplementedException();
        }

        public override System.Net.IPEndPoint LocalEndPoint
        {
            get { throw new NotImplementedException(); }
        }

        public override System.Net.IPEndPoint RemoteEndPoint
        {
            get { throw new NotImplementedException(); }
        }
    }
}
