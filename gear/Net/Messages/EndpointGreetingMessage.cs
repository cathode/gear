using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.Messages
{
    [ProtoContract]
    public class EndpointGreetingMessage
    {
        [ProtoMember(0)]
        public Guid EndpointId { get; set; }

        [ProtoMember(1)]
        public EndpointKind Kind { get; set; }
        //public 
    }

    public enum EndpointKind
    {
        Client,
        Service,
    }
}
