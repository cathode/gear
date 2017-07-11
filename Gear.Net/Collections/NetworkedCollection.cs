using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.Collections
{
    /// <summary>
    /// Implements a generic collection that is synchronized with a remote endpoint. This class is thread-safe.
    /// </summary>
    public class NetworkedCollection<T> : ICollection<T>, IObservable<T>, IMessagePublisher
    {
        private readonly object syncLock = new object();

        private System.Collections.Generic.Dictionary<int, T> items;

        public event EventHandler<MessageEventArgs> MessageAvailable;
        public event EventHandler ShuttingDown;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkedCollection"/> class.
        /// </summary>
        public NetworkedCollection()
        {

        }

        public long CollectionId { get; internal set; }

        public ReplicationMode Mode { get; set; }

        #region Methods

        /// <summary>
        /// Becomes a consumer for a remote networked collection with the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public bool Consume(Guid id, Channel channel)
        {
            var msg = new NetworkedCollectionUpdateMessage();
            

            return false;
        }
        
        protected virtual void OnMessageAvailable(MessageEventArgs e)
        {
            this.MessageAvailable?.Invoke(this, e);
        }

        #endregion


        #region ICollection<T> members

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                if (this.Mode == ReplicationMode.Producer)
                    return false;
                else
                    return true;
            }
        }

        public void Add(T item)
        {
            if (this.IsReadOnly)
                throw new InvalidOperationException("This collection is read-only.");

            // Add the item to the wrapped collection:
            var k = item.GetHashCode();
            this.items.Add(k, item);

            // Build update message:
            var msg = new NetworkedCollectionUpdateMessage();
            msg.CollectionId = this.CollectionId;
            msg.Action = NetworkedCollectionAction.Add;
            

        }

        public void Clear()
        {
            if (this.IsReadOnly)
                throw new InvalidOperationException("This collection is read-only.");
        }

        public bool Contains(T item)
        {
            return this.items.Values.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the specified item from the collection. If the specified item does not already exist within the collection, no action is taken.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            if (this.IsReadOnly)
                throw new InvalidOperationException("This collection is read-only.");

            if (!this.items.Values.Contains(item))
                return true;
            

            var msg = new NetworkedCollectionUpdateMessage();

            

            return true;
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.Values.GetEnumerator();
        }
        #endregion

        
        public IDisposable Subscribe(IObserver<T> observer)
        {
            throw new NotImplementedException();
        }

    }
}
