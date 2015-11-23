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
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Gear.Model
{
    /// <summary>
    /// A game world.
    /// </summary>
    public class World
    {

        /// <summary>
        /// Gets or sets the unique id of the world.
        /// </summary>
        public Guid WorldId { get; set; }

        /// <summary>
        /// Gets or sets the name of the world.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        public WorldDimension GetDimension(string name)
        {
            throw new NotImplementedException();
        }
    }


    public class WorldDimension
    {
        public string Name
        {
            get;
            set;
        }

        public Chunk PrimaryChunk
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Represents the implementation of a given type of block.
    /// </summary>
    public class BlockDefinition
    {
        /// <summary>
        /// Gets or sets the numerical id of the block definition.
        /// </summary>
        public ushort TypeId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the block.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

    }
}