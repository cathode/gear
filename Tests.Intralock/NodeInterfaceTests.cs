using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Intralock
{
    public class NodeInterfaceTests
    {
        /* NodeInterface - Transparently relays state synchronizations between a local node, and a remote node.
         * 
         * BASICS:
         *  A NodeInterface functions as a bi-directional link; has a send and receive.
         *  Send and Receive are each a queue of updates.
         *  "Send" is for updates originating with the local Node.
         *  "Receive" is for updates originating with the remote Node, which might be on another thread, another process, or another machine.
         *  The Send and Receive queues are seen from the point of view of the local node. Thus, the "Send" queue of the local Node is the "Receive" queue of the remote Node.
         *  When the local Node pushes an update onto the Send queue, it will appear at some point in the Receive queue of the remote Node.
         *  When the local Node pops an update from the Receive queue, it will dissapear at some point from the Send queue of the remote Node.
         *  Updates are always pushed and popped from a single thread (that the Node is running on).
         *  Specific implementations of the NodeInterface class might need to be thread safe.
         *  The order that updates are processed is always preserved. The second update pushed to the Send queue will never be processed by the remote node before the first.
         * 
         * ADVANCED TOPICS:
         *  A NodeInterface should be able to "collapse" updates that redundantly update the same field. This behavior might invalidate other facts about behavior.
         *
         * 
         * Facts about a NodeInterface (from the point of view of a the local Node):
         * 
         * When I add an Update to the Send queue, I should be able to check the size of the Send queue and see that it has increased by 1.
         * When I remove an Update from the Receive queue, I should be able to check the size of the Receive queue and see that it has decreased by 1.
         * 
         */
    }
}
