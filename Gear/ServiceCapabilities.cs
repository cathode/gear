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

namespace Gear
{
    /// <summary>
    /// Represents the hardware or host resources of a service EndPoint.
    /// </summary>
    [ProtoContract]
    public class ServiceCapabilities
    {
        [ProtoMember(0)]
        public Guid ServiceEndPointId { get; set; }

        [ProtoMember(1)]
        public ushort CoreCount { get; set; }

        [ProtoMember(2)]
        public ushort ThreadCount { get; set; }

        [ProtoMember(3)]
        public ulong MemoryAvilable { get; set; }

        [ProtoMember(4)]
        public string OperatingSystem { get; set; }
    }
}
