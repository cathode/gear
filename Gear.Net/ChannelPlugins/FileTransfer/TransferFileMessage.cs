using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Net;
using Gear.Net.Messages;
using ProtoBuf;

namespace Gear.Net.ChannelPlugins.FileTransfer
{
    [ProtoContract]
    public class TransferFileMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return BuiltinMessageIds.TransferFile;
            }
        }

        [ProtoMember(1)]
        public Guid TransferId { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public string SourcePath { get; set; }

        [ProtoMember(4)]
        public long Length { get; set; }

        [ProtoMember(5)]
        public int CRC32 { get; set; }

        [ProtoMember(6)]
        public Dictionary<string, string> ExtendedAttributes { get; set; }
    }
}
