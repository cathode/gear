/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Assets
{
    public static class AssetManager
    {
        #region Constructors - Static
        static AssetManager()
        {
            AssetManager.graph = new AssetGraph();
        }
        #endregion
        #region Fields - Private
        private static AssetGraph graph;
        #endregion
        #region Methods
        public static void AddAssetSource(Uri url)
        {

        }
        #endregion
    }
}
