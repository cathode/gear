/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gear.Assets;
using Gear.Net;
using Gear.Winforms;

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
                Log.BindOutput(File.Open("Gear.log", FileMode.Append, FileAccess.Write, FileShare.None));

            Log.Write("Message log initialized", "system", LogMessageGroup.Info);

            if (args.Contains("-dedicated") || args.Contains("-d"))
                Program.DedicatedServerMain(args);
            else
                Program.ClientMain(args);

        }

        private static void DedicatedServerMain(string[] args)
        {
            ConnectionListener listener = new ConnectionListener();
            listener.ConnectionAccepted += new EventHandler<ConnectionEventArgs>(listener_ConnectionAccepted);
            listener.Start();

            //while (listener.IsListening)
                System.Threading.Thread.Sleep(1000);
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
            clientEngine.Shell.Parse("set sv_name Demo Server");
            //clientEngine.Run();
            var form = new GameShellForm()
            {
                Shell = clientEngine.Shell
            };
            Application.Run(form);

            Console.WriteLine("Done...");
            Console.Read();
            
        }

        static void listener_ConnectionAccepted(object sender, ConnectionEventArgs e)
        {

        }
#if DEBUG
        private static void CompileSamplePackages()
        {
            var pc = new PackageCompiler();
            if (pc.Compile("./Assets/Samples/Sample.xml", "./Assets/Sample.gp"))
                Console.WriteLine("Sample compilation OK");
            else
                Console.WriteLine("Sample compilation FAIL");
        }
#endif

        static void Engine_Tick(object sender, EventArgs e)
        {
            //Log.Write(string.Format("Tick #{0}", Program.ticks++), "program", LogMessageGroup.Debug);
        }
    }
}
