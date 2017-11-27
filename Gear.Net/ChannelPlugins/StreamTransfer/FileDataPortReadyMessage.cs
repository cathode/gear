using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Net.Messages;
using ProtoBuf;

namespace Gear.Net.ChannelPlugins.FileTransfer
{
    /// <summary>
    /// Represents a network message that indicates to the passive peer that a data port is opened on the active peer for transfering file data.
    /// </summary>
    [ProtoContract]
    public class FileDataPortReadyMessage : IMessage
    {
        [ProtoIgnore]
        int IMessage.DispatchId
        {
            get
            {
                return BuiltinMessageIds.FileDataPortReady;
            }
        }

        /// <summary>
        /// Gets or sets the numeric transfer id that this message relates to.
        /// </summary>
        [ProtoMember(1)]
        public long TransferId { get; set; }

        /// <summary>
        /// Gets or sets the port number on the active side.
        /// </summary>
        [ProtoMember(2)]
        public ushort DataPort { get; set; }
    }
}
