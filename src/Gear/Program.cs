/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Gear.Assets;
using System.IO;

namespace Gear
{
    public static class Program
    {
        static long ticks;
        public static void Main(string[] args)
        {
#if DEBUG
            Program.CompileSamplePackages();
#endif
            // == Engine Setup ==

            // Log to console.
            Log.BindOutput(Console.OpenStandardOutput());

            if (true)
                Log.BindOutput(File.Open("rust.log", FileMode.Append, FileAccess.Write, FileShare.None));

            Log.Write("Message log initialized", "system", LogMessageGroup.Info);

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

            Engine.TickRate = 4.0;
            Engine.Tick += new EventHandler(Engine_Tick);
            Engine.Start();
            Console.WriteLine("Done...");
            Console.Read();
        }
#if DEBUG
        private static void CompileSamplePackages()
        {
            var pc = new PackageCompiler();
            if (pc.Compile("./Assets/Samples/Sample.xml", "./Assets/Sample.rp"))
                Console.WriteLine("Sample compilation OK");
            else
                Console.WriteLine("Sample compilation FAIL");
        }
#endif

        static void Engine_Tick(object sender, EventArgs e)
        {
            Log.Write(string.Format("Tick #{0}", Program.ticks++), "program", LogMessageGroup.Debug);
        }
    }
}
