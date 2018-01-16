using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.Collections
{
    /// <summary>
    /// Representing a message conveying updates for a <see cref="NetworkedCollection{T}"/>.
    /// </summary>
    [ProtoContract]
    public class NetworkedCollectionUpdateMessage : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkedCollectionUpdateMessage"/> class.
        /// </summary>
        public NetworkedCollectionUpdateMessage()
        {
        }

        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return BuiltinMessageIds.NetworkedCollectionUpdate;
            }
        }

        /// <summary>
        /// Gets or sets a value that determines which collection the update applies to.
        /// </summary>
        [ProtoMember(1)]
        public long CollectionGroupId { get; set; }

        /// <summary>
        /// Gets or sets the update action.
        /// </summary>
        [ProtoMember(2)]
        public NetworkedCollectionUpdateAction Action { get; set; }

        [ProtoMember(3)]
        public MessageDataHint DataHints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ProtoMember(4)]
        public string Data { get; set; }

        [ProtoMember(5)]
        public byte[] DataBinary { get; set; }
    }
}
