using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.Collections
{
    /// <summary>
    /// Enumerates supported update actions for networked collections.
    /// </summary>
    public enum NetworkedCollectionUpdateAction
    {
        Add = 0x01,

        Remove = 0x02,

        Clear = 0x03,
    }
}
