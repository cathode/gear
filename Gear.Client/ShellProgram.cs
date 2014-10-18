using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Client
{
    /// <summary>
    /// Wraps the <see cref="GearShell"/> execution engine when invoked from the client in 'shell-only' mode.
    /// </summary>
    public class ShellProgram
    {

        public static void Run()
        {
            Console.WriteLine("Starting shell.");

            Console.Write("> ");

            while (ShellProgram.HandleInput(Console.ReadLine()))
            {
                Console.Write("> ");
            }

            Console.WriteLine("Exiting shell.");
        }

        private static bool HandleInput(string line)
        {
            var cleaned = line.Trim();

            if (cleaned.Equals("exit", StringComparison.OrdinalIgnoreCase))
                return false;
            else
                return true;
        }
    }
}
