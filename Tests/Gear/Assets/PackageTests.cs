using System;
using System.Collections.Generic;

using System.Text;
using Gear.Assets;
using NUnit.Framework;

namespace Tests.Gear.Assets
{
    [TestFixture]
    public class PackageTests
    {
        /* Things to know about a package:
         * 
         * It contains assets
         * It needs to support reading and writing operations
         * It needs to support appending to an existing asset
         * It needs to support adding new assets and removing existing assets.
         * It needs to be verifiable (e.g. digital signature or some other method of guaranteeing contents)
         * It needs to support a way of referencing other packages
         * The ability to add and remove assets and read and write assets must support arbitrary accesses.
         */
        private Package pkg;

        [TestFixtureSetUp]
        public void SetUp()
        {
            this.pkg = Package.CreateInMemory();
        }
        [TestFixtureTearDown]
        public void TearDown()
        {
            this.pkg.Dispose();
        }

        [Test]
        public void AssetCountShouldReturnOneWhenOneAssetAdded()
        {
            pkg.Include(new TestAsset());

            Assert.AreEqual(1, pkg.Count);
        }

        /// <summary>
        /// Ensures that an asset instance shows up as being part of a package after it is added to that package.
        /// </summary>
        [Test]
        public void AssetShouldExistInPackageAfterBeingAdded()
        {
            Asset asset = new TestAsset();

            pkg.Include(asset);

            bool expected = true;
            bool actual = false;

            Assert.AreEqual(expected, actual);
        }
    }
    public class TestAsset : Asset
    {
    }
}
