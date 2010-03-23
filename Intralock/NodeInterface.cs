using System;
using System.Collections.Generic;

namespace Intralock
{
    /// <summary>
    /// Represents a communication channel used by a <see cref="Node"/> to synchronize state with another. Hides the underlying method of communication (socket, named pipe, cross-thread call, etc.)
    /// </summary>
    /// <remarks>
    /// Encapsulates a pair of queues. <see cref="Update"/> items are added to the send queue by the local <see cref="Node"/>. When the <see cref="NodeInterface.Flush"/> method is called,
    /// all queued updates on the send queue are removed and sent to the remote <see cref="Node"/> at once (in the order they were originally enqueued).
    /// </remarks>
    public abstract class NodeInterface
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeInterface"/> class.
        /// </summary>
        protected NodeInterface()
        {
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the number of <see cref="Update"/>s that are in the send queue.
        /// </summary>
        public abstract int SendCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of <see cref="Update"/>s that are in the receive queue.
        /// </summary>
        public abstract int ReceiveCount
        {
            get;
        }

        /// <summary>
        /// Gets or sets the status of the current <see cref="NodeInterface"/>.
        /// </summary>
        public abstract NodeInterfaceStatus Status
        {
            get;
        }

        /// <summary>
        /// Gets the latency between the local node and the remote node.
        /// </summary>
        public abstract TimeSpan Latency
        {
            get;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Adds the specified <see cref="Update"/> onto the send queue.
        /// </summary>
        /// <param name="update">The <see cref="Update"/> instance to push to the send queue.</param>
        public abstract void Enqueue(Update update);

        /// <summary>
        /// Adds the specified range of <see cref="Update"/>s onto the send queue.
        /// </summary>
        /// <param name="updates">A collection of <see cref="Update"/> instnaces to push to the send queue.</param>
        public virtual void Enqueue(IEnumerable<Update> updates)
        {
            foreach (Update update in updates)
                this.Enqueue(update);
        }

        /// <summary>
        /// Removes the next <see cref="Update"/> from the receive queue and returns it.
        /// </summary>
        /// <returns>The <see cref="Update"/> instance that was popped from the receive queue.</returns>
        public abstract Update Dequeue();

        /// <summary>
        /// Removes the specified number of <see cref="Update"/>s from the receive queue and returns them in the order they were removed.
        /// </summary>
        /// <param name="max">The maximum number of <see cref="Update"/>s to dequeue.</param>
        /// <returns>A collection of <see cref="Update"/>s that were dequeued.</returns>
        public virtual IEnumerable<Update> Dequeue(int max)
        {
            var result = new Update[Math.Min(this.ReceiveCount, max)];
            for (int i = 0; i < result.Length; i++)
                result[i] = this.Dequeue();

            return result;
        }

        /// <summary>
        /// Causes all updates in the send queue to be sent to the remote node and receives any updates sent from the remote node.
        /// </summary>
        public abstract void Flush();
        #endregion
    }
}
