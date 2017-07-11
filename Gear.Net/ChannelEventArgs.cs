/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Gear.Net
{
    public class ChannelEventArgs
    {
        public ChannelEventArgs(Channel channel)
        {
            Contract.Requires(channel != null);

            this.Channel = channel;
        }

        public Channel Channel { get; private set; }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Channel != null);
        }
    }
}
