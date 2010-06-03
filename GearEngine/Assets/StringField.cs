using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Assets
{
    public sealed class StringField : AssetField<string>
    {
        public StringField(uint id)
            : base(id)
        {
        }
    }
}
