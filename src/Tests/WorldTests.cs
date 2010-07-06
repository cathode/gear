using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Tests.Intralock
{
    [TestFixture]
    public class WorldTests
    {
        /* World - Represents a virtual environment and everything in it. Basically, a huge global container that directly or indirectly contains everything.
         * 
         * Basics about a World:
         * 
         * A World contains entities.
         * A World can't contain another World, that would be MADNESS!
         * One or more servers host a single World, but a server can only host one World at a time.
         * The state of a World should be persistable (to a file or database or another server).
         * The state of a World should support synchronization between servers.
         * A World instance needs to be thread-safe.
         * 
         * Implementation details:
         * When an entity is added to the world, the EntityCount should reflect the added entity.
         * When an entity with child entities is added to the world, any child entities that aren't associated with the World should be added as well.
         * 
         */
        [Test]
        public void WorldContainsEntities()
        {
            Assert.Pass();
        }

        [Test]
        public void WorldShouldBeWritableByMultipleSimultaneousThreads()
        {
            Assert.Pass();
        }
    }
}
