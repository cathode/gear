using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.Collections
{
    /// <summary>
    /// Enumerates actions supported for networked collection updates.
    /// </summary>
    public enum NetworkedCollectionAction : byte
    {
        /// <summary>
        /// Indicates the collection is being cleared (all items removed at once).
        /// </summary>
        Clear   = 0x00,

        /// <summary>
        /// Indicates that an item is being added to the collection.
        /// </summary>
        Add     = 0x01,

        /// <summary>
        /// Indicates that an item is being removed from the collection.
        /// </summary>
        Remove  = 0x02,

        /// <summary>
        /// Indicates that a collection instance is joining a synchronization group.
        /// </summary>
        Join    = 0x10,

        /// <summary>
        /// Indicates that a collection instance is leaving a synchronization group.
        /// </summary>
        Leave   = 0x11,

        /// <summary>
        /// Indicates the collection is changing replication modes.
        /// </summary>
        ModeChange = 0x12,
    }
}
