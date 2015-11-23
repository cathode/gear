/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Gear.Client.Modeling.Parametrics
{
    /// <summary>
    /// Represents an element of a parametric model.
    /// </summary>
    public class Entity
    {

        private List<Entity> children;

        /// <summary>
        /// Gets or sets a parent entity of the current entity.
        /// </summary>
        public Entity Parent { get; set; }


        public long Id { get; set; }


        public int GetLevel()
        {
            if (this.Parent != null)
                return this.Parent.GetLevel() + 1;

            return 0;
        }

        public bool AddChild(Entity value)
        {
            //sif (value.GetLevel() < this.GetLevel())

            throw new NotImplementedException();
        }
    }
}
