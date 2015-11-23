using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.Messages
{
    [ProtoContract]
    public class LocatorResponseMessage : IMessage
    {

        [ProtoMember(1)]
        public Gear.Services.ServiceInfo[] Services { get; set; }

        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get { return Ids.LocatorResponse; }

        }

        public bool IsBroadcastMessage
        {
            get
            {
                return false;
            }
        }
    }
}
