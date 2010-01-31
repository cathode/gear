/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Assets
{
    /// <summary>
    /// Represents supported kinds of assets.
    /// </summary>
    public enum AssetKind
    {
        /// <summary>
        /// Indicates an asset containing binary data.
        /// </summary>
        Binary = 0x0,
        /// <summary>
        /// Indicates an asset containing text data.
        /// </summary>
        Text,
        /// <summary>
        /// Indicates an asset containing numeric data.
        /// </summary>
        Numeric,
        /// <summary>
        /// Indicates an asset containing image data.
        /// </summary>
        Image,
        /// <summary>
        /// Indicates an asset containing video data.
        /// </summary>
        Video,
        /// <summary>
        /// Indicates an asset containing audio data.
        /// </summary>
        Audio,
        /// <summary>
        /// Indicates an asset containing model data.
        /// </summary>
        Model,
    }
}
