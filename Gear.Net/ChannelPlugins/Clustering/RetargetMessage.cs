using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.ChannelPlugins.Clustering
{
    [ProtoContract]
    public class RetargetMessage : IMessage
    {
        int IMessage.DispatchId
        {
            get
            {
                return BuiltinMessageIds.Retarget;
            }
        }

        [ProtoMember(1)]
        public IPTarget NewTarget { get; set; }
    }
}
