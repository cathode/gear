/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
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
