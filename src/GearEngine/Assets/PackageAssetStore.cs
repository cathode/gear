using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Assets
{
    /// <summary>
    /// Represents an <see cref="AssetStore"/> implementation that uses the Gear Engine package format to store game asset data.
    /// </summary>
    public abstract class PackageAssetStore : AssetStore
    {
        public override IEnumerable<Guid> ListAssets(int category, int subcategory)
        {
            throw new NotImplementedException();
        }
    }
}
