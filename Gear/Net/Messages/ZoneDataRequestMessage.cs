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
using ProtoBuf;

namespace Gear.Net.Messages
{
    /// <summary>
    /// Represents a request from one peer to fetch the block data of a zone.
    /// </summary>
    public class ZoneDataRequestMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return Ids.ZoneDataRequest;
            }
        }

        public long ZoneX { get; set; }
        public long ZoneY { get; set; }
        public long ZoneZ { get; set; }

        public byte ChunkX { get; set; }
        public byte ChunkY { get; set; }
        public byte ChunkZ { get; set; }
    }
}
