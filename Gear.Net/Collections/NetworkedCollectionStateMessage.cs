using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.Collections
{
    public class NetworkedCollectionStateMessage : IMessage
    {
        int IMessage.DispatchId
        {
            get
            {
                return BuiltinMessageIds.NetworkedCollectionAction;
            }
        }

        [ProtoMember(1)]
        public NetworkedCollectionStateAction Action { get; set; }
    }
}
