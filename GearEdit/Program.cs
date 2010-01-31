/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using System.Windows.Forms;

namespace Gear.Tools.Editor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EditorForm());
        }
    }
}
