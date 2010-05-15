using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Assets
{
    public abstract class AssetField<T>
    {
        #region Fields
        private bool isModified;
        private T currentValue;
        private T previousValue;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the value of the current <see cref="AssetField"/>.
        /// </summary>
        public T Value
        {
            get
            {
                return this.currentValue;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetField"/> class.
        /// </summary>
        protected AssetField()
        {

        }
        #endregion
        #region Methods
        public abstract Delta GetDelta(T previous, T current);
        #endregion
    }
}
