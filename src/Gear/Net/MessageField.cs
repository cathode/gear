/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/

namespace Gear.Net
{
    /// <summary>
    /// Represents a serialized field of a <see cref="Message"/>.
    /// </summary>
    public abstract class MessageField
    {
        #region Properties
        /// <summary>
        /// Gets the id of the message field.
        /// </summary>
        public abstract MessageFieldId Id
        {
            get;
        }

        public short Tag
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the size in bytes required to serialize the <see cref="MessageField"/> in it's current state.
        /// </summary>
        public abstract short Size
        {
            get;
        }
        #endregion
        #region Methods
        public abstract int CopyTo(byte[] buffer, int startIndex);
        public abstract int CopyFrom(byte[] buffer, int startIndex, int count);
        #endregion
    }
}
