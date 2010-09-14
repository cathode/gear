/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
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
            Console.WriteLine("Creating ServerEngine");
            ServerEngine engine = new ServerEngine();
            engine.Listener.ConnectionAccepted += new EventHandler<Net.ConnectionEventArgs>(Listener_ConnectionAccepted);
            Console.WriteLine("Starting ServerEngine");
            engine.Run();
        }

        static void Listener_ConnectionAccepted(object sender, Net.ConnectionEventArgs e)
        {
            Console.WriteLine("Connection Accepted");
        }
    }
}
