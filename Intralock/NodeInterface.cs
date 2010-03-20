using System;
using System.Collections.Generic;
using System.Text;

namespace Intralock
{
    /// <summary>
    /// Represents a communication channel used by a <see cref="Node"/> to synchronize state with another. Hides the underlying method of communication (socket, named pipe, cross-thread call, etc.)
    /// </summary>
    /// <remarks>
    /// Encapsulates a pair of queues. <see cref="Update"/> items are added to the send queue by the local <see cref="Node"/>. When the <see cref="NodeInterface.Flush"/> method is called,
    /// all queued updates on the send queue are removed and sent to the remote <see cref="Node"/> at once (in the order they were originally enqueued). At the same time, any updates that were sent
    /// from the remote node are added to the receive queue.
    /// </remarks>
    public abstract class NodeInterface
    {
        #region Fields
        /// <summary>
        /// Local storage of updates to be sent.
        /// </summary>
        protected readonly Queue<Update> Send = new Queue<Update>();

        /// <summary>
        /// Local storage of updates that have been received.
        /// </summary>
        protected readonly Queue<Update> Receive = new Queue<Update>();
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeInterface"/> class.
        /// </summary>
        protected NodeInterface()
        {
            this.Send = new Queue<Update>();
            this.Receive = new Queue<Update>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the number of <see cref="Update"/>s that are in the send queue.
        /// </summary>
        public int SendCount
        {
            get
            {
                return this.Send.Count;
            }
        }

        /// <summary>
        /// Gets the number of <see cref="Update"/>s that are in the receive queue.
        /// </summary>
        public int ReceiveCount
        {
            get
            {
                return this.Receive.Count;
            }
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
        public void Enqueue(Update update)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the specified range of <see cref="Update"/>s onto the send queue.
        /// </summary>
        /// <param name="update">A collection of <see cref="Update"/> instnaces to push to the send queue.</param>
        public void Enqueue(IEnumerable<Update> update)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the next <see cref="Update"/> from the receive queue and returns it.
        /// </summary>
        /// <returns>The <see cref="Update"/> instance that was popped from the receive queue.</returns>
        public Update Dequeue()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the specified number of <see cref="Update"/>s from the receive queue and returns them in the order they were removed.
        /// </summary>
        /// <param name="max">The maximum number of <see cref="Update"/>s to dequeue.</param>
        /// <returns>A collection of <see cref="Update"/>s that were dequeued.</returns>
        public IEnumerable<Update> Dequeue(int max)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Causes all updates in the send queue to be sent to the remote node and receives any updates sent from the remote node.
        /// </summary>
        public abstract void Flush();
        #endregion
    }
}
