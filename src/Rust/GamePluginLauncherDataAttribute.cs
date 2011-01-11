using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rust
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
