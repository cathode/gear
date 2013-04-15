/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
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
using System.Threading.Tasks;
using System.Windows.Forms;
using Gear.Net;

namespace Gear.Client.UI
{
    public partial class GearLauncher : Form
    {
        public GearLauncher()
        {
            InitializeComponent();

            Log.BindOutput(new LogMessageHandler(this.DisplayLogMessage));
        }

        private void DisplayLogMessage(LogMessage message)
        {
            if (this.logDisplay.InvokeRequired)
                this.logDisplay.Invoke((MethodInvoker)(() => this.DisplayLogMessage(message)));
            else
                this.logDisplay.Items.Add(message.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LanServerScanner.BeginDiscovery(5);
        }
    }
}
