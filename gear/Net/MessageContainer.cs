﻿using System;
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