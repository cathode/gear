using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Services
{
    /// <summary>
    /// Represents the data in a service announcement which is broadcast over the LAN.
    /// </summary>
    [ProtoContract]
    public class ServiceInfo
    {
        [ProtoMember(1, IsRequired = true)]
        public Guid ServiceInstanceId { get; set; }

        [ProtoMember(2, IsRequired = true)]
        public ServerService Service { get; set; }

        [ProtoMember(3, IsRequired = true)]
        public ushort LocalPort { get; set; }

        [ProtoMember(4, IsRequired = false)]
        public string ServiceName { get; set; }
    }
}
