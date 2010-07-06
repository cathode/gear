// Gear - Copyright © 2009-2010 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Gear.Winforms
{
    /// <summary>
    /// A form that provides a user interface for a <see cref="GameShell"/> instance.
    /// </summary>
    public partial class GameShellForm : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameShellForm"/> class.
        /// </summary>
        public GameShellForm()
        {
            InitializeComponent();
            this.Text = EngineResources.ShellUITitle;
            this.Shell = null;
        }
        #endregion
        #region Fields
        private GameShell shell;
        private StringBuilder outputBuilder = new StringBuilder();
        #endregion
        #region Methods - Private

        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                string commandText = this.input.Text;
                this.input.Text = string.Empty;
                try
                {
                    this.Shell.Parse(commandText);
                }
                catch (GameShellParseException ex)
                {
                    string line = ex.Message + "\r\n";

                    this.outputBuilder.AppendLine(line);
                }
                System.Threading.Thread.Sleep(50);
                this.output.AppendText(this.outputBuilder.ToString());
                this.outputBuilder.Length = 0;
                this.outputBuilder.Capacity = 0;
            }
        }

        /// <summary>
        /// Forces the current <see cref="GameShellForm"/> to reflect it's current state.
        /// </summary>
        private void Reset()
        {
            this.input.Clear();
            this.input.ClearUndo();
            this.output.Clear();

            this.input.Enabled = (this.Shell != null);
            this.output.Enabled = (this.Shell != null);

            this.Invalidate(true);

            if (this.Shell != null)
                this.Shell.Output = new StringWriter(this.outputBuilder);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the active <see cref="GameShell"/>.
        /// </summary>
        public GameShell Shell
        {
            get
            {
                return this.shell;
            }
            set
            {
                this.shell = value;

                this.Reset();
            }
        }
        #endregion
        #region Types

        #endregion
    }
}
