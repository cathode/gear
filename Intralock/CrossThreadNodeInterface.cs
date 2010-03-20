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
        /// Performs synchronization with the remote node.
        /// </summary>
        protected override void PerformSync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
