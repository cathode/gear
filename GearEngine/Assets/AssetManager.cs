/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
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
