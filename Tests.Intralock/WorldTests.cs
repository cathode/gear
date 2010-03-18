using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Intralock
{
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
         */
        [Fact]
        public void ThisShouldFail()
        {
            Assert.Equal<bool>(false, true);
        }

        [Fact]
        public void ThisShouldPass()
        {
            Assert.Equal<int>(1, 1);
        }
    }

    public class World
    {

    }
}
