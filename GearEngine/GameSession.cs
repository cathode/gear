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

        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            if (!this.IsDisposed)
                this.Dispose(true);

            this.isDisposed = true;
        }

        #endregion
        #region Properties
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
