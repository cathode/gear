/******************************************************************************
 * Gear.Client: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Gear.Client.Geometry
{
    public interface Vector4 : Gear.Client.Geometry.Vector3
    {
        #region Properties
        double W
        {
            get;
        }
        #endregion
        #region Methods
        Vector4 ToVector4();
        #endregion
    }
}
