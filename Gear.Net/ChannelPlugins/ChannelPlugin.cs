using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.ChannelPlugins
{
    /// <summary>
    /// Provides a base class for network channel plugins.
    /// </summary>
    public abstract class ChannelPlugin
    {
        #region Fields
        private Channel attachedChannel;
        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="Channel"/> that the plugin is attached to.
        /// </summary>
        public Channel AttachedChannel
        {
            get
            {
                return this.attachedChannel;
            }

            protected set
            {
                this.attachedChannel = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the plugin instance is attached to a network <see cref="Channel"/>.
        /// </summary>
        public bool IsAttached
        {
            get
            {
                return this.AttachedChannel != null;
            }
        }

        #endregion
        #region Methods
        public abstract void Attach(Channel channel);

        public abstract void Detach(Channel channel);
        #endregion
    }
}
