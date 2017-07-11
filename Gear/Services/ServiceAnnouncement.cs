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

namespace Gear.Services
{
    [ProtoContract]
    public class ServiceAnnouncement 
    {
        [ProtoMember(1)]
        public ulong AnnounceId { get; set; }
        [ProtoMember(2)]
        public Guid ClusterId { get; set; }
        [ProtoMember(3)]
        public string Version { get; set; }
        [ProtoMember(4)]
        public ServiceInfo[] Services { get; set; }
    }
}
