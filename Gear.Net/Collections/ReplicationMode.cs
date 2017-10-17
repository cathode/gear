using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.Collections
{
    /// <summary>
    /// Enumerates supported modes of replication for a <see cref="NetworkedCollection{T}"/>.
    /// </summary>
    public enum ReplicationMode
    {
        /// <summary>
        /// Indicates that the collection is not participating in any replication activities with another remote collection.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Indicates that the collection is designated as the active collection and publishes changes of its' contents to subscribers.
        /// </summary>
        Producer = 0x1,

        /// <summary>
        /// Indicates that the collection is designated as a passive collection and it's contents reflect the remote tracked producer that it is bound to.
        /// </summary>
        Consumer = 0x2,

        /// <summary>
        /// Indicates that the collection is operating in an active-active mode with other remote networked collections.
        /// </summary>
        Full = 0x03
    }
}
