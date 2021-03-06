﻿/******************************************************************************
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
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GSCore;
using ProtoBuf;
using ProtoBuf.Meta;

namespace Gear.Net
{
    /// <summary>
    /// Represents a communication channel between the local end point and a remote end point.
    /// </summary>
    public abstract class Channel
    {
        #region Fields
        private readonly object txLock = new object();
        private readonly object rxLock = new object();

        private readonly object txFlushGate = new object();
        private readonly object rxFlushGate = new object();

        private readonly object metaLock = new object();

        private readonly Dictionary<int, List<MessageHandlerRegistration>> messageHandlers = new Dictionary<int, List<MessageHandlerRegistration>>();

        private volatile bool isRxQueueIdle;

        /// <summary>
        /// Holds the messages that are being serialized and flushed to the socket.
        /// </summary>
        private Queue<IMessage> txBuffer;

        /// <summary>
        /// Holds the messages that have been queued for sending.
        /// </summary>
        private Queue<IMessage> txQueue;

        /// <summary>
        /// Holds the messages that are being processed by message handlers / event subscribers.
        /// </summary>
        private Queue<IMessage> rxBuffer;

        /// <summary>
        /// Holds the messages that have been deserialized and received from the socket.
        /// </summary>
        private Queue<IMessage> rxQueue;

        #endregion
        #region Constructors
        static Channel()
        {
            Contract.Assume(RuntimeTypeModel.Default != null);

            RuntimeTypeModel.Default.Add(typeof(IMessage), false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Channel"/> class.
        /// </summary>
        protected Channel()
        {
            this.txQueue = new Queue<IMessage>();
            this.txBuffer = new Queue<IMessage>();
            this.rxQueue = new Queue<IMessage>();
            this.rxBuffer = new Queue<IMessage>();
        }
        #endregion

        #region Events

        /// <summary>
        /// Raised when a network message sent by the remote endpoint is received locally by this <see cref="Channel"/>.
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageReceived;
        #endregion
        #region Properties

        /// <summary>
        /// Gets the <see cref="IPEndPoint"/> that represents the local network endpoint for this communication channel.
        /// </summary>
        public abstract IPEndPoint LocalEndPoint { get; }

        /// <summary>
        /// Gets the <see cref="IPEndPoint"/> that represents the remote peer's network endpoint for this communication channel.
        /// </summary>
        public abstract IPEndPoint RemoteEndPoint { get; }

        /// <summary>
        /// Gets or sets the <see cref="ChannelState"/> for this communication channel.
        /// </summary>
        public ChannelState State { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether message handlers will be called in an async (non-blocking), or synchronous (blocking) fashion.
        /// </summary>
        /// <remarks>
        /// When handlers are called in an async fashion, they can be invoked in any order and care should be taken to avoid race conditions in handler action code.
        /// </remarks>
        public bool InvokeHandlersAsync { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// All messages published by the specified publisher will be forwarded
        /// to the remote endpoint via this channel.
        /// </summary>
        /// <param name="publisher"></param>
        public void SubscribeToPublisher(IMessagePublisher publisher)
        {
            Contract.Requires(publisher != null);

            publisher.MessageAvailable += this.publisher_MessageAvailable;
            publisher.ShuttingDown += this.publisher_ShuttingDown;
        }

        /// <summary>
        /// Registers a handler for the specified message dispatch ID.
        /// </summary>
        /// <param name="dispatchId">The dispatch id to register the handler for.</param>
        /// <param name="handlerAction">The delegate to be invoked, which will handle a received message with the specified dispatch id.</param>
        /// <param name="owner">The object associated with this registration (for cleanup purposes).</param>
        public void RegisterHandler(int dispatchId, Action<MessageEventArgs, IMessage> handlerAction, object owner = null)
        {
            var reg = new MessageHandlerRegistration();

            reg.Owner = owner;
            reg.Action = handlerAction;

            lock (this.metaLock)
            {
                if (!this.messageHandlers.ContainsKey(dispatchId))
                {
                    this.messageHandlers.Add(dispatchId, new List<MessageHandlerRegistration>());
                }

                Contract.Assume(this.messageHandlers[dispatchId] != null);

                this.messageHandlers[dispatchId].Add(reg);
            }

            // Any recieved messages that didnt' have a handler when they arrived, might be handled now.
            this.ProcessRxQueue();
        }

        /// <summary>
        /// Registers a handler for the specified message dispatch ID.
        /// </summary>
        /// <param name="handlerAction">The delegate to be invoked, which will handle a received message of the specified implementation type.</param>
        /// <param name="owner">The object associated with this registration (for cleanup purposes).</param>
        /// <typeparam name="T">The message implementation type to register the handler for.</typeparam>
        public void RegisterHandler<T>(Action<MessageEventArgs, T> handlerAction, object owner = null)
            where T : IMessage
        {
            var inst = Activator.CreateInstance<T>();

            this.RegisterHandler(inst.DispatchId, (e, m) => { handlerAction(e, (T)m); }, owner);
        }

        /// <summary>
        /// Removes message handlers associated with the specified object owner.
        /// </summary>
        /// <param name="owner">The owning object related to the message handlers to unregister.</param>
        public void UnregisterHandler(object owner)
        {
            lock (this.metaLock)
            {
                var handlers = this.messageHandlers.Values.SelectMany(e => e.Where(p => p.Owner == owner)).ToArray();

                var rem = new List<int>();

                foreach (var kvp in this.messageHandlers)
                {
                    foreach (var h in handlers)
                    {
                        if (kvp.Value.Contains(h))
                        {
                            kvp.Value.Remove(h);
                        }
                    }

                    if (kvp.Value.Count == 0)
                    {
                        rem.Add(kvp.Key);
                    }
                }

                foreach (var id in rem)
                {
                    this.messageHandlers.Remove(id);
                }
            }
        }

        /// <summary>
        /// Queues one or more messages on the channel, to be sent to the remote endpoint.
        /// </summary>
        /// <param name="messages">The message(s) to be sent.</param>
        public void Send(params IMessage[] messages)
        {
            Contract.Requires(messages != null);
            Contract.Requires(messages.Length > 0);

            this.QueueTxMessageThreadSafe(messages);
        }

        /// <summary>
        /// Starts the socket receive loop.
        /// </summary>
        public void Setup()
        {
            this.BeginBackgroundReceive();
        }

        /// <summary>
        /// Gracefully shuts down the <see cref="Channel"/> and notifies the remote endpoint of the impending connection closure.
        /// </summary>
        public void Teardown()
        {
            var msg = new Messages.TeardownChannelMessage();

            this.Send(msg);
            this.State = ChannelState.Disconnected;
            this.ProcessTxQueue();
        }

        /// <summary>
        /// Starts the Channel's rx process in the background.
        /// </summary>
        protected abstract void BeginBackgroundReceive();

        /// <summary>
        /// When implemented in a derived class, performs the relevant actions to write the messages to be sent to the remote endpoint.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        protected abstract int SendMessages(Queue<IMessage> messages);

        /// <summary>
        /// Runs through the queue of messages to be sent and writes them to the network.
        /// </summary>
        protected void ProcessTxQueue()
        {
            try
            {
                // Ensure method is limited to a single active call.
                // If another call to FlushMessage is running (has txFlushGate locked), we just abort.
                if (Monitor.TryEnter(this.txFlushGate, 50))
                {
                    // Channel can't be disconnected or dead
                    if (this.State == ChannelState.Connected)
                    {
                        // Lock the active queue and send all messages in it.
                        lock (this.txQueue)
                        {
                            // Swap queued messages to send buffer
                            var q = this.txQueue;
                            var b = this.txBuffer;
                            this.txBuffer = q;
                            this.txQueue = b;

                            if (this.txBuffer.Count > 0)
                            {
                                this.SendMessages(this.txBuffer);
                            }
                        }
                    }
                }
            }
            finally
            {
                if (Monitor.IsEntered(this.txFlushGate))
                {
                    Monitor.Exit(this.txFlushGate);
                }
            }
        }

        /// <summary>
        /// Runs through the the queue of messages that have been received and invokes the appropriate events / message handlers.
        /// </summary>
        protected void ProcessRxQueue()
        {
            try
            {
                // Ensure method is limited to a single active call.
                // If another call to FlushMessage is running (has txFlushGate locked), we just abort.
                if (Monitor.TryEnter(this.rxFlushGate, 50))
                {

                    if (this.State == ChannelState.Connected)
                    {
                        lock (this.rxQueue)
                        {
                            var q = this.rxQueue;
                            var b = this.rxBuffer;
                            this.rxBuffer = q;
                            this.rxQueue = b;
                        }

                        while (this.rxBuffer.Count > 0)
                        {
                            var item = this.rxBuffer.Dequeue();

                            this.OnMessageReceived(new MessageEventArgs(item) { ReceivedAt = DateTime.Now, Sender = this.RemoteEndPoint, Channel = this });
                        }
                    }

                    this.isRxQueueIdle = true;
                }
                else
                {
                    //Log.Write(LogMessageGroup.Debug, "Another thread already processing RxQueue");
                }
            }
            finally
            {
                if (Monitor.IsEntered(this.rxFlushGate))
                {
                    Monitor.Exit(this.rxFlushGate);
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="MessageReceived"/> event, and invokes any registered handlers for the received message's dispatch id.
        /// </summary>
        /// <param name="e">Event data for the event.</param>
        protected virtual void OnMessageReceived(MessageEventArgs e)
        {
            Contract.Requires(e != null);

            this.MessageReceived?.Invoke(this, e);

            var msg = e.Data;
            if (this.messageHandlers.ContainsKey(msg.DispatchId))
            {
                if (this.InvokeHandlersAsync)
                {
                    Task.Run(() => this.messageHandlers[msg.DispatchId].ForEach((h) => { h.Action(e, msg); }));
                }
                else
                {
                    this.messageHandlers[msg.DispatchId].ForEach((h) => { h.Action(e, msg); });
                }
            }
            else
            {
                Log.Write(LogMessageGroup.Informational, "No handler registered for message dispatch id {0:X8} received from peer {1}", msg.DispatchId, e.Sender);
            }
        }

        /// <summary>
        /// Adds messages to the send queue with thread safety checks.
        /// </summary>
        /// <param name="messages"></param>
        protected void QueueTxMessageThreadSafe(params IMessage[] messages)
        {
            Contract.Requires(messages != null);
            Contract.Requires(messages.Length > 0);

            // TODO: ensure that locking against the queue itself is correct.
            lock (this.txQueue)
            {
                foreach (var m in messages)
                {
                    this.txQueue.Enqueue(m);
                }
            }

            this.ProcessTxQueue();
        }

        /// <summary>
        /// Adds messages to the receive queue with thread safety checks.
        /// </summary>
        /// <param name="messages">The messages to add to the receive queue.</param>
        protected void QueueRxMessageThreadSafe(params IMessage[] messages)
        {
            Contract.Requires(messages != null);
            Contract.Requires(messages.Length > 0);

            lock (this.rxQueue)
            {
                foreach (var m in messages)
                {
                    this.rxQueue.Enqueue(m);
                }
            }

            this.ProcessRxQueue();
        }

        private void publisher_ShuttingDown(object sender, EventArgs e)
        {
            var publisher = (IMessagePublisher)sender;

            if (publisher != null)
            {
                // Remove event bindings
                publisher.ShuttingDown -= this.publisher_ShuttingDown;
                publisher.MessageAvailable -= this.publisher_MessageAvailable;
            }
        }

        private void publisher_MessageAvailable(object sender, MessageEventArgs e)
        {
            Contract.Requires(e != null);
            this.QueueTxMessageThreadSafe(e.Data);
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.txQueue != null);
            Contract.Invariant(this.txBuffer != null);
            Contract.Invariant(this.rxQueue != null);
            Contract.Invariant(this.rxBuffer != null);

            Contract.Invariant(this.messageHandlers != null);
        }
        #endregion
    }
}