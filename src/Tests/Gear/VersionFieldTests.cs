/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using Gear.Net.Messaging;
using NUnit.Framework;
using Gear;

namespace Tests.Gear.Net.Messaging
{
    [TestFixture]
    public class VersionFieldTests
    {
        [Test(Description="Ensures that the Version will be preserved when encoded and then decoded.")]
        public void EnsureRoundTripAccuracy()
        {
            Version version = new Version(11, 3, 5533, 30344);

            VersionField field = new VersionField(version);

            byte[] buffer = new byte[field.Size];
            int copied = field.CopyTo(buffer, 0);

            VersionField field2 = new VersionField();
            int copied2 = field2.CopyFrom(buffer, 0, copied);

            Assert.AreEqual(version, field2.Value);
        }
    }
}
