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
                return BuiltinMessageIds.NetworkedCollectionQueryResponse;
            }
        }

        /// <summary>
        /// Gets or sets an array of the collection id
        /// </summary>
        public long[] CollectionIds { get; set; }
    }
}
