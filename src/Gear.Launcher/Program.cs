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
using System.Windows.Forms;

namespace Gear.Launcher
{
    internal static class Program
    {
        #region Methods
        internal static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new LauncherForm());
        }
        #endregion
    }
}
