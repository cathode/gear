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
    public class TransferFileMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return Ids.TransferFile;
            }
        }

        [ProtoMember(1)]
        public string Name { get; set; }

        [ProtoMember(2)]
        public string SourcePath { get; set; }

        [ProtoMember(3)]
        public long Length { get; set; }

        [ProtoMember(4)]
        public int CRC32 { get; set; }

        [ProtoMember(5)]
        public Guid FileId { get; set; }

        [ProtoMember(6)]
        public int ChunkSize { get; set; }

        [ProtoIgnore]
        public int ChunkCount
        {
            get
            {
                return (int)Math.Ceiling((double)this.Length / this.ChunkSize);
            }
        }
    }
}
