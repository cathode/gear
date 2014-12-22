﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2014 William 'cathode' Shelley. All Rights Reserved.      *
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
    public class Compound : ICompound
    {
        /// <summary>
        /// Gets or sets the internal reference ID of the compound (used by the engine).
        /// </summary>
        public Guid CompoundId { get; set; }

        public decimal MeltingPoint
        {
            get { throw new NotImplementedException(); }
        }

        public decimal BoilingPoint
        {
            get { throw new NotImplementedException(); }
        }

        public decimal Density
        {
            get { throw new NotImplementedException(); }
        }

        public Dictionary<IElement, int> Elements
        {
            get { throw new NotImplementedException(); }
        }
    }
}
