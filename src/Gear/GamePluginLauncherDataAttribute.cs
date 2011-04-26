/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear
{
    public sealed class GamePluginLauncherDataAttribute : Attribute
    {
        public GamePluginLauncherDataAttribute()
        {
        }
        public string BannerPath
        {
            get;
            set;
        }
        public string BackgroundPath
        {
            get;
            set;
        }
    }
}
