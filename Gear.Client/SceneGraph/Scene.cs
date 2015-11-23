/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Drawing;
using Gear.Client.Geometry;

namespace Gear.Client.SceneGraph
{
    /// <summary>
    /// Represents the highest level portion of the scene graph.
    /// </summary>
    public sealed class Scene
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Scene.Root"/> property.
        /// </summary>
        private Node root;

        private Vector4f ambientLight;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        public Scene()
        {
            this.root = new Node();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets a color used to control the global ambient lighting in the scene.
        /// </summary>
        public Vector4f AmbientLight
        {
            get
            {
                return this.ambientLight;
            }
            set
            {
                this.ambientLight = value;
            }
        }
   

        /// <summary>
        /// Gets the root node of the scene.
        /// </summary>
        public Node Root
        {
            get
            {
                return this.root;
            }
            set
            {
                this.root = value;
            }
        }
        #endregion
    }
}