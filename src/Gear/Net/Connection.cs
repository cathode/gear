/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference License (MS-RL). See the 'license.txt' file for details.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Gear.Net
{
    /// <summary>
    /// Represents a network connection between two endpoints which provides queue and event-based notifications.
    /// </summary>
    public abstract class Connection
    {
        #region Fields
        /// <summary>
        /// Holds the default port number used by Gear network communication.
        /// </summary>
        public const ushort DefaultPort = 10421;

        /// <summary>
        /// Backing field for the <see cref="Connection.State"/> property.
        /// </summary>
        private ConnectionState state;

        /// <summary>
        /// Backing field for the <see cref="Connection.SendQueue"/> property.
        /// </summary>
        private readonly Queue<Message> sendQueue;

        /// <summary>
        /// Backing field for the <see cref="Connection.ReceiveQueue"/> property.
        /// </summary>
        private readonly Queue<Message> receiveQueue;
        #endregion
        #region Constructors
        protected Connection()
        {
            this.sendQueue = new Queue<Message>();
            this.receiveQueue = new Queue<Message>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="ConnectionState"/> of the current <see cref="Connection"/>.
        /// </summary>
        public ConnectionState State
        {
            get
            {
                return this.state;
            }
            protected set
            {
                var e = new ConnectionStateEventArgs(this.state, value);
                this.state = value;
                this.OnStateChanged(this, e);
            }
        }

        public Queue<Message> SendQueue
        {
            get
            {
                return this.sendQueue;
            }
        }

        public Queue<Message> ReceiveQueue
        {
            get
            {
                return this.receiveQueue;
            }
        }
        #endregion
        #region Events
        /// <summary>
        /// Raised when the value of the <see cref="Connection.State"/> property changes, indicating a change in the underlying network socket.
        /// </summary>
        public event EventHandler StateChanged;

        /// <summary>
        /// Raised when a <see cref="Message"/> is received from the remote endpoint, after it has been enqueued to the <see cref="Connection.ReceiveQueue"/>.
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageReceived;

        /// <summary>
        /// Raised when a <see cref="Message"/> has been sent to the remote endpoint, after it has been dequeued from the <see cref="Connection.SendQueue"/>.
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageSent;
        #endregion
        #region Methods
        /// <summary>
        /// Raises the <see cref="Connection.StateChanged"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnStateChanged(object sender, ConnectionStateEventArgs e)
        {
            if (this.StateChanged != null)
                this.StateChanged(sender, e);
        }

        /// <summary>
        /// Raises the <see cref="Connection.MessageSent"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnMessageSent(object sender, MessageEventArgs e)
        {
            if (this.MessageSent != null)
                this.MessageSent(sender, e);
        }

        /// <summary>
        /// Raises the <see cref="Connection.MessageReceived"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnMessageReceived(object sender, MessageEventArgs e)
        {
            if (this.MessageReceived != null)
                this.MessageReceived(sender, e);
        }
        #endregion
    }
}
