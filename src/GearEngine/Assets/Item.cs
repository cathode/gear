using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Assets
{
    /// <summary>
    /// Represents a game item.
    /// </summary>
    public class Item : Asset
    {
        #region Fields
        private const int ItemClassId = 0x0433 << 16;
        private const ushort ItemNameFieldId = 0x0;
        private const ushort ItemFlagsFieldId = 0x1;
        private const ushort ItemKindFieldId = 0x2;
        private const ushort ItemCostFieldId = 0x3;
        private const ushort ItemIdentifierFieldId = 0x4;
        private readonly StringField name = new StringField(ItemClassId | ItemNameFieldId);
        private readonly Int32Field cost = new Int32Field(ItemClassId | ItemCostFieldId);
        
        #endregion
        #region Properties
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
        #endregion
    }
}
