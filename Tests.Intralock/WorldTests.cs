using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Intralock
{
    public class WorldTests
    {
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
