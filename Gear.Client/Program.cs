/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
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

            //var launcher = new GearLauncher();
            //launcher.Shown += delegate { Log.Write("test"); };
            //Application.Run(launcher);

            var chunk = new Chunk();

            Console.Write("Press any key...");
            Console.Read();
        }
    }
}
