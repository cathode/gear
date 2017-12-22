using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.ChannelPlugins.Clustering
{
    public class ClusteringPlugin : ChannelPlugin
    {
        public override void Attach(Channel channel)
        {
            this.AttachedChannel = channel;

            this.AttachedChannel.RegisterHandler<RetargetMessage>(this.Handle_Retarget, this);
        }

        public override void Detach(Channel channel)
        {
            channel.UnregisterHandler(this);
            this.AttachedChannel = null;
        }

        private void Handle_Retarget(MessageEventArgs e, RetargetMessage message)
        {
            var cc = this.AttachedChannel as ConnectedChannel;

            if (cc != null)
            {
                cc.Disconnect();
            }
        }
    }
}
