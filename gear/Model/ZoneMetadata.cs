using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Model
{
    [ProtoContract]
    public class ZoneMetadata
    {
        [ProtoMember(0)]
        public Guid ZoneId { get; set; }

        [ProtoMember(1)]
        public Guid[] Regions { get; set; }
    }
}
