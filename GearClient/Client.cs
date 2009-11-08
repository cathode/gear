// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GearEngine;

namespace GearClient
{
    internal static class Client
    {
        #region Fields
        internal static GameShell GConsole;
        #endregion
        #region Methods
        /// <summary>
        /// GearClient application entry point.
        /// </summary>
        /// <param name="args"></param>
        internal static void Main(string[] args)
        {
            var cq = new CommandQueue();

            GConsole = new GameShell(cq);

            while (true)
            {

            }
        }
        #endregion
    }
}
