using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Net;
using Gear.Net.Messages;
using ProtoBuf;

namespace DB2Library.Net.Messages
{
    [ProtoContract]
    public class TransferFileDataMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return Ids.TransferFileData;
            }
        }
        
        [ProtoMember(1)]
        public Guid FileId { get; set; }

        [ProtoMember(2)]
        public int ChunkId { get; set; }

        [ProtoMember(3)]
        public int Length { get; set; }

        [ProtoMember(4)]
        public byte[] Bytes { get; set; }

        
    }
}
