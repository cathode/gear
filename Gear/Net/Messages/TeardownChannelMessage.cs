﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.Messages
{
    [ProtoContract]
    public class TeardownChannelMessage : IMessage
    {

        [ProtoMember(1)]
        public bool Confirmation { get; set; }

        [ProtoIgnore]
        public ushort DispatchId
        {
            get { return Ids.TeardownChannel; }
        }

        [ProtoIgnore]
        public bool IsBroadcastMessage
        {
            get { return false; }
        }
    }
}
