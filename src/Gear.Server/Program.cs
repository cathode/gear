using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Gear.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Debug.WriteLine("Creating ServerEngine");
            ServerEngine engine = new ServerEngine();

            Debug.WriteLine("Starting ServerEngine");
            engine.Run();
        }
    }
}
