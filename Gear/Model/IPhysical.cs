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
    public interface IPhysical
    {
        decimal MeltingPoint { get; }

        decimal BoilingPoint { get; }

        decimal Density { get; }

        /// <summary>
        /// Gets a value indicating the substance's ability to conduct thermal energy (heat). Measured in Watts per Meter Kelvin.
        /// </summary>
        decimal ThermalConductivity { get; }

        /// <summary>
        /// Gets a value indicating how strongly the substance opposes the flow of electric current.
        /// </summary>
        decimal ElectricialResistivity { get; }

        decimal ElasticModulus { get; }

        decimal ShearModulus { get; }

        decimal BulkModulus { get; }

        decimal BrinellHardness { get; }
    }
}
