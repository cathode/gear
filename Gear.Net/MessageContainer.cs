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
using ProtoBuf;
using System.Diagnostics.Contracts;
using System.Net;


namespace Gear.Net
{
    /// <summary>
    /// Provides a wrapper for message data to assist the dispatch process.
    /// </summary>
    [ProtoContract]
    public class MessageContainer
    {
        private IMessage contents;

        static MessageContainer()
        {
            ProtoBuf.Meta.RuntimeTypeModel.Default.AutoAddMissingTypes = true;
            ProtoBuf.Meta.RuntimeTypeModel.Default.Add(typeof(MessageContainer), true);
            //var t = RuntimeTypeModel.Default.Add(typeof(IMessage), true);
            //t.AddSubType(1, typeof(ClientGreetingMessage));

        }

        public MessageContainer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageContainer"/> class.
        /// </summary>
        /// <param name="contents"></param>
        public MessageContainer(IMessage contents)
        {
            Contract.Requires(contents != null);

            this.contents = contents;

            if (contents.DispatchId != 0)
            {
                this.DispatchId = contents.DispatchId;
            }
            else
            {
                this.DispatchId = contents.GetType().GetHashCode();
            }
        }

        /// <summary>
        /// Gets or sets the dispatch id of the message type.
        /// </summary>
        [ProtoMember(1)]
        public int DispatchId { get; set; }

        [ProtoMember(2)]
        public Guid InstanceId { get; set; }

        [ProtoMember(3)]
        public Guid MessageId { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="IMessagePayload"/> object that represents the actual content of the message transmitted.
        /// </summary>
        [ProtoMember(4)]
        public IMessage Contents
        {
            get
            {
                return this.contents;
            }
            set
            {
                Contract.Requires(value != null);

                this.DispatchId = value.DispatchId;
                this.contents = value;
            }
        }

        [ProtoIgnore]
        public IPEndPoint Destination { get; set; }
    }


}
