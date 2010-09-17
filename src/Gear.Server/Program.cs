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
using Gear.Net;

namespace Gear.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Creating ServerEngine");
            ServerEngine engine = new ServerEngine();
            engine.Listener.ConnectionAccepted += new EventHandler<ConnectionEventArgs>(Listener_ConnectionAccepted);
            Console.WriteLine("Starting ServerEngine");
#if DEBUG
            engine.PostLoad += new EventHandler(engine_PostLoad);
#endif
            engine.Run();
        }

        static void engine_PostLoad(object sender, EventArgs e)
        {
            ClientConnection connection = new ClientConnection();
            connection.Connect(System.Net.IPAddress.Loopback);
         
        }

        static void Listener_ConnectionAccepted(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine("Connection Accepted");
            e.Connection.MessageReceived += new EventHandler<MessageEventArgs>(Connection_MessageReceived);
        }

        static void Connection_MessageReceived(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Message Received");
        }
    }
}
