/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Assets
{
    /// <summary>
    /// Represents a game asset.
    /// </summary>
    public abstract class Asset : IDisposable
    {
        #region Constructors - Protected
        /// <summary>
        /// Initializes a new instance of the <see cref="Gear.Assets.Asset"/> class with a newly generated UniqueId.
        /// </summary>
        protected Asset()
        {
            this.uniqueId = Guid.NewGuid();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Gear.Assets.Asset"/> class with the specified UniqueId.
        /// </summary>
        /// <param name="uniqueId"></param>
        protected Asset(Guid uniqueId)
        {
            this.uniqueId = uniqueId;
        }
        #endregion
        #region Fields
        private bool isDisposed;
        private readonly Guid uniqueId;
        #endregion
        #region Methods - Protected
        protected virtual bool Dispose(bool disposing)
        {
            return true;
        }
        #endregion
        #region Methods - Public

        public void Dispose()
        {
            if (this.IsDisposed)
                return;

            this.isDisposed = true;


        }

        #endregion
        #region Properties - Public
        public bool IsDisposed
        {
            get
            {
                return this.isDisposed;
            }
        }
        #endregion
    }
}
