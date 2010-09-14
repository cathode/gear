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
        /// <summary>
        /// Binds the connection to trigger a flush each time the attached engine raises the Update event.
        /// </summary>
        /// <param name="engine">The engine to attach to.</param>
        public void Attach(EngineBase engine)
        {
            if (this.engine != null)
                this.Detach();

            this.engine = engine;
            this.engine.Update += new EventHandler(this.EngineUpdateCallback);
        }

        /// <summary>
        /// Removes any existing binding to a game engine.
        /// </summary>
        public void Detach()
        {
            if (this.engine != null)
                this.engine.Update -= new EventHandler(this.EngineUpdateCallback);

            this.engine = null;
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
                this.socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(this.SendAsyncCallback), msg);
            }
        }

        /// <summary>
        /// Dequeues the next <see cref="Message"/> from the receive queue (if any) and returns it.
        /// </summary>
        /// <returns>The first message in the receive queue, or null if the receive queue is empty.</returns>
        public Message Receive()
        {
            if (this.ReceiveQueueCount > 0)
                return this.receiveQueue.Dequeue();

            return null;
        }

        /// <summary>
        /// Queues the specified <see cref="Message"/> for sending.
        /// </summary>
        /// <param name="message">The <see cref="Message"/> to be sent.</param>
        public void Send(Message message)
        {
            this.sendQueue.Enqueue(message);
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

        /// <summary>
        /// Invoked each time the attached <see cref="EngineBase"/> raises it's <see cref="EngineBase.Update"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void EngineUpdateCallback(object sender, EventArgs e)
        {
            if (this.State == ConnectionState.Connected)
                this.Flush();
        }

        protected virtual void SendAsyncCallback(IAsyncResult result)
        {
            this.socket.EndSend(result);
        }
        #endregion
    }
}
