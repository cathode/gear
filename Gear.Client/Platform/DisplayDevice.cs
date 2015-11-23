﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Gear.Client.Platform
{
    /// <summary>
    /// Represents a pixel-based display device, such as a monitor, television, or projector.
    /// </summary>
    public abstract class DisplayDevice
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayDevice"/> class.
        /// </summary>
        protected DisplayDevice()
        {
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets a <see cref="DisplayProfile"/> that represents the native display settings for
        /// the current <see cref="DisplayDevice"/>.
        /// </summary>
        public abstract DisplayProfile NativeProfile
        {
            get;
        }

        /// <summary>
        /// Gets a <see cref="DisplayProfile"/> that represents the best viewing experience which
        /// is supported by the current <see cref="DisplayDevice"/>. It is assumed that the optimal
        /// display profile is the native profile, as is the case with LCD screens.
        /// </summary>
        public virtual DisplayProfile OptimalProfile
        {
            get
            {
                return this.NativeProfile;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// When overridden in a derived class, queries the system or display hardware for a list of
        /// supported full-screen display profile configurations.
        /// </summary>
        /// <returns>A list of supported full-screen display profiles supported by the current <see cref="DisplayDevice"/>.</returns>
        public abstract DisplayProfile[] GetProfiles();
        #endregion
    }
}
