using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net.ChannelPlugins
{
    /// <summary>
    /// Implements a message type that notifies the remote endpoint when a <see cref="ChannelPlugin"/> is attached/detached on the local endpoint.
    /// </summary>
    [ProtoContract]
    public class PluginAttachmentEventMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return BuiltinMessageIds.PluginAttachmentEvent;
            }
        }

        /// <summary>
        /// Gets or sets the class name of the plugin that was attached.
        /// </summary>
        [ProtoMember(1)]
        public string PluginClassName { get; set; }

        [ProtoMember(2)]
        public bool PluginAttached { get; set; }
    }
}
