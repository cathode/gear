/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2014 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gear.Client.UI;

namespace Gear.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                // Check if the client is being invoked in 'shell-only' mode.
                if (args.Any(e => e == "--shell"))
                {
                    ShellProgram.Run();
                }

            }

            //var launcher = new GearLauncher();
            //launcher.Shown += delegate { Log.Write("test"); };
            //Application.Run(launcher);

            ////var chunk = new Chunk();

            //Console.Write("Press any key...");
            //Console.Read();
        }
    }
}
