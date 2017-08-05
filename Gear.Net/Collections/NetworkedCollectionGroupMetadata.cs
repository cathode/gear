using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.Collections
{
    public class NetworkedCollectionGroupMetadata
    {
        public long CollectionGroupId { get; set; }

        public Guid ProducerId { get; set; }

        public List<Guid> ConsumerIds { get; set; }
    }
}
