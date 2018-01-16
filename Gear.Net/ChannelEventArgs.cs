/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    /// <summary>
    /// Represents event data for channel connectivity events.
    /// </summary>
    public class ChannelEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelEventArgs"/> class.
        /// </summary>
        /// <param name="channel">The <see cref="Gear.Net.Channel"/> that the event is associated with.</param>
        public ChannelEventArgs(Channel channel)
        {
            Contract.Requires<ArgumentNullException>(channel != null);

            this.Channel = channel;
        }

        /// <summary>
        /// Gets the <see cref="Gear.Net.Channel"/> that the event is associated with.
        /// </summary>
        public Channel Channel { get; private set; }

        /// <summary>
        /// Invariant contracts for this class.
        /// </summary>
        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Channel != null);
        }
    }
}
