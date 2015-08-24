/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2014 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net
{
    /// <summary>
    /// Provides a wrapper for message data to assist the dispatch process.
    /// </summary>
    [ProtoContract]
    public class MessageContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageContainer"/> class.
        /// </summary>
        /// <param name="contents"></param>
        public MessageContainer(IMessage contents)
        {
            this.DispatchId = contents.DispatchId;
        }

        /// <summary>
        /// Gets or sets the dispatch id of the message type.
        /// </summary>
        [ProtoMember(1)]
        public int DispatchId { get; set; }

        /// <summary>
        /// Gets or sets the message data object.
        /// </summary>
        [ProtoMember(2)]
        public IMessage Contents { get; set; }
    }
}
