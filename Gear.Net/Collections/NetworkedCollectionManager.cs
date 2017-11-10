using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net.Collections
{
    /// <summary>
    /// Provides a supervisory class that assists the <see cref="NetworkedCollection{T}"/> class.
    /// </summary>
    public class NetworkedCollectionManager
    {
        #region Fields
        private static Lazy<NetworkedCollectionManager> instance = new Lazy<NetworkedCollectionManager>(true);

        private readonly Dictionary<int, NetworkedCollectionProxy> collections = new Dictionary<int, NetworkedCollectionProxy>();
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkedCollectionManager"/> class.
        /// </summary>
        private NetworkedCollectionManager()
        {
        }
        #endregion

        #region Properties

        public static NetworkedCollectionManager Instance
        {
            get
            {
                Contract.Ensures(Contract.Result<NetworkedCollectionManager>() != null);

                return NetworkedCollectionManager.instance.Value;
            }
        }

        #endregion

        /// <summary>
        /// Includes the specified <see cref="Channel"/> in networked collection communications.
        /// </summary>
        /// <param name="remote"></param>
        public void IncludeChannel(Channel remote)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Requests a list of networked collections that exist on the remote peer.
        /// </summary>
        /// <param name="peer"></param>
        public void QueryPeerCollections(Channel peer)
        {
            throw new NotImplementedException();
        }

        #region Methods - Internal

        /// <summary>
        /// Associates the networked collection with this manager instance.
        /// </summary>
        /// <param name="collection">A <see cref="NetworkedCollectionProxy"/> that wraps the collection to be attached.</param>
        internal void AttachCollection(NetworkedCollectionProxy collection)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
