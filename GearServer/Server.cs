// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearEngine;
using GearEngine.Winforms;
using System.Windows.Forms;
using System.Threading;

namespace GearServer
{
    /// <summary>
    /// Holds the GearServer entry point method.
    /// </summary>
    internal static class Server
    {
        #region Fields
        internal static ServerEngine Engine;
        internal static ServerSession Session;
        internal static GameShellForm Form;
        internal static GameShell Shell;
        #endregion
        #region Methods
        /// <summary>
        /// GearServer executable entry point.
        /// </summary>
        /// <param name="args"></param>
        internal static void Main(string[] args)
        {
            Server.Engine = new ServerEngine();

            Server.Session = new ServerSession();
            Server.Shell = new GameShell(Server.Engine.Input);
            Server.Form = new GameShellForm();
            Server.Form.Shell = Server.Shell;

            Application.EnableVisualStyles();
            Application.Run(Server.Form);
        }
        #endregion
    }
}
