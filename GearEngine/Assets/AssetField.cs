using System;
using System.Collections.Generic;

using System.Text;

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
        /// Initializes a new instance of the <see cref="AssetField"/> class.
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
            return field.Value;
        }
        #endregion
    }
}
