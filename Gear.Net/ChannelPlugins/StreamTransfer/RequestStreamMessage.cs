using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.ChannelPlugins.StreamTransfer
{
    [ProtoContract]
    public class RequestStreamMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return BuiltinMessageIds.RequestStream;
            }
        }

        [ProtoMember(1)]
        public string Name { get; set; }
    }
}
