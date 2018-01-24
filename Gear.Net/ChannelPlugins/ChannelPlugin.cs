using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSCore;

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

        #region Events
        /// <summary>
        /// Raised when this instance is attached to a message <see cref="Channel"/>.
        /// </summary>
        public event EventHandler<ChannelEventArgs> LocalPluginAttached;

        /// <summary>
        /// Raised when this instance is detached from a message <see cref="Channel"/>.
        /// </summary>
        public event EventHandler<ChannelEventArgs> LocalPluginDetached;

        /// <summary>
        /// Raised when an instance of this plugin is attached by the remote peer to the message <see cref="Channel"/> between the local endpoint and the remote peer.
        /// </summary>
        public event EventHandler RemotePluginAttached;

        /// <summary>
        /// Raised 
        /// </summary>
        public event EventHandler RemotePluginDetached;
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
        public void Attach(Channel channel)
        {
            this.AttachedChannel = channel;
            this.AttachedChannel.RegisterHandler<PluginAttachmentEventMessage>(this.Handle_PluginAttachmentEvent, this);

            this.DoAttach(channel);

            this.OnLocalPluginAttached(new ChannelEventArgs(channel));
        }

        public void Detach(Channel channel)
        {
            this.DoDetach(channel);

            this.AttachedChannel = null;

            this.OnLocalPluginDetached(new ChannelEventArgs(channel));

            channel.UnregisterHandler(this);
        }

        protected abstract void DoAttach(Channel channel);

        protected abstract void DoDetach(Channel channel);

        protected virtual void OnLocalPluginAttached(ChannelEventArgs data)
        {
            this.LocalPluginAttached?.Invoke(this, data);

            var msg = new PluginAttachmentEventMessage();
            msg.PluginClassName = this.GetType().Name;
            msg.PluginAttached = true;

            data.Channel.Send(msg);
        }

        protected virtual void OnLocalPluginDetached(ChannelEventArgs data)
        {
            this.LocalPluginDetached?.Invoke(this, data);

            var msg = new PluginAttachmentEventMessage();
            msg.PluginClassName = this.GetType().Name;
            msg.PluginAttached = false;

            data.Channel.Send(msg);
        }

        protected virtual void OnRemotePluginAttached(EventArgs data = null)
        {
            this.RemotePluginAttached?.Invoke(this, data);
        }

        protected virtual void OnRemotePluginDetached(EventArgs data = null)
        {
            this.RemotePluginDetached?.Invoke(this, data);
        }

        protected virtual void Handle_PluginAttachmentEvent(MessageEventArgs e, PluginAttachmentEventMessage message)
        {
            if (message.PluginClassName == this.GetType().Name)
            {
                if (message.PluginAttached)
                {
                    Log.Write(LogMessageGroup.Debug, "Remote side has attached plugin {0} to connection.", message.PluginClassName);
                    this.OnRemotePluginAttached();
                }
                else
                {
                    Log.Write(LogMessageGroup.Debug, "Remote side has detached plugin {0} from connection.", message.PluginClassName);
                    this.OnRemotePluginDetached();
                }
            }
        }
        #endregion
    }
}
