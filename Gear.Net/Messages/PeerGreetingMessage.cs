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
using ProtoBuf;

namespace Gear.Net.Messages
{
    /// <summary>
    /// Represents a greeting sent to a peer. The receiving side should reply with a greeting message.
    /// </summary>
    [ProtoContract]
    public class PeerGreetingMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return BuiltinMessageIds.PeerGreeting;
            }
        }

        [ProtoMember(1)]
        public PeerMetadata Metadata { get; set; }

        [ProtoMember(2)]
        public bool IsResponseRequested { get; set; }
    }
}
