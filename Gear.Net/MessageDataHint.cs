using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    [Flags]
    public enum MessageDataHint : long
    {
        None = 0x0,
        MultipleItems = 0x01
    }
}
