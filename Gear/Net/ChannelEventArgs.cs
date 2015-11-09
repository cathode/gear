using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    public class ChannelEventArgs
    {
        public ChannelEventArgs()
        {

        }
        public ChannelEventArgs(Channel channel)
        {
            this.Channel = channel;
        }

        public Channel Channel { get; set; }
    }
}
