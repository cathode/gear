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
using Rust.Assets;
using System.IO;

namespace Rust
{
    public static class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            Program.CompileSamplePackages();
#endif
            // == Engine Setup ==

            // Built-in path for game plugins.
            if (!Directory.Exists("./Plugins/"))
                Directory.CreateDirectory("./Plugins/");
            GamePluginManager.RegisterGamePluginSearchPath("./Plugins/");

            // Built-in path for asset packages.
            if (!Directory.Exists("./Assets/"))
                Directory.CreateDirectory("./Assets/");
            PackageManager.RegisterPackageSearchPath("./Assets/");
            var pkgs = PackageManager.LoadAllPackages();

            //GamePluginManager.RegisterGamePluginSearchPath("./");

            //var ids = GamePluginManager.ScanForGamePlugins();

            //var launcherUI = new LauncherUI();
            //Application.EnableVisualStyles();
            //Application.Run(launcherUI);

            //int adapterOrdinal = SlimDX.DXGI.

            //Engine.Tick += new EventHandler(Engine_Tick);
            //Engine.Start();
            Console.WriteLine("Done...");
            Console.Read();
        }
#if DEBUG
        private static void CompileSamplePackages()
        {
            var pc = new PackageCompiler("./Assets/Samples/Sample.xml", "./Assets/Sample.rp");
            if (pc.Compile())
                Console.WriteLine("Sample compilation OK");
            else
                Console.WriteLine("Sample compilation FAIL");
        }
#endif
        
        static void Engine_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
