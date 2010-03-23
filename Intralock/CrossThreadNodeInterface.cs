using System;
using System.Collections.Generic;

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

        private Queue<Update> primarySend = new Queue<Update>();

        /// <summary>
        /// A secondary buffer to enable double-buffered send queuing.
        /// </summary>
        private Queue<Update> secondarySend = new Queue<Update>();

        private Queue<Update> Receive = new Queue<Update>();
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossThreadNodeInterface"/> class.
        /// </summary>
        /// <remarks>
        /// Two instances are created. The second one is assigned as the "remote" node interface.
        /// </remarks>
        public CrossThreadNodeInterface()
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
                return TimeSpan.Zero;
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
        /// Removes the next <see cref="Update"/> from the receive queue and returns it.
        /// </summary>
        /// <returns>The <see cref="Update"/> instance that was popped from the receive queue.</returns>
        public override Update Dequeue()
        {
            lock (this.flushLock)
                return this.Receive.Dequeue();
        }

        /// <summary>
        /// Removes the specified number of <see cref="Update"/>s from the receive queue and returns them in the order they were removed.
        /// </summary>
        /// <param name="max">The maximum number of <seesho cref="Update"/>s to dequeue.</param>
        /// <returns>A collection of <see cref="Update"/>s that were dequeued.</returns>
        public override IEnumerable<Update> Dequeue(int max)
        {
            lock (this.flushLock)
            {
                if (this.Receive.Count == 0)
                    return new Update[0];

                var result = new Update[Math.Min(this.ReceiveCount, max)];
                for (int i = 0; i < result.Length; i++)
                    result[i] = this.Receive.Dequeue();

                return result;
            }
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
                this.secondarySend = System.Threading.Interlocked.Exchange<Queue<Update>>(ref this.primarySend, this.secondarySend); // Basically, double buffering.
                
                while (this.secondarySend.Count > 0)
                    this.remote.Receive.Enqueue(this.secondarySend.Dequeue());
            }
        }
        #endregion

        public override int SendCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int ReceiveCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void Enqueue(Update update)
        {
            throw new NotImplementedException();
        }

        public override NodeInterfaceStatus Status
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
