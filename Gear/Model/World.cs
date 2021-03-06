﻿/******************************************************************************
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
using System.Diagnostics.Contracts;
using Gear.Geometry;

namespace Gear.Model
{
    /// <summary>
    /// A game world.
    /// </summary>
    public abstract class World
    {
        protected World(int seed)
        {
        }

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

        public int Seed { get; set; }

        public IGenerator Generator { get; protected set; }

        /// <summary>
        /// Gets the approximate gravity vector at the specified location.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public abstract Vector3d GetGravityVector(Vector3d location);

        public abstract Chunk GenerateChunk(Vector3d location);

        public abstract double GetSurfaceElevation(Vector3d location);

        // public abstract double[] GetElevationMap();
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