using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.Collections
{
    public class NetworkedCollectionItemEventArgs<T> : EventArgs
    {
        public NetworkedCollectionUpdateAction Action { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
