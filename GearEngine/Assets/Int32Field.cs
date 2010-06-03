using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Assets
{
    public sealed class Int32Field : AssetField<Int32>
    {
        public Int32Field(uint fieldId) : base(fieldId)
        {
        }
    }
}
