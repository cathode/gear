using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.Messages
{
    [ProtoContract]
    public class PeerHandoffMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return BuiltinMessageIds.PeerHandoff;
            }
        }

        [ProtoMember(1)]
        public string TargetHostname { get; set; }

        [ProtoMember(2)]
        public ushort TargetPort { get; set; }
    }
}
