/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Net.Sockets;

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
        /// Backing field for the <see cref="Connection.SendQueue"/> property.
        /// </summary>
        private readonly Queue<Message> sendQueue;

        /// <summary>
        /// Backing field for the <see cref="Connection.ReceiveQueue"/> property.
        /// </summary>
        private readonly Queue<Message> receiveQueue;

        /// <summary>
        /// Backing field for the <see cref="Connection.State"/> property.
        /// </summary>
        private ConnectionState state;

        /// <summary>
        /// Backing field for the <see cref="Connection.Socket"/> property.
        /// </summary>
        private Socket socket;

        /// <summary>
        /// Backing field for the <see cref="Connection.Mode"/> property.
        /// </summary>
        private ConnectionMode mode;

        private EngineBase engine;
        #endregion
        #region Constructors
        protected Connection()
        {
            this.sendQueue = new Queue<Message>();
            this.receiveQueue = new Queue<Message>();
        }
        #endregion
        #region Events
        /// <summary>
        /// Raised when the value of the <see cref="Connection.State"/> property changes, indicating a change in the underlying network socket.
        /// </summary>
        public event EventHandler StateChanged;

        /// <summary>
        /// Raised when a <see cref="Message"/> is received from the remote endpoint, after it has been enqueued to the message receive queue.
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageReceived;

        /// <summary>
        /// Raised when a <see cref="Message"/> has been sent to the remote endpoint, after it has been dequeued from the message send queue.
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageSent;
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
                this.OnStateChanged(e);
            }
        }

        public Socket Socket
        {
            get
            {
                return this.socket;
            }
            protected set
            {
                this.socket = value;
            }
        }

        /// <summary>
        /// Gets the number of messages currently waiting in the message send queue.
        /// </summary>
        public int SendQueueCount
        {
            get
            {
                return this.sendQueue.Count;
            }
        }

        /// <summary>
        /// Gets the number of messages currently waiting in the message receive queue.
        /// </summary>
        public int ReceiveQueueCount
        {
            get
            {
                return this.receiveQueue.Count;
            }
        }

        public ConnectionMode Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;
            }
        }
        #endregion

        #region Methods
        public void Attach(EngineBase engine)
        {
            engine.Update += new EventHandler(this.EngineUpdateCallback);
        }
        public void Detach()
        {
            engine.Update -= new EventHandler(this.EngineUpdateCallback);
        }
        /// <summary>
        /// Processes all messages queued for sending and scans any received data to ensure that all fully received messages are parsed and queued in the receive queue.
        /// </summary>
        public void Flush()
        {
            if (this.State != ConnectionState.Connected)
                throw new InvalidOperationException("Connection must be in the 'Connected' state.");

            while (this.sendQueue.Count > 0)
            {
                var msg = this.sendQueue.Dequeue();
                int size = msg.GetByteCount();
                var buffer = new byte[size];
                msg.WriteTo(buffer, 0);
                socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(this.SendAsyncCallback), msg);
            }

            if (this.receiveQueue.Count > 0)
            {

            }
        }

        public void Send(Message message)
        {
            this.Send(message, this.Mode);
        }
        public void Send(Message message, ConnectionMode mode)
        {
            if (mode == ConnectionMode.Blocking)
                throw new NotImplementedException();
            else
                this.sendQueue.Enqueue(message);
        }
        public Message Receive()
        {
            return this.Receive(this.Mode);
        }
        public Message Receive(ConnectionMode mode)
        {
            if (this.ReceiveQueueCount > 0)
                return this.receiveQueue.Dequeue();

            if (mode == ConnectionMode.Blocking)
                throw new NotImplementedException();
            else
                return null;
        }
        /// <summary>
        /// Raises the <see cref="Connection.StateChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="ConnectionStateEventArgs"/> that contains the event data.</param>
        protected virtual void OnStateChanged(ConnectionStateEventArgs e)
        {
            if (this.StateChanged != null)
                this.StateChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Connection.MessageSent"/> event.
        /// </summary>
        /// <param name="e">A <see cref="MessageEventArgs"/> that contains the event data.</param>
        protected virtual void OnMessageSent(MessageEventArgs e)
        {
            if (this.MessageSent != null)
                this.MessageSent(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Connection.MessageReceived"/> event.
        /// </summary>
        /// <param name="e">A <see cref="MessageEventArgs"/> that contains the event data.</param>
        protected virtual void OnMessageReceived(MessageEventArgs e)
        {
            if (this.MessageReceived != null)
                this.MessageReceived(this, e);
        }

        private void EngineUpdateCallback(object sender, EventArgs e)
        {
            if (this.State == ConnectionState.Connected)
                this.Flush();
        }

        private void SendAsyncCallback(IAsyncResult result)
        {
            this.socket.EndSend(result);
        }
        #endregion
    }
}
