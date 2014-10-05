using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    public class BroadcastChannel : Channel
    {
        public BroadcastChannel(ushort port)
        {


        }

        protected override void FlushMessages()
        {
            throw new NotImplementedException();
        }
    }
}
