using System;
using System.Collections.Generic;
using System.Text;

namespace Intralock
{
    /// <summary>
    /// Interacts with a <see cref="World"/>.
    /// </summary>
    public abstract class Node
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Node.Local"/> property.
        /// </summary>
        private NodeInterface local;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="NodeInterface"/> that the local <see cref="Node"/> uses to communicate with the remote <see cref="Node"/>.
        /// </summary>
        public NodeInterface Local
        {
            get
            {
                return this.local;
            }
            set
            {
                this.local = value;
            }
        }
        #endregion
    }
}