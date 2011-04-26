/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/

namespace Gear
{
    /// <summary>
    /// Represents a serializable field.
    /// </summary>
    public abstract class Field
    {
        #region Properties
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

        /// <summary>
        /// Gets the size in bytes required to serialize the <see cref="Field"/> in it's current state.
        /// </summary>
        public abstract short Size
        {
            get;
        }
        #endregion
        #region Methods
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
        #endregion
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
