/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear
{
    /// <summary>
    /// Represents a <see cref="Field"/> that holds a <see cref="Version"/>.
    /// </summary>
    public class VersionField : Field
    {
        #region Fields
        private Version value;
        #endregion
        #region Constructors
        public VersionField()
        {
            this.value = default(Version);
        }
        public VersionField(Version value)
        {
            this.value = value;
        }
        #endregion
        #region Properties
        public override FieldKind Id
        {
            get
            {
                return FieldKind.Version;
            }
        }

        public override short Size
        {
            get
            {
                return 16;
            }
        }

        public Version Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        #endregion

        public override int CopyTo(byte[] buffer, int startIndex)
        {
            if ((buffer.Length - startIndex) < 16)
                throw new NotImplementedException();

            DataBuffer db = new DataBuffer(buffer, ByteOrder.NetworkByteOrder);
            db.Position = startIndex;
            db.WriteInt32(this.value.Major);
            db.WriteInt32(this.value.Minor);
            db.WriteInt32(this.value.Build);
            db.WriteInt32(this.value.Revision);

            return 16;
        }

        public override int CopyFrom(byte[] buffer, int startIndex, int count)
        {
            if ((buffer.Length - startIndex) < 16)
                throw new NotImplementedException();

            DataBuffer db = new DataBuffer(buffer, ByteOrder.NetworkByteOrder);
            db.Position = startIndex;

            var major = db.ReadInt32();
            var minor = db.ReadInt32();
            var build = db.ReadInt32();
            var revision = db.ReadInt32();

            this.value = new Version(major, minor, build, revision);
            return 16;
        }
    }
}
