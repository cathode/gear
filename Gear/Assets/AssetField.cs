/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Diagnostics.Contracts;

namespace Gear.Assets
{
    public abstract class AssetField<T>
    {
        #region Fields
        private bool isModified;
        private T value;
        private readonly uint id;
        #endregion
        #region Properties
        public bool IsModified
        {
            get
            {
                return this.isModified;
            }
            internal set
            {
                this.isModified = value;
            }
        }

        /// <summary>
        /// Gets or sets the value of the current <see cref="AssetField"/>.
        /// </summary>
        public T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.isModified = true;
                this.value = value;
            }
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="AssetField"/> class.
        /// </summary>
        protected AssetField(uint id)
        {
            this.id = id;
        }
        #endregion
        #region Methods
        #endregion
        #region Operators
        [Obsolete]
        public static implicit operator T(AssetField<T> field)
        {
            Contract.Requires(field != null);

            return field.Value;
        }
        #endregion
    }
}
