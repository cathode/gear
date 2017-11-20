using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.ChannelPlugins
{
    public abstract class ChannelPlugin
    {
        public abstract void Attach(Channel channel);

        public abstract void Detach(Channel channel);
    }
}
