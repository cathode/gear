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
using System.Net.Sockets;
using System.Diagnostics.Contracts;
using System.Threading;
using ProtoBuf.Meta;
using ProtoBuf;

namespace Gear.Net
{
    /// <summary>
    /// Represents a communication channel between the local end point and a remote end point.
    /// </summary>
    public abstract class Channel
    {
        /// <summary>
        /// Holds the messages that are being serialized and flushed to the socket.
        /// </summary>
        protected Queue<IMessage> txBuffer;

        /// <summary>
        /// Holds the messages that have been queued for sending.
        /// </summary>
        protected Queue<IMessage> txQueue;

        /// <summary>
        /// Holds the messages that are being processed by message handlers / event subscribers.
        /// </summary>
        protected Queue<IMessage> rxBuffer;

        /// <summary>
        /// Holds the messages that have been deserialized and received from the socket.
        /// </summary>
        protected Queue<IMessage> rxQueue;

        private readonly object txLock = new object();
        private readonly object rxLock = new object();

        private readonly object txFlushGate = new object();
        private readonly object rxFlushGate = new object();

        private readonly object metaLock = new object();

        private readonly Dictionary<int, List<MessageHandlerRegistration>> messageHandlers = new Dictionary<int, List<MessageHandlerRegistration>>();

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

            this.txFlushGate = new AutoResetEvent(true);
            this.isRxQueueIdle = true;
            //this.rxFlushGate = new AutoResetEvent(true);
        }

        public abstract IPEndPoint LocalEndPoint { get; }
        public abstract IPEndPoint RemoteEndPoint { get; }

        public ChannelState State { get; protected set; }

        public event EventHandler<MessageEventArgs> MessageReceived;

        public bool InvokeHandlersAsync { get; set; }

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
        /// <param name="dispatchId"></param>
        /// <param name="handler"></param>
        /// <returns>The guid of the handler registration, used for unregistering</returns>
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

                this.messageHandlers[dispatchId].Add(reg);
            }
            // Any recieved messages that didnt' have a handler when they arrived, might be handled now.
            this.ProcessRxQueue();
        }

        /// <summary>
        /// Registers a handler for the specified message dispatch ID.
        /// </summary>
        /// <param name="dispatchId"></param>
        /// <param name="handler"></param>
        /// <returns>The guid of the handler registration, used for unregistering the handler.</returns>
        public void RegisterHandler<T>(Action<MessageEventArgs, T> handlerAction, object owner = null) where T : IMessage
        {
            var inst = Activator.CreateInstance<T>();

            this.RegisterHandler(inst.DispatchId, (e, m) => { handlerAction(e, (T)m); }, owner);
        }

        public void UnregisterHandler(object owner)
        {
            lock (this.metaLock)
            {
                var handlers = this.messageHandlers.Values.SelectMany(e => e.Where(p => p.Owner == owner)).ToArray();

                var rem = new List<int>();

                foreach (var kvp in this.messageHandlers)
                {
                    foreach (var h in handlers)
                        if (kvp.Value.Contains(h))
                        {
                            kvp.Value.Remove(h);
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
        /// <param name="message"></param>
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
                if (Monitor.TryEnter(this.txFlushGate))
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
                        }

                        if (this.txBuffer.Count > 0)
                        {
                            this.SendMessages(this.txBuffer);
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

        private volatile bool isRxQueueIdle;

        /// <summary>
        /// Runs through the the queue of messages that have been receives and invokes the appropriate events / message handlers.
        /// </summary>
        protected void ProcessRxQueue()
        {
            if (this.isRxQueueIdle)
            {
                this.isRxQueueIdle = false;

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

                        this.OnMessageReceived(new MessageEventArgs(item) { ReceivedAt = DateTime.Now, Sender = this.RemoteEndPoint });
                    }
                }

                this.isRxQueueIdle = true;
            }
        }

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
        /// <param name="messages"></param>
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

        public class MessageHandlerRegistration
        {
            public object Owner { get; set; }

            public Action<MessageEventArgs, IMessage> Action { get; set; }
        }
    }
}
