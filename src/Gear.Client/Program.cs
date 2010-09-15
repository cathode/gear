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
using Gear.Net;

namespace Gear.Client
{
    public static class Program
    {
        internal static void Main(string[] args)
        {
            ClientConnection connection = new ClientConnection();
            connection.Connect(System.Net.IPAddress.Loopback);

            ClientEngine engine = new ClientEngine();
            connection.Attach(engine);
            engine.Run();

            Console.ReadKey();
        }
    }
}
