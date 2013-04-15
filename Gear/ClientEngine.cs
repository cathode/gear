/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Gear
{
    /// <summary>
    /// Provides client-specific game engine functionality.
    /// </summary>
    public sealed class ClientEngine : EngineBase
    {
        #region Fields
        #endregion
        #region Constructors
        public ClientEngine()
        {

        }
        #endregion
        #region Methods
        public override void Run()
        {
            if (!this.IsInitialized)
                this.Initialize();

        }
        protected override void OnInitializing(EventArgs e)
        {
          
        }

        #endregion
    }
}
