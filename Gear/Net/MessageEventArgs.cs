using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics.Contracts;

namespace Gear.Net
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(IMessage data)
        {
            Contract.Requires(data != null);

            this.Data = data;
        }

        public IPEndPoint Sender { get; set; }

        public IMessage Data { get; private set; }

        public Guid MessageId { get; set; }

        public DateTime ReceivedAt { get; set; }

        
        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Data != null);
            
        }
    }
}
