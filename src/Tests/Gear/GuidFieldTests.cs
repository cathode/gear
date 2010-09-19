/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Gear;

namespace Tests.Gear
{
    [TestFixture]
    public class GuidFieldTests
    {
        [Test(Description="Ensures that the GUID value remains the same when serialized, then deserialized.")]
        public void RoundTripTest()
        {
            var field = new GuidField();
            var guid = Guid.NewGuid();
            field.Value = guid;

            var buffer = new byte[field.Size];
            field.CopyTo(buffer);

            var field2 = new GuidField();
            field2.CopyFrom(buffer);

            Assert.AreEqual(guid, field2.Value);
        }
    }
}
