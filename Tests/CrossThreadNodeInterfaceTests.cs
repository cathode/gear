using Intralock;
using NUnit.Framework;

namespace Tests.Intralock
{
    /// <summary>
    /// Tests for the <see cref="CrossThreadNodeInterface"/> class.
    /// </summary>
    public class CrossThreadNodeInterfaceTests
    {
        /// <summary>
        /// Verifies that enquing an update increments the reported count of updates in the send queue by 1.
        /// </summary>
        [Test]
        public void EnquingUpdateShouldIncrementSendCount()
        {
            var local = new CrossThreadNodeInterface();

            int before = local.SendCount;
            local.Enqueue(new Update());
            int after = local.SendCount;

            Assert.AreEqual(before + 1, after);
        }

        /// <summary>
        /// Verifies that after the <see cref="CrossThreadNodeInterface.Flush"/> method returns,
        /// the send queue is reset to empty.
        /// </summary>
        [Test]
        public void FlushShouldRemoveAllItemsFromSendQueue()
        {
            var local = new CrossThreadNodeInterface();

            local.Enqueue(new Update());
            local.Flush();
            int count = local.SendCount;

            Assert.AreEqual(0, count);
        }

        /// <summary>
        /// Tests to verify that when enquing updates, they are dequeued by the remote node
        /// in the same order they were enqued.
        /// </summary>
        [Test]
        public void EnqueueOrderShouldBePreserved()
        {
            var local = new CrossThreadNodeInterface();

            Update first = new Update();
            Update second = new Update();
            Update third = new Update();

            local.Enqueue(first);
            local.Enqueue(second);
            local.Enqueue(third);

            local.Flush();

            var remote = local.Remote;
            var remoteFirst = remote.Dequeue();
            var remoteSecond = remote.Dequeue();
            var remoteThird = remote.Dequeue();

            Assert.AreSame(first, remoteFirst);
            Assert.AreSame(second, remoteSecond);
            Assert.AreSame(third, remoteThird);
        }
    }
}
