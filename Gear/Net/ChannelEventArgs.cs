using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Gear.Net
{
    public class ChannelEventArgs
    {
        public ChannelEventArgs(Channel channel)
        {
            Contract.Requires(channel != null);

            this.Channel = channel;
        }

        public Channel Channel { get; private set; }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Channel != null);
        }
    }
}
