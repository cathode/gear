﻿/******************************************************************************
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

namespace Gear.Net.Messages
{
    [ProtoContract]
    public class EndPointGreetingMessage : IMessage
    {
        [ProtoMember(1)]
        public Guid EndPointId { get; set; }

        [ProtoMember(2)]
        public EndPointKind Kind { get; set; }
        //public 

        public int DispatchId
        {
            get { return 0; }
        }
    }
}
