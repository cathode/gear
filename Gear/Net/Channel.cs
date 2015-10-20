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
        protected Queue<IMessage> outgoingInactive;
        protected Queue<IMessage> outgoingActive;
        protected Queue<IMessage> incomingInactive;
        protected Queue<IMessage> incomingActive;

        protected System.Runtime.Serialization.IFormatter serializer = ProtoBuf.Serializer.CreateFormatter<MessageContainer>();

        protected ChannelState State;

        static Channel()
        {
            Contract.Assume(RuntimeTypeModel.Default != null);

            RuntimeTypeModel.Default.Add(typeof(IMessage), false);
        }

        protected Channel()
        {
            this.outgoingActive = new Queue<IMessage>();
            this.outgoingInactive = new Queue<IMessage>();
            this.incomingActive = new Queue<IMessage>();
            this.incomingInactive = new Queue<IMessage>();


        }

        public event EventHandler<MessageEventArgs> MessageReceived;

        public void SetUp()
        {

        }

        /// <summary>
        /// All messages published by the specified publisher will be forwarded
        /// to the remote endpoint via this channel.
        /// </summary>
        /// <param name="publisher"></param>
        public void SubscribeToPublisher(IMessagePublisher publisher)
        {
            Contract.Requires(publisher != null);

            publisher.MessageAvailable += publisher_MessageAvailable;
            publisher.ShuttingDown += publisher_ShuttingDown;
        }

        void publisher_ShuttingDown(object sender, EventArgs e)
        {
            var publisher = (IMessagePublisher)sender;

            if (publisher != null)
            {
                // Remove event bindings
                publisher.ShuttingDown -= publisher_ShuttingDown;
                publisher.MessageAvailable -= publisher_MessageAvailable;
            }
        }

        void publisher_MessageAvailable(object sender, MessageEventArgs e)
        {
            Contract.Requires(e != null);
            if (e.Message != null)
                this.QueueMessage(e.Message);
        }

        /// <summary>
        /// Queues an individual message on the channel, to be sent to the remote endpoint.
        /// </summary>
        /// <param name="message"></param>
        public void QueueMessage(IMessage message)
        {
            Contract.Requires(message != null);

            if (this.State == ChannelState.Shutdown)
                throw new InvalidOperationException();

            this.QueueMessageThreadSafe(message);
        }

        private void QueueMessageThreadSafe(IMessage message)
        {
            Contract.Requires(message != null);

            // TODO: ensure that locking against the queue itself is correct.
            lock (this.outgoingInactive)
            {
                this.outgoingInactive.Enqueue(message);
            }

            this.FlushMessages();
        }

        /// <summary>
        /// Starts the socket receive loop.
        /// </summary>
        public void Setup()
        {

        }

        /// <summary>
        /// Gracefully shuts down the <see cref="Channel"/> and notifies the remote endpoint of the impending connection closure.
        /// </summary>
        public void Teardown()
        {
            var msg = new Messages.TeardownChannelMessage();

            this.QueueMessage(msg);
            this.State = ChannelState.Shutdown;
            this.FlushMessages();
        }

        protected virtual void DoSetup()
        {

        }

        protected abstract System.IO.Stream GetMessageDestinationStream();

        protected abstract void SendMessage(MessageContainer mc);

        /// <summary>
        /// Runs through all messages in all internal queues and ensures they are processed.
        /// </summary>
        protected void FlushMessages()
        {
            // TODO: Ensure FlushMessages method is limited to a single active call.

            //var ws = this.GetMessageDestinationStream();

            var ser = ProtoBuf.Serializer.CreateFormatter<MessageContainer>();

            lock (this.outgoingActive)
            {
                if (this.outgoingActive.Count == 0)
                    this.SwapBuffersOutgoing();

                if (this.outgoingActive.Count > 0)
                {
                    var msg = this.outgoingActive.Dequeue();

                    if (msg != null)
                    {
                        this.SendMessage(new MessageContainer(msg));
                    }
                }
            }
        }

        protected virtual void OnMessageReceived(object e)
        {
            this.MessageReceived(this, null);


        }

        protected void SwapBuffersIncoming()
        {
            // Swap the buffers used for incoming messages.
            try
            {
                // Attempt to grab the active queue. If this fails it probably means that
                // it has messages and they're being processed still.
                // Always lock active first, then inactive.
                if (Monitor.TryEnter(this.incomingActive))
                {
                    // Only swap if the active buffer is empty (all the messages in it were processed)
                    // Otherwise, the queue would become out-of-order very quickly.
                    if (this.incomingActive.Count > 0)
                        return;

                    var q = this.incomingInactive;
                    this.incomingInactive = this.incomingActive;
                    this.incomingActive = q;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Monitor.IsEntered(this.incomingActive))
                    Monitor.Exit(this.incomingActive);
            }
        }

        protected void SwapBuffersOutgoing()
        {
            // Swap the buffers used for outgoing messages.
            try
            {
                if (Monitor.TryEnter(this.outgoingActive, 1))
                {
                    lock (this.outgoingInactive)
                    {
                        if (outgoingActive.Count > 0)
                            return;

                        var q = this.outgoingInactive;
                        this.outgoingInactive = this.outgoingActive;
                        this.outgoingActive = q;
                    }
                }
            }
            catch (Exception ex)
            {
                if (Monitor.IsEntered(this.outgoingActive))
                    Monitor.Exit(this.outgoingActive);

                throw ex;
            }
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.outgoingActive != null);
            Contract.Invariant(this.outgoingInactive != null);
            Contract.Invariant(this.incomingActive != null);
            Contract.Invariant(this.incomingInactive != null);

        }
    }
}
