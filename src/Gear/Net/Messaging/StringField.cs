/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
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
    public sealed class StringField : MessageField
    {
        #region Fields
        private string value;
        #endregion
        #region Constructors
        public StringField()
        {
            this.value = string.Empty;
        }
        public StringField(string value)
        {
            this.value = value ?? string.Empty;
        }
        #endregion
        #region Properties
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
                this.value = value ?? string.Empty;
            }
        }

        public override short Size
        {
            get
            {
                return (short)Encoding.UTF8.GetByteCount(this.value);
            }
        }
        #endregion
        #region Methods
        public override int CopyTo(byte[] buffer, int startIndex)
        {
            var bytes = Encoding.UTF8.GetBytes(this.value);
            bytes.CopyTo(buffer, startIndex);
            return bytes.Length;
        }

        public override int CopyFrom(byte[] buffer, int startIndex, int count)
        {
            this.value = Encoding.UTF8.GetString(buffer, startIndex, count);
            return count;
        }
        #endregion
        
    }
}
