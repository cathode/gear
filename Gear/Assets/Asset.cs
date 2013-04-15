/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Assets
{
    /// <summary>
    /// Represents a game asset.
    /// </summary>
    public abstract class Asset : IDisposable, IFieldSerializable
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Asset.Id"/> property.
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

        public IEnumerable<Field> GetFields()
        {
            throw new NotImplementedException();
        }

        public Field GetField(FieldKind id, short tag)
        {
            throw new NotImplementedException();
        }
    }
}
