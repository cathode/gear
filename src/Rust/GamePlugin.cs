/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rust
{
    public sealed class GamePlugin
    {
        public string Name
        {
            get;
            set;
        }
        public string LauncherBannerResourcePath
        {
            get;
            set;
        }
        public string LauncherIconResourcePath
        {
            get;
            set;
        }
        public string LauncherDetailText
        {
            get;
            set;
        }
        public Version Version
        {
            get;
            set;
        }
       
    }
}
