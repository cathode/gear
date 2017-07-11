using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Model;

namespace Gear.IO
{
    /// <summary>
    /// Represents the behavior of a game data store.
    /// </summary>
    public interface IDataStore
    {
        IEnumerable<World> GetWorlds();
    }
}
