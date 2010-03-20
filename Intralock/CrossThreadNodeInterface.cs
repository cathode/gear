using System;
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
        private CrossThreadNodeInterface remote;
        private readonly object receiveLock = new object();
        private readonly object sendLock = new object();
        #endregion
        #region Constructors
        private CrossThreadNodeInterface()
        {
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
            var local = new CrossThreadNodeInterface();
            local.remote = new CrossThreadNodeInterface();
            local.remote.remote = local;
            return local;
        }

        /// <summary>
        /// Performs synchronization with the remote node.
        /// </summary>
        public override void Flush()
        {

        }
        #endregion
    }
}
