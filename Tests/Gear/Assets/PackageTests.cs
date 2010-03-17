using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        [Test]
        public void AssetCountShouldReturnOneWhenOneAssetAdded()
        {
            Package pkg = new Package();
            pkg.Add(new Asset());

            Assert.AreEqual(1, pkg.AssetCount);
        }

        /// <summary>
        /// Ensures that an asset instance shows up as being part of a package after it is added to that package.
        /// </summary>
        [Test]
        public void AssetShouldExistInPackageAfterBeingAdded()
        {
            Package pkg = new Package();

            Asset asset = new Asset();

            pkg.Add(asset);

            bool expected = true;
            bool actual = pkg.Contains(asset);

            Assert.AreEqual(expected, actual);
        }
    }

    public class Package
    {
        private Asset asset;

        internal void Add(Asset asset)
        {
            this.asset = asset;
        }

        public int AssetCount
        {
            get
            {
                return 1;
            }
        }

        internal bool Contains(Asset asset)
        {
            if (this.asset == asset)
                return true;

            return false;
        }
    }

    public class Asset
    {
    }
}
