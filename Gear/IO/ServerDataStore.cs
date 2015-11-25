using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Model;

namespace Gear.IO
{
    /// <summary>
    /// Implements a data store that retrieves world data from a server.
    /// </summary>
    public class ServerDataStore : IDataStore
    {
        public IEnumerable<World> GetWorlds()
        {
            throw new NotImplementedException();
        }
    }
}
