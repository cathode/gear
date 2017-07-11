using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;


namespace Gear.Net.Collections
{
    [ProtoContract]
    public class NetworkedCollectionUpdateMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return Gear.Net.Messages.BuiltinMessageIds.NetworkedCollectionUpdate;
            }
        }

        /// <summary>
        /// Gets or sets a value that determines which collection the update applies to.
        /// </summary>
        [ProtoMember(1)]
        public long CollectionId { get; set; }

        /// <summary>
        /// Gets or sets the update action.
        /// </summary>
        [ProtoMember(2)]
        public NetworkedCollectionAction Action { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ProtoMember(3)]
        public string Data { get; set; }

    }
}
