﻿/************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/   *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.   *
 * -------------------------------------------------------------------- *
 * Contributors:                                                        *
 * - Will 'cathode' Shelley <cathode@live.com>                          *
 ************************************************************************/
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
        /// Backing field for the <see cref="Entity.UniqueId"/> field.
        /// </summary>
        private readonly Guid uniqueId;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <remarks>
        /// The new instance is given a newly created <see cref="Guid"/>.
        /// </remarks>
        public Entity()
        {
            this.uniqueId = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="uniqueId"></param>
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
