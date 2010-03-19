using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intralock
{
    /// <summary>
    /// Represents an element within a virtual world.
    /// </summary>
    public class Entity
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Entity.InstanceId"/> property.
        /// </summary>
        private Guid instanceId;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the unique identifier of the current <see cref="Entity"/> instance. The instance id is unique over the lifetime of the entity,
        /// including between sessions.
        /// </summary>
        public Guid InstanceId
        {
            get
            {
                return this.instanceId;
            }
            internal set
            {
                this.instanceId = value;
            }
        }
        #endregion
    }
}
