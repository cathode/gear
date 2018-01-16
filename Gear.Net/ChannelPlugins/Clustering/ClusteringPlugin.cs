using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.ChannelPlugins.Clustering
{
    public class ClusteringPlugin : ChannelPlugin
    {
        protected override void DoAttach(Channel channel)
        {
            //this.AttachedChannel = channel;

            this.AttachedChannel.RegisterHandler<RetargetMessage>(this.Handle_Retarget, this);
        }

        protected override void DoDetach(Channel channel)
        {
            channel.UnregisterHandler(this);
            //this.AttachedChannel = null;
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
