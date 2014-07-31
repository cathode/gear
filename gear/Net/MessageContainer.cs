using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    public class MessageContainer
    {
        public MessageContainer(IMessage contents)
        {
            this.DispatchId = contents.DispatchId;
        }

        public int DispatchId { get; set; }

        public IMessage Contents { get; set; }
    }
}
