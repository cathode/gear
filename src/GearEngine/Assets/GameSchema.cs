using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Assets
{
    public abstract class GameSchema
    {
        public abstract IEnumerable<int> ListCategories();
        public abstract IEnumerable<int> ListSubcategories(int category);
    }
}
