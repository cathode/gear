﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Intralock
{
    /// <summary>
    /// Provides a <see cref="NodeInterface"/> implementation that establishes a communication channel with a 
    /// </summary>
    public sealed class CrossThreadNodeInterface : NodeInterface
    {
        #region Fields
        /// <summary>
        /// A lock object which is shared between the local and remote interface, and is used to enable thread safety.
        /// </summary>
        private readonly object flushLock = new object();

        /// <summary>
        /// Backing field for the <see cref="CrossThreadNodeInterface.Remote"/> property.
        /// </summary>
        private readonly CrossThreadNodeInterface remote;

        /// <summary>
        /// A secondary buffer to enable double-buffered send queuing.
        /// </summary>
        private Queue<Update> secondarySend = new Queue<Update>();
        #endregion
        #region Constructors
        /// <summary>
        /// Prevents a default instance of the <see cref="CrossThreadNodeInterface"/> class from being created.
        /// </summary>
        private CrossThreadNodeInterface()
        {
            this.remote = new CrossThreadNodeInterface(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrossThreadNodeInterface"/> class.
        /// </summary>
        /// <param name="remote">The interface used by the remote node.</param>
        private CrossThreadNodeInterface(CrossThreadNodeInterface remote)
        {
            this.remote = remote;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the latency between the local node and the remote node.
        /// </summary>
        public override TimeSpan Latency
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the <see cref="CrossThreadNodeInterface"/> that is used by the remote node.
        /// </summary>
        public CrossThreadNodeInterface Remote
        {
            get
            {
                return this.remote;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Creates a linked pair of <see cref="CrossThreadNodeInterface"/> instances where each instance refers to the other as the "remote".
        /// </summary>
        /// <returns>One of the two linked <see cref="CrossThreadNodeInterface"/> instances.</returns>
        public static CrossThreadNodeInterface CreatePair()
        {
            return new CrossThreadNodeInterface();
        }

        /// <summary>
        /// Adds the specified <see cref="Update"/> onto the send queue.
        /// </summary>
        /// <param name="update">The <see cref="Update"/> instance to push to the send queue.</param>
        public override void Enqueue(Update update)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the next <see cref="Update"/> from the receive queue and returns it.
        /// </summary>
        /// <returns>The <see cref="Update"/> instance that was popped from the receive queue.</returns>
        public override Update Dequeue()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Pushes all unsent updates to the remote node.
        /// </summary>
        public override void Flush()
        {
            // Obtain a lock allowing us to safely write to the remote node interface.
            lock (this.remote.flushLock)
            {
                // Atomic exchange of the "active" send queue and the secondary, "inactive" send queue.
                // This allows lockless, safe access to the send queue of the local node interface.
                this.secondarySend = System.Threading.Interlocked.Exchange<Queue<Update>>(ref this.Send, this.secondarySend); // Basically, double buffering.

                while (this.secondarySend.Count > 0)
                    this.remote.Receive.Enqueue(this.secondarySend.Dequeue());
            }
        }
        #endregion
    }
}
