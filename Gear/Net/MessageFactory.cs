/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gear.Net.Messaging;

namespace Gear.Net
{
    /// <summary>
    /// Provides a way to create instances of specific <see cref="Message"/>-derived types
    /// using the <see cref="MessageId"/> of the specific type of message to create.
    /// </summary>
    public class MessageFactory
    {
        #region Fields
        private static MessageFactory current;
        #endregion
        #region Constructors
        static MessageFactory()
        {
            MessageFactory.current = new MessageFactory();
        }
        public MessageFactory()
        {
        }
        #endregion
        #region Properties
        public static MessageFactory Current
        {
            get
            {
                return MessageFactory.current;
            }
            set
            {
                MessageFactory.current = value;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Creates and returns a new <see cref="Message"/>-derived type current.
        /// </summary>
        /// <param name="id">The <see cref="MessageId"/> of the message type to create.</param>
        /// <returns></returns>
        public virtual Message Create(MessageId id)
        {
            Message message = null;
            switch (id)
            {
                case MessageId.ClientInfo:
                    message = new ClientInfoMessage();
                    break;

                case MessageId.ServerInfo:
                    message = new ServerInfoMessage();
                    break;
            }

            return message;
        }
        #endregion
    }
}
