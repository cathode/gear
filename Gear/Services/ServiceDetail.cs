using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Services
{
    /// <summary>
    /// Represents a message sent by a service that 
    /// </summary>
    [ProtoContract]
    public class ServiceDetail
    {
        [ProtoMember(1)]
        public Guid ServiceInstanceId { get; set; }
    }
}
