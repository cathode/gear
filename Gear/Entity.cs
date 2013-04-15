/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Gear
{
    /// <summary>
    /// Represents an in-game entity.
    /// </summary>
    public sealed class Entity
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Entity.Id"/> field.
        /// </summary>
        private readonly Guid uniqueId;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="Entity"/> class.
        /// </summary>
        /// <remarks>
        /// The new current is given a newly created <see cref="Guid"/>.
        /// </remarks>
        public Entity()
        {
            this.uniqueId = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new current of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="uniqueId">The unique id of the new current.</param>
        public Entity(Guid uniqueId)
        {
            this.uniqueId = uniqueId;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the unique identifier of the current <see cref="Entity"/>.
        /// </summary>
        public Guid UniqueId
        {
            get
            {
                return this.uniqueId;
            }
        }
        #endregion
    }
}
