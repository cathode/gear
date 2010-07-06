using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Assets
{
    /// <summary>
    /// Represents a source that game assets are loaded from.
    /// </summary>
    public abstract class AssetStore
    {
        #region Properties
        /// <summary>
        /// Gets a <see cref="GameSchema"/> that defines the assets used by the game.
        /// </summary>
        public abstract GameSchema Schema
        {
            get;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Lists the unique IDs of all assets maintained by the current <see cref="AssetStore"/>.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Guid> ListAssets()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Lists the unique IDs of all assets of the specified category that are maintained by the current <see cref="AssetStore"/>.
        /// </summary>
        /// <param name="category">The category of assets to list.</param>
        /// <returns></returns>
        public virtual IEnumerable<Guid> ListAssets(int category)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Lists the unique IDs of all assets of the specified category and subcategory combination that are maintained by the current <see cref="AssetStore"/>.
        /// </summary>
        /// <param name="category">The category of assets to list.</param>
        /// <param name="subcategory">The category-specific subcategory of assets to list.</param>
        /// <returns></returns>
        public abstract IEnumerable<Guid> ListAssets(int category, int subcategory);
        #endregion
    }
}
