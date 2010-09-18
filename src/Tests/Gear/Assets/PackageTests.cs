/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
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
        public void Placeholder()
        {
            Assert.Pass();
        }

        [Test]
        public void NewPackageIsEmpty()
        {
            Package pkg = Package.CreateInMemory();
            Assert.AreEqual(pkg.Count, 0);
        }

        [Test]
        public void CanAddAssetToPackage()
        {
            try
            {
                Package pkg = Package.CreateInMemory();
                Asset asset = new ExampleAsset();
                pkg.Add(asset);
                Assert.Pass();
            }
            catch
            {
                Assert.Fail("The operation should have run without throwing an exception.");
            }
        }

        [Test]
        public void CanRemoveAssets()
        {
            try
            {
                Package pkg = Package.CreateInMemory();
                Asset asset = new ExampleAsset();
                pkg.Add(asset);
                pkg.Remove(asset);
            }
            catch
            {
                Assert.Fail("The operation should have run without throwing an exception.");
            }
        }

        [Test]
        public void RemoveNonExistingAssetFails()
        {
            Package pkg = Package.CreateInMemory();
            Asset asset = new ExampleAsset();
            bool actual = pkg.Remove(asset);

            Assert.AreEqual(false, actual);
        }

        [Test]
        public void RemoveExistingAssetSucceeds()
        {
            Package pkg = Package.CreateInMemory();
            Asset asset = new ExampleAsset();
            pkg.Add(asset);
            bool actual = pkg.Remove(asset);

            Assert.AreEqual(true, actual);
        }

        [Test(Description = "Ensures that an asset cannot be added to the same package twice.")]
        public void AddingExistingAssetFails()
        {
            var pkg = Package.CreateInMemory();
            var asset = new ExampleAsset();
            bool actual = true;
            bool expected = false;
            if (pkg.Add(asset))
                actual = pkg.Add(asset);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddingNullAssetFails()
        {
            var pkg = Package.CreateInMemory();
            bool actual = pkg.Add(null);
            bool expected = false;

            Assert.AreEqual(expected, actual);
        }

        public sealed class ExampleAsset : Asset
        {
        }
    }
}
