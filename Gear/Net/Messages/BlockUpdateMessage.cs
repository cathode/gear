using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.Messages
{
    [ProtoContract]
    public class BlockUpdateMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get { return Ids.BlockUpdate; }
        }

        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public int? NewBlockId { get; set; }
    }
}
