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
using Gear.Client.Geometry;
using Gear.Client.SceneGraph;

namespace Gear.Client.Rendering
{
    /// <summary>
    /// Defines the essential functionality that a 3d renderer implementation should provide.
    /// </summary>
    public interface IRenderer
    {
        #region Properties
        /// <summary>
        /// Gets or sets a <see cref="Vector4f"/> describing a background color for rendered frames.
        /// </summary>
        Vector4f BackgroundColor
        {
            get;
            set;
        }
        /// <summary>
        /// Gets a value indicating whether the renderer is performing real-time frame rendering.
        /// </summary>
        bool IsRunning
        {
            get;
        }

        /// <summary>
        /// Gets or sets the <see cref="Scene"/>
        /// </summary>
        Scene Scene
        {
            get;
            set;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Prepares the renderer using the specified options.
        /// </summary>
        /// <param name="options"></param>
        void Initialize(RendererOptions options);

        /// <summary>
        /// Causes the renderer to render a frame.
        /// </summary>
        void RenderFrame();

        /// <summary>
        /// Causes the renderer to begin rendering frames for real-time operation.
        /// </summary>
        void Start();

        /// <summary>
        /// Causes the renderer to stop rendering real-time frames.
        /// </summary>
        void Stop();
        #endregion
    }
}
