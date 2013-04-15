/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;

namespace Gear.Net
{
    /// <summary>
    /// Represents a message sent over the network from one endpoint to another.
    /// </summary>
    public abstract class Message
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Message.Fields"/> property.
        /// </summary>
        private Field[] fields;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="Message"/> class.
        /// </summary>
        protected Message()
        {
            this.fields = new Field[0];
        }

        /// <summary>
        /// Initializes a new current of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="fields">The fields of the new <see cref="Message"/>.</param>
        protected Message(Field[] fields)
        {
            // The 'fields' member should never be null.
            this.fields = fields ?? new Field[0];
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the <see cref="MessageId"/> of the current <see cref="Message"/>.
        /// </summary>
        public abstract MessageId Id
        {
            get;
        }

        /// <summary>
        /// Gets or sets the fields in the current <see cref="Message"/>.
        /// </summary>
        public Field[] Fields
        {
            get
            {
                return this.fields;
            }
            protected set
            {
                this.fields = value ?? new Field[0]; 
            }
        }
        #endregion
        #region Methods
        public abstract Field GetField(FieldKind id, short tag);
        #endregion
    }
}
