using System;
using Gear.Net;
using NUnit.Framework;

namespace Gear_Tests
{
    [TestFixture]
    public class IPTargetTest
    {
        [Test]
        public void TestResolving()
        {
            var target = new IPTarget("controller", 9999);

            target.Resolve();

            Assert.Pass();
        }
    }
}