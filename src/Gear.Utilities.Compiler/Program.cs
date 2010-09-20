/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Gear.Assets;

namespace Gear.Utilities.Compiler
{
    class Program
    {
        internal static void Main(string[] args)
        {
            if (args.Length != 1)
                Console.WriteLine("Invalid number of arguments.\r\nUsage: Gear.Utilities.PackageCompiler.exe <path-to-package-xml-file>");
            else
            {
                string path = Path.GetFullPath(args[0]);
                bool result = false;
                var compiler = new PackageCompiler(path);
                try
                {
                    result = compiler.Compile();
                }
                finally
                {
                    if (result)
                        Console.WriteLine("Compiled package definition from '{0}'", path);
                    else
                        Console.WriteLine("Failed to compile package definition from '{0}'", path);
                }
            }
        }
    }
}
