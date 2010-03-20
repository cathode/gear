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
        public override TimeSpan Latency
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override void DoSync()
        {
            throw new NotImplementedException();
        }
    }
}
