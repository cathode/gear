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
using ProtoBuf;

namespace Gear.Net
{
    [ProtoContract]
    public class MessageContainer
    {
        public MessageContainer(IMessage contents)
        {
            this.DispatchId = contents.DispatchId;
        }

        [ProtoMember(1)]
        public int DispatchId { get; set; }

        [ProtoMember(2)]
        public IMessage Contents { get; set; }
    }
}
