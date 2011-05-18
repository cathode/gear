/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.IO;
using System.Linq;
using Gear.Assets;

namespace Gear
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Log to console.
            Log.BindOutput(Console.OpenStandardOutput());

            if (true)
                Log.BindOutput(File.Open("Gear.log", FileMode.Append, FileAccess.Write, FileShare.None));

            Log.Write("Message log initialized", "system", LogMessageGroup.Info);
            
            if (args.Contains("-dedicated") || args.Contains("-d"))
                Program.DedicatedServerMain(args);
            else
                Program.ClientMain(args);
            
        }

        private static void DedicatedServerMain(string[] args)
        {
            var engine = new ServerEngine();
            engine.Run();
        }

        private static void ClientMain(string[] args)
        {
            // Built-in path for asset packages.
            if (!Directory.Exists("./Assets/"))
                Directory.CreateDirectory("./Assets/");
            PackageManager.RegisterPackageSearchPath("./Assets/");
            var pkgs = PackageManager.LoadAllPackages();

            //ClientConnection conn = new ClientConnection();
            //conn.Connect(System.Net.IPAddress.Loopback);

            var clientEngine = new ClientEngine();
            clientEngine.Initialize();
            //clientEngine.Shell.Parse("set sv_name Demo Server");
            clientEngine.Run();
            

            Console.WriteLine("Press any key to quit...");
            Console.Read();

        }
    }
}
