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
    public enum NetworkedCollectionStateAction : byte
    {
        /// <summary>
        /// Indicates that a collection instance is joining a synchronization group.
        /// </summary>
        Join = 0x01,

        /// <summary>
        /// Indicates that a collection instance is leaving a synchronization group.
        /// </summary>
        Leave = 0x02,

        /// <summary>
        /// Indicates that the collection instance is requesting a one time retrieval from the synchronization group.
        /// </summary>
        Pull = 0x03,

        /// <summary>
        /// Indicates that the collection instance is providing a one-time population of items to the synchronization group.
        /// </summary>
        Push = 0x04,

        /// <summary>
        /// Indicates the collection is changing replication modes.
        /// </summary>
        ModeChange = 0x05
    }
}
