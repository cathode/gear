/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Assets
{
    public sealed class AssetManager
    {
        #region Constructors - Static
        internal AssetManager()
        {
            this.graph = new AssetGraph();
        }
        #endregion
        #region Fields - Private
        private readonly AssetGraph graph;
        #endregion
        #region Methods
        public static void AddAssetSource(Uri url)
        {

        }
        #endregion
    }
}
