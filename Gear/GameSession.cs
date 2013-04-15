/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Gear
{
    /// <summary>
    /// Holds and maintains all information related to an active game session.
    /// </summary>
    public abstract class GameSession : IDisposable
    {
        #region Fields
        private bool isDisposed;
        #endregion

        #region Methods

        /// <summary>
        /// Performs disposal of the current <see cref="GameSession"/>.
        /// </summary>
        /// <param name="disposing"></param>
        protected abstract void Dispose(bool disposing);

        /// <summary>
        /// Disposes the current <see cref="GameSession"/>, releasing all managed and unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            this.isDisposed = true;
            GC.SuppressFinalize(this);
        }

        #endregion
        #region Properties

        /// <summary>
        /// Indicates if the current <see cref="GameSession"/> is disposed.
        /// </summary>
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
