using System;

namespace Intralock
{
    /// <summary>
    /// Represents a changed or new value of an <see cref="Entity"/> field.
    /// </summary>
    public class Update
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Update.Target"/> property.
        /// </summary>
        private Guid target;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="Guid"/> that points to the <see cref="Entity.InstanceId"/> of the <see cref="Entity"/> that is being updated.
        /// </summary>
        public Guid Target
        {
            get
            {
                return this.target;
            }
            set
            {
                this.target = value;
            }
        }
        #endregion
    }
}
