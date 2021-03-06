﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
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
using GSCore;

namespace Gear.Client.UI
{
    public partial class GearLauncher : Form
    {
        public GearLauncher()
        {
            this.InitializeComponent();

            Log.BindOutput(new LogMessageHandler(this.DisplayLogMessage));
        }

        private void DisplayLogMessage(LogMessage message)
        {
            if (this.logDisplay.InvokeRequired)
            {
                this.logDisplay.Invoke((MethodInvoker)(() => this.DisplayLogMessage(message)));
            }
            else
            {
                this.logDisplay.Items.Add(message.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // LanServerScanner.BeginDiscovery(5);
        }
    }
}
