using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Gear.Net.Collections
{
    /// <summary>
    /// Implements a generic collection that is synchronized with a remote endpoint. This class is thread-safe.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    public class NetworkedCollection<T> : ICollection<T>, IObservable<T>, IMessagePublisher
    {
        #region Fields
        private readonly object syncLock = new object();

        private readonly System.Collections.Generic.Dictionary<int, T> items = new Dictionary<int, T>();

        private readonly List<object> consumers;

        private Channel source;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkedCollection{T}"/> class.
        /// </summary>
        public NetworkedCollection()
        {
        }

        #endregion
        #region Events

        /// <summary>
        /// Raised when an item is added to the collection.
        /// </summary>
        /// <remarks>
        /// This event is raised when items are added to the collection by code running locally,
        /// and also when a tracked peer adds an item to a collection.
        /// </remarks>
        public event EventHandler<NetworkedCollectionItemEventArgs<T>> ItemAdded;

        /// <summary>
        /// Raised when a network message is published by this message publisher.
        /// </summary>
        /// <see cref="IMessagePublisher"/>
        public event EventHandler<MessageEventArgs> MessageAvailable;

        /// <summary>
        /// Raised when the <see cref="NetworkedCollection{T}"/> ceases synchronizing contents with peers.
        /// </summary>
        public event EventHandler ShuttingDown;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique id of the current <see cref="NetworkedCollection{T}"/> instance.
        /// </summary>
        public Guid CollectionInstanceId { get; set; }

        /// <summary>
        /// Gets or sets the numeric id of the group that the collection belongs to.
        /// </summary>
        public long CollectionGroupId { get; set; }

        /// <summary>
        /// Gets or sets a name identifier for the collection group.
        /// </summary>
        public string CollectionGroupName { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ReplicationMode"/> that determines how the current networked collection instance should operate.
        /// </summary>
        public ReplicationMode Mode { get; set; }

        /// <summary>
        /// Gets a value indicating the number of items held in the collection.
        /// </summary>
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the collection is read-only.
        /// </summary>
        /// <remarks>
        /// The collection may be read-only depending on the <see cref="Mode"/> it is in.
        /// </remarks>
        public bool IsReadOnly
        {
            get
            {
                if (this.Mode == ReplicationMode.Producer)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Becomes a consumer for a remote networked collection with the specified id.
        /// </summary>
        /// <param name="id">The numeric id of the remote collection to subscribe to.</param>
        /// <param name="channel">A <see cref="Channel"/> that represents the remote endpoint where the collection exists.</param>
        public void Consume(long id, Channel channel)
        {
            Contract.Requires<ArgumentNullException>(channel != null);

            if (this.Mode != ReplicationMode.None)
            {
                throw new NotImplementedException();
            }

            var msg = new NetworkedCollectionStateMessage();

            //msg.Action = NetworkedCollectionAction.Join;
            //msg.CollectionGroupId = id;
            //msg.Data = null;

            this.Mode = ReplicationMode.Consumer;
            this.CollectionGroupId = id;
            this.source = channel;

            this.source.RegisterHandler<NetworkedCollectionUpdateMessage>(this.MessageHandler_NetworkedCollectionUpdate, this);

            this.source.Send(msg);
        }

        /// <summary>
        /// Performs a manual pull synchronization with a remote peer.
        /// </summary>
        /// <param name="id">The collection id on the remote peer to pull from.</param>
        /// <param name="remote">The <see cref="Channel"/> that will be used for communication with the remote peer.</param>
        public void PullOnce(long id, Channel remote)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs a manual push synchronization with a remote peer.
        /// </summary>
        /// <param name="remote">The <see cref="Channel"/> that will be used for communication with the remote peer.</param>
        public void PushOnce(Channel remote)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the specified item to the collection.
        /// </summary>
        /// <param name="item">The item to add to the colllection.</param>
        /// <exception cref="InvalidOperationException">Thrown when the networked collection is read-only,
        /// typically because the <see cref="Mode"/> is <see cref="ReplicationMode.Consumer"/>.</exception>
        public void Add(T item)
        {
            if (this.IsReadOnly)
            {
                throw new InvalidOperationException("This collection is read-only.");
            }

            var k = this.GetItemKey(item);

            if (this.items.ContainsKey(k))
            {
                throw new InvalidOperationException("Item with the specified key already exists in this collection.");
                //return;
            }

            // Add the item to the wrapped collection:
            this.items.Add(k, item);

            // Build update message:
            var msg = new NetworkedCollectionUpdateMessage();
            msg.CollectionGroupId = this.CollectionGroupId;
            msg.Action = NetworkedCollectionUpdateAction.Add;
            msg.Data = this.SerializeItem(item);

            this.OnMessageAvailable(new MessageEventArgs(msg));
        }

        /// <summary>
        /// Removes all the items from the collection.
        /// </summary>
        public void Clear()
        {
            if (this.IsReadOnly)
            {
                throw new InvalidOperationException("This collection is read-only.");
            }
        }

        /// <summary>
        /// Returns a value indicating whether the specified item exists in the collection.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>true if the collection contains the item; otherwise false.</returns>
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
            {
                throw new InvalidOperationException("This collection is read-only.");
            }

            if (!this.items.Values.Contains(item))
            {
                return true;
            }

            var msg = new NetworkedCollectionUpdateMessage();

            msg.Action = NetworkedCollectionUpdateAction.Remove;
            msg.Data = this.GetItemKey(item).ToString();

            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.Values.GetEnumerator();
        }

        public void ChangeMode(ReplicationMode mode)
        {

        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            throw new NotImplementedException();
        }

        public void BindToChannel(Channel channel)
        {
            channel.RegisterHandler<NetworkedCollectionUpdateMessage>(this.MessageHandler_NetworkedCollectionUpdate, this);
        }

        protected virtual void OnItemAdded(T item)
        {
            this.ItemAdded?.Invoke(this, new NetworkedCollectionItemEventArgs<T> { Action = NetworkedCollectionUpdateAction.Add, Items = new[] { item } });
        }

        protected virtual string SerializeItem(T item)
        {
            return JsonConvert.SerializeObject(item);
        }

        protected virtual T DeserializeItem(string item)
        {
            return JsonConvert.DeserializeObject<T>(item);
        }

        protected virtual string SerializeItems(IEnumerable<T> items)
        {
            return JsonConvert.SerializeObject(items);
        }

        protected virtual IEnumerable<T> DeserializeItems(string items)
        {
            return JsonConvert.DeserializeObject<T[]>(items);
        }

        protected virtual int GetItemKey(T item)
        {
            return item.GetHashCode();
        }

        protected virtual void OnMessageAvailable(MessageEventArgs e)
        {
            this.MessageAvailable?.Invoke(this, e);
        }

        protected void SendItems(IEnumerable<T> items, Channel sendTo = null)
        {
            if (items == null || items.Count() == 0)
            {
                return;
            }

            // Build update message:
            var msg = new NetworkedCollectionUpdateMessage();
            msg.CollectionGroupId = this.CollectionGroupId;
            msg.Action = NetworkedCollectionUpdateAction.Add;
            if (items.Count() > 1)
            {
                msg.DataHints = MessageDataHint.MultipleItems;
                msg.Data = this.SerializeItems(items);
            }
            else
            {
                var item = items.Single();
                msg.Data = this.SerializeItem(item);
            }

            if (sendTo != null)
            {
                // Send items to specific endpoint
                sendTo.Send(msg);
            }
            else
            {
                // Publish messasge to all subscribers
                this.OnMessageAvailable(new MessageEventArgs(msg));
            }
        }

        private void MessageHandler_NetworkedCollectionUpdate(MessageEventArgs e, NetworkedCollectionUpdateMessage msg)
        {
            if (msg == null)
            {
                return;
            }
            else if (msg.CollectionGroupId != this.CollectionGroupId)
            {
                return;
            }

            if (this.Mode == ReplicationMode.Consumer)
            {
                if (msg.Action == NetworkedCollectionUpdateAction.Add)
                {
                    if (msg.DataHints == MessageDataHint.MultipleItems)
                    {
                        var items = this.DeserializeItems(msg.Data);
                        foreach (var item in items)
                        {
                            var key = this.GetItemKey(item);

                            if (!this.items.ContainsKey(key))
                            {
                                this.items.Add(key, item);
                                this.OnItemAdded(item);
                            }
                        }
                    }
                    else
                    {
                        var item = this.DeserializeItem(msg.Data);
                        this.items.Add(this.GetItemKey(item), item);

                        this.OnItemAdded(item);
                    }
                }
            }
            else if (this.Mode == ReplicationMode.Producer)
            {
                //if (msg.Action == NetworkedCollectionAction.Join)
                //{
                //    // The remote endpoint is joining as a consumer or peer to this networked collection instance.
                //    this.SendItems(this.items.Values, e.Channel);
                //}
            }

        }

        [ContractInvariantMethod]
        private void ContractInvariants()
        {
            Contract.Invariant(this.items != null);
        }
        #endregion
    }
}
