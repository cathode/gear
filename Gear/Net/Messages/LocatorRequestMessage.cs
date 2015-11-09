using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;


namespace Gear.Net.Messages
{
    [ProtoContract]
    public class LocatorRequestMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get { return Ids.LocatorRequest; }
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
