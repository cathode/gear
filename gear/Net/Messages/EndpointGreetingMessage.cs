using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.Messages
{
    [ProtoContract]
    public class EndPointGreetingMessage : IMessage
    {
        [ProtoMember(1)]
        public Guid EndPointId { get; set; }

        [ProtoMember(2)]
        public EndPointKind Kind { get; set; }
        //public 

        public int DispatchId
        {
            get { return 0; }
        }
    }
}
