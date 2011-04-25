/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rust.Assets
{
    public sealed class Field<T> where T : IFieldValue, new()
    {
        #region Fields
        private T value;
        private bool isModified;
        private Field<T> parent;
        #endregion
        #region Constructors
        public Field()
        {
            this.value = default(T);
            this.isModified = false;
            this.parent = null;
        }
        #endregion
        #region Properties
        public bool IsModified
        {
            get
            {
                return this.isModified;
            }
        }
        /// <summary>
        /// Gets the value of the current field, or the value of the parent field if the current field is unmodified.
        /// </summary>
        public T Value
        {
            get
            {
                return (this.IsModified) ? this.value : (this.parent ?? this).value;
            }
            set
            {
                this.value = value;
                this.isModified = true;
            }
        }
        #endregion
        #region Methods
        public void Clear()
        {
            this.value = default(T);
            this.isModified = false;
        }
        #endregion
    }
}
