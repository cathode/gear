/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Rust
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Hardcoded engine set-up.
            //Engine.RegisterGamePluginSearchPath("./plugins/");
            Engine.RegisterGamePluginSearchPath("./");

            var ids = Engine.ScanForGamePlugins();

            var launcherUI = new LauncherUI();
            Application.EnableVisualStyles();
            Application.Run(launcherUI);
        }
    }
}
