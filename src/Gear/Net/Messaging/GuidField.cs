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

namespace Gear.Net.Messaging
{
    /// <summary>
    /// Represents a <see cref="MessageField"/> that holds a <see cref="Guid"/> current.
    /// </summary>
    public sealed class GuidField : MessageField
    {
        #region Fields
        private Guid value;
        #endregion
        #region Constructors
        public GuidField()
        {
            this.value = default(Guid);
        }
        public GuidField(Guid value)
        {
            this.value = value;
        }
        #endregion
        #region Properties
        public override MessageFieldId Id
        {
            get
            {
                return MessageFieldId.Guid;
            }
        }
        public Guid Value
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
        public override short Size
        {
            get
            {
                return 16;
            }
        }
        #endregion
        #region Methods
        public override int CopyTo(byte[] buffer, int startIndex)
        {
            this.value.ToByteArray().CopyTo(buffer, startIndex);
            return 16;
        }

        public override int CopyFrom(byte[] buffer, int startIndex, int count)
        {
            if (count < 16)
                throw new NotImplementedException();

            byte[] guid = new byte[16];
            Array.Copy(buffer, startIndex, guid, 0, 16);
            this.value = new Guid(guid);
            return 16;
        }
        #endregion
    }
}
