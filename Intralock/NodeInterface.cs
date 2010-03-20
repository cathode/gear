using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intralock
{
    /// <summary>
    /// Represents a communication channel used by a <see cref="Node"/> to synchronize state with another. Hides the underlying method of communication (socket, named pipe, cross-thread call, etc.)
    /// </summary>
    public abstract class NodeInterface
    {
        //public 
    }
}
