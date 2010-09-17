using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Assets
{
    /// <summary>
    /// Represents a game asset.
    /// </summary>
    public abstract class Asset : IDisposable
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Asset.UniqueId"/> property.
        /// </summary>
        private readonly Guid uniqueId;

        /// <summary>
        /// Backing field for the <see cref="Asset.IsDisposed"/> property.
        /// </summary>
        private bool isDisposed;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="Asset"/> class.
        /// </summary>
        protected Asset()
        {
            this.uniqueId = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new current of the <see cref="Asset"/> class.
        /// </summary>
        /// <param name="uniqueId"></param>
        protected Asset(Guid uniqueId)
        {
            this.uniqueId = uniqueId;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the unique identifier of the current <see cref="Asset"/>.
        /// </summary>
        public Guid UniqueId
        {
            get
            {
                return this.uniqueId;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="Asset"/> is disposed.
        /// </summary>
        public bool IsDisposed
        {
            get
            {
                return this.isDisposed;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Releases unmanaged resources used by the current <see cref="Asset"/>.
        /// </summary>
        public void Dispose()
        {
            if (this.IsDisposed)
                return;

            this.isDisposed = true;

            this.Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged resources used by the current <see cref="Asset"/>.
        /// </summary>
        /// <param name="disposing"></param>
        /// <returns></returns>
        protected virtual bool Dispose(bool disposing)
        {
            return true;
        }
        #endregion
    }
}
