/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Gear.Serialization
{
    /// <summary>
    /// Represents a serializable field.
    /// </summary>
    public abstract class PacketField
    {
        private readonly Dictionary<string, List<PacketField>> subFields = new Dictionary<string, List<PacketField>>();
        //private readonly <PacketField> subFields = new List<PacketField>();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
        /// </summary>
        protected PacketField()
        {
            this.Tag = 0;
            this.Name = null;
        }

        public PacketField this[string name, ushort index = 0]
        {
            get
            {
                return this.subFields[name][index];
            }
        }

        /// <summary>
        /// Gets the id of the message field.
        /// </summary>
        public abstract FieldKind Id
        {
            get;
        }

        public short Tag
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }



        /// <summary>
        /// Gets the size in bytes required to serialize the value assigned to the field.
        /// </summary>
        public abstract short ValueSize
        {
            get;
        }

        public abstract bool Assign(object value);

        public virtual bool Assign<T>(T value)
        {
            throw new NotImplementedException();
        }

        public virtual int CopyTo(byte[] buffer)
        {
            return this.CopyTo(buffer, 0);
        }
        public abstract int CopyTo(byte[] buffer, int startIndex);

        public virtual int CopyFrom(byte[] buffer)
        {
            return this.CopyFrom(buffer, 0, buffer.Length);
        }
        public abstract int CopyFrom(byte[] buffer, int startIndex, int count);


        public int GetTotalSize()
        {
            throw new NotImplementedException();
        }

        #region Types
        public abstract class FieldBase<T> : Field
        {
            #region Fields
            private T value;
            #endregion
            #region Constructors
            protected FieldBase()
            {
                this.value = default(T);
            }
            protected FieldBase(T value)
            {
                this.value = value;
            }
            #endregion
            #region Properties
            public T Value
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
        }
        #endregion
    }
}
