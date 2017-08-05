using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.Collections
{
    /// <summary>
    /// Provides a supervisory class that assists the <see cref="NetworkedCollection{T}"/> class.
    /// </summary>
    public class NetworkedCollectionManager
    {
        private static Lazy<NetworkedCollectionManager> instance = new Lazy<NetworkedCollectionManager>(true);

        private NetworkedCollectionManager()
        {
        }
    }
}
