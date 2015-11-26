/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Gear.Geometry
{
    public interface Vector4 : Gear.Geometry.Vector3
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
