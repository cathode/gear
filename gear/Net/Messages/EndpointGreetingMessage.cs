using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.Messages
{
    [ProtoContract]
    public class EndPointGreetingMessage
    {
        [ProtoMember(0)]
        public Guid EndPointId { get; set; }

        [ProtoMember(1)]
        public EndPointKind Kind { get; set; }
        //public 
    }
}
