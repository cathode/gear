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
using System.Drawing;
using Gear.Geometry;

namespace Gear.Client.SceneGraph
{
    public abstract class LightNode : Node
    {
        public Vector4d Diffuse
        {
            get;
            set;
        }
        public Vector4d Specular
        {
            get;
            set;
        }
    }
}
