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
using System.Net;
using System.Diagnostics.Contracts;

namespace Gear.Net
{
    /// <summary>
    /// Represents data associated with a message event.
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="data">The <see cref="IMessage"/> instance associated with the event invocation.</param>
        public MessageEventArgs(IMessage data)
        {
            Contract.Requires(data != null);

            this.Data = data;
        }

        /// <summary>
        /// Gets or sets the <see cref="IPEndPoint"/> associated with the remote host where the message originated.
        /// </summary>
        public IPEndPoint Sender { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IMessage"/> that is associated with the event.
        /// </summary>
        public IMessage Data { get; private set; }

        /// <summary>
        /// Gets or sets the unique id of the message. This is only used when a direct 'reply' message is expected by the sender.
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the message was received.
        /// </summary>
        public DateTime ReceivedAt { get; set; }

        public Channel Channel { get; set; }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Data != null);
        }
    }
}
