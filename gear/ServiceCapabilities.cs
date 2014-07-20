using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear
{
    /// <summary>
    /// Represents the hardware or host resources of a service endpoint.
    /// </summary>
    [ProtoContract]
    public class ServiceCapabilities
    {
        [ProtoMember(0)]
        public Guid ServiceEndpointId { get; set; }

        [ProtoMember(1)]
        public ushort CoreCount { get; set; }

        [ProtoMember(2)]
        public ushort ThreadCount { get; set; }

        [ProtoMember(3)]
        public ulong MemoryAvilable { get; set; }

        [ProtoMember(4)]
        public string OperatingSystem { get; set; }
    }
}
