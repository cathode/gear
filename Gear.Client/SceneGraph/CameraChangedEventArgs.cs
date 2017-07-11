/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Client.SceneGraph
{
    public sealed class CameraChangedEventArgs : EventArgs
    {
        #region Constructors
        public CameraChangedEventArgs(Camera camera)
        {
            this.camera = camera;
        }
        #endregion
        #region Fields
        private Camera camera;
        #endregion
        #region Properties
        public Camera Camera
        {
            get
            {
                return this.camera;
            }
        }
        #endregion
    }
}
