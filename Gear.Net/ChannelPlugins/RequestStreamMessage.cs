using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.ChannelPlugins
{
    [ProtoContract]
    public class RequestStreamMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return Gear.Net.Messages.BuiltinMessageIds.RequestStream;
            }
        }

        [ProtoMember(1)]
        public string Name { get; set; }
    }
}
