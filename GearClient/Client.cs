/* Copyright © 2009 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gear;
using Gear.Winforms;

namespace Gear.Client
{
    internal static class Client
    {
        #region Fields
        internal static ClientEngine Engine;
        internal static GameShellForm ShellUI;
        #endregion
        #region Methods
        /// <summary>
        /// GearClient application entry point.
        /// </summary>
        /// <param name="args"></param>
        internal static void Main(string[] args)
        {
            // Initialize client engine
            Client.Engine = new ClientEngine();
            Client.ShellUI = new GameShellForm()
            {
                Shell = Client.Engine.Shell
            };
        }
        #endregion
    }
}
