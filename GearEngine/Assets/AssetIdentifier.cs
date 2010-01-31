/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Assets
{
    /// <summary>
    /// Identifies an asset.
    /// </summary>
    public sealed class AssetIdentifier
    {
        #region Properties - Public
        public AssetKind Kind
        {
            get;
            set;
        }
        public Version Version
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        #endregion
    }
}
