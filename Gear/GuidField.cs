﻿/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
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
    /// Represents a <see cref="Field"/> that holds a <see cref="Guid"/> value.
    /// </summary>
    public sealed class GuidField : Field.FieldBase<Guid>
    {
        #region Fields

        #endregion
        #region Constructors
        public GuidField()
        {
        }
        public GuidField(Guid value)
            : base(value)
        {
        }
        #endregion
        #region Properties
        public override FieldKind Id
        {
            get
            {
                return FieldKind.Guid;
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
            this.Value.ToByteArray().CopyTo(buffer, startIndex);
            return 16;
        }

        public override int CopyFrom(byte[] buffer, int startIndex, int count)
        {
            if (count < 16)
                throw new NotImplementedException();

            byte[] guid = new byte[16];
            Array.Copy(buffer, startIndex, guid, 0, 16);
            this.Value = new Guid(guid);
            return 16;
        }
        #endregion
    }
}