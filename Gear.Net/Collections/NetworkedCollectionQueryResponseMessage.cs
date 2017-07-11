using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.Collections
{
    [ProtoContract]
    public class NetworkedCollectionQueryResponseMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return Gear.Net.Messages.BuiltinMessageIds.NetworkedCollectionQueryResponse;
            }
        }

        public long[] CollectionIds { get; set; }
    }
}
