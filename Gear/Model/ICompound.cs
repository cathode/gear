using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Model
{
    /// <summary>
    /// Represents the characteristics of a chemical compound.
    /// </summary>
    public interface ICompound : IPhysical
    {
        string CASRegistryNumber { get; }

        public Dictionary<IElement, int> Elements { get; }
    }
}
