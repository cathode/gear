/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Gear.Launcher
{
    public partial class LauncherForm : Form
    {
        public LauncherForm()
        {
            InitializeComponent();
        }

        private void launchClientButton_Click(object sender, EventArgs e)
        {
            Process.Start("Gear.Client.exe");
            Application.Exit();
        }

        private void launchServerButton_Click(object sender, EventArgs e)
        {
            Process.Start("Gear.Server.exe");
            Application.Exit();
        }

        private void launchEditorButton_Click(object sender, EventArgs e)
        {
            Process.Start("Gear.Editor.exe");
            Application.Exit();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void websiteButton_Click(object sender, EventArgs e)
        {

        }
    }
}
