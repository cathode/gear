﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Net;
using Gear.Net.Messages;
using ProtoBuf;

namespace Gear.Net.ChannelPlugins.StreamTransfer
{
    [ProtoContract]
    public class TransferStreamMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return BuiltinMessageIds.TransferStream;
            }
        }

        [ProtoMember(1)]
        public StreamTransferState TransferState { get; set; }

        [ProtoMember(2)]
        public ushort? DataPort { get; set; }

        [ProtoMember(3)]
        public bool RequestDataPort { get; set; }
    }
}
