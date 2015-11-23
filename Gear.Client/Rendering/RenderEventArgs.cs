/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Drawing;

namespace Gear.Client.Rendering
{
    /// <summary>
    /// Represents event data for a frame render event.
    /// </summary>
    public class RenderEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderEventArgs"/> class.
        /// </summary>
        public RenderEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderEventArgs"/> class.
        /// </summary>
        /// <param name="options">A set of options associated with the rendering operation.</param>
        public RenderEventArgs(FrameOptions options)
        {
            this.Options = options;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets a set of options associated with the rendering operation.
        /// </summary>
        public FrameOptions Options
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="IManagedRendererTarget"/> that is the output for the current rendering operation.
        /// </summary>
        public IManagedRendererTarget Target
        {
            get;
            set;
        }
        #endregion
    }
}
