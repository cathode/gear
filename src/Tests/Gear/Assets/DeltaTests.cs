using System;
using System.Collections.Generic;

using System.Text;
using NUnit.Framework;
using Gear.Assets;

namespace Tests.Gear.Assets
{
    [TestFixture]
    public class DeltaTests
    {
        /* Delta - Defines a binary difference between an "original" value and a "current" value.
         * 
         * Applying a Delta to the "original" should yield the "current".
         * Reverse-applying a delta to the "current" should yield the "original".
         * Generating a Delta using the same value for both "original" and "current" should return a null or otherwise empty Delta.
         * Passing "null" to either original or current (but not both) is allowed.
         */

        private byte[] original;
        private byte[] current;

        [TestFixtureSetUp]
        public void SetUp()
        {
            this.original = Encoding.ASCII.GetBytes("Hello world");
            this.current = Encoding.ASCII.GetBytes("hELLO world");
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            this.original = null;
            this.current = null;
        }

        [Test]
        public void NullOrEmptyOriginalIsInvalid()
        {
            Assert.Fail();
        }

        [Test]
        public void NullOrEmptyCurrentIsInvalid()
        {
            Assert.Fail();
        }

        [Test]
        public void ApplyingDeltaToOriginalShouldYieldCurrent()
        {
            byte[] original = null;
            byte[] current = null;

            Delta delta = Delta.Calculate(original, current);

            byte[] postDelta = delta.Apply(original);

            bool expected = true;
            bool actual = current == postDelta;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ApplyingReverseDeltaToCurrentShouldYieldOriginal()
        {
            byte[] original = null;
            byte[] current = null;

            Delta delta = Delta.Calculate(original, current);

            byte[] postDelta = delta.ApplyReverse(current);

            bool expected = true;
            bool actual = original == postDelta;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Ensures that overloads of <see cref="Delta.Apply"/> yield the same results.
        /// </summary>
        [Test]
        public void OverloadsOfApplyShouldBeConsistent()
        {
            Delta delta = Delta.Calculate(this.original, this.current);

            byte[] normal = delta.Apply(this.original);

            byte[] byRef = new byte[0];

            delta.Apply(this.original, ref byRef);

            bool expected = true;
            bool actual = normal == byRef;

            Assert.AreEqual(expected, actual);
        }
    }
}
