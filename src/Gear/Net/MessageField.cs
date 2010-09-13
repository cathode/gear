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

namespace Gear.Net
{
    /// <summary>
    /// Represents a serialized field of a <see cref="Message"/>.
    /// </summary>
    public class MessageField
    {
        #region Fields
        private readonly ushort id;
        private ushort length;
        private byte[] data;
        #endregion
        #region Properties
        public ushort Id
        {
            get
            {
                return this.id;
            }
        }
        public ushort Length
        {
            get
            {
                return this.length;
            }
            set
            {
                this.length = value;
            }
        }
        public byte[] Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }
        #endregion
    }
}
