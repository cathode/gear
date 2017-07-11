/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Model
{
    /// <summary>
    /// Represents the characteristics of an element, something that compounds are made from.
    /// </summary>
    public interface IElement : IPhysical
    {
        /// <summary>
        /// Gets the name of the element.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the designated atomic number of the element.
        /// </summary>
        ushort AtomicNumber { get; }

        /// <summary>
        /// Gets the relative atomic mass of the element.
        /// </summary>
        decimal StandardAtomicWeight { get; }

        ushort ProtonCount { get; }

        ushort NeutronCount { get; }

        ushort ElectronCount { get; }

        /// <summary>
        /// Gets the elements half-life (in seconds). Values of 0 or less indicate the element is stable and does not decay.
        /// </summary>
        decimal HalfLife { get; }
    }
}
