using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Intralock
{
    public class NodeTests
    {
        /* Node - Maintains a World or part of a World. Communicates with clients.
         * 
         * Aspects of a Node:
         * 
         * Communicates with other Nodes via NodeInterface.
         * Does all processing on one thread. Multi-core/Multi-processor systems use multiple nodes instead.
         * Responsible for replicating relevant state changes between nodes.
         * Partitioning of a World between Nodes can be done spatially or by entity type.
         * Needs to have some way of determining a master node (that makes authoritative decisions like authentication of new nodes)
         * Needs fault tolerance in the event that one or more remote nodes go offline without warning.
         * 
         * TENATIVE FEATURES:
         * Some way to support licensing on a per-client/per-node basis maybe? ("pay-per-node"?)
         */
    }
}
