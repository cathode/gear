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
        //public static Dictionary<string, object> RegisteredCommands;

        public static void Run()
        {
            Console.WriteLine("Starting shell.");

            Console.Write("> ");
            var prog = new ShellProgram();

            while (prog.HandleInput(Console.ReadLine()))
            {
                Console.Write("> ");
            }

            Console.WriteLine("Exiting shell.");
        }

        public void Write(string output)
        {
            Console.Write(output);
        }

        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }

        private bool HandleInput(string line)
        {
            var cleaned = line.Trim();

            if (cleaned.Equals("exit", StringComparison.OrdinalIgnoreCase))
                return true;

            var spi = cleaned.IndexOf(' ');
            if (spi == -1)
            {
                var command = cleaned;

                switch (command)
                {
                    case "exit":
                    case "quit":
                        return true;

                    case "discover":
                        return this.CommandDiscover(line);

                    case "list":
                        return this.CommandList(line);
                }
            }

            return false;
        }

        private bool CommandDiscover(string line)
        {
            var locator = new Services.ServiceLocator();

            locator.Run();

            return true;
        }

        private bool CommandList(string line)
        {
            return true;
        }
    }
}
