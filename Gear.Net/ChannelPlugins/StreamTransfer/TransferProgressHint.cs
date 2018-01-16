using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.ChannelPlugins.StreamTransfer
{
    public enum TransferProgressHint
    {
        Queued = 0x0,
        Initiated,
        WaitingForDataConnection,
        ConnectingToDataPort,
        Sending,
        Receiving,
        Completed,
        Failed
    }
}
