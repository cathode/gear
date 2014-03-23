﻿/******************************************************************************
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
using System.IO;

namespace Gear.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
          
            // Log to console.
            Log.BindOutput(Console.OpenStandardOutput());

            if (true)
                Log.BindOutput(File.Open("Gear.Server.log", FileMode.Append, FileAccess.Write, FileShare.None));

            Log.Write("Message log initialized", "system", LogMessageGroup.Info);

            var engine = new ServerEngine();
            engine.Run();
        }
    }
}