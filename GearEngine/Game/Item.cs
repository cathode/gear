using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Game
{
    /// <summary>
    /// Represents a game item.
    /// </summary>
    public class Item
    {
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
    }
}
