using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Game
{
    public interface IBaseItem
    {
       string Name
        {
            get;
        }

        ItemFlags Flags
        {
            get;
        }

        ItemKind Kind
        {
            get;
        }

        int Cost
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether the consumer of the base item is required to deep clone.
        /// </summary>
        bool ForceClone
        {
            get;
        }
    }
}
