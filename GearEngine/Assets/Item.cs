using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Assets
{
    /// <summary>
    /// Represents a game item.
    /// </summary>
    public class Item
    {
        #region Properties
        public string Path
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ItemFlags Flags
        {
            get;
            set;
        }

        public ItemKind Kind
        {
            get;
            set;
        }

        public int Cost
        {
            get;
            set;
        }
        public Uri Identifier
        {
            get;
            set;
        }
        #endregion
    }
}
