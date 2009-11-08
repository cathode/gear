// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearEngine
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
