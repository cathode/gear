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
    public sealed class StringMessageField : MessageField
    {
        #region Fields
        private string value;
        #endregion
        public override MessageFieldId Id
        {
            get
            {
                return MessageFieldId.String;
            }
        }
        public string Value
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

        public override int CopyTo(byte[] buffer, int startIndex, int count)
        {
            throw new NotImplementedException();
        }

        public override int CopyFrom(byte[] buffer, int startIndex, int count)
        {
            throw new NotImplementedException();
        }

        public override int Size
        {
            get
            {
                return Encoding.UTF8.GetByteCount(this.value);
            }
        }
    }
}
