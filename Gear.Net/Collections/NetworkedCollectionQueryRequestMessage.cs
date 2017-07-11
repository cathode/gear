using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;


namespace Gear.Net.Collections
{
    /// <summary>
    /// Represents a network message that is sent to a remote endpoint to retrieve a list of available <see cref="NetworkedCollection{T}"/> synchronization groups.
    /// </summary>
    [ProtoContract]
    public class NetworkedCollectionQueryRequestMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return Gear.Net.Messages.BuiltinMessageIds.NetworkedCollectionQueryRequest;
            }
        }

    }
}
