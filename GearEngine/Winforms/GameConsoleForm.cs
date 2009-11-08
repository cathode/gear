// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GearEngine.Winforms
{
    /// <summary>
    /// A form that provides a user interface for a <see cref="GameConsole"/> instance.
    /// </summary>
    public partial class GameConsoleForm : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameConsoleForm"/> class.
        /// </summary>
        public GameConsoleForm()
        {
            InitializeComponent();

            this.Console = null;
        }
        #endregion
        #region Fields
        private GameConsole console;
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
                    this.Console.Parse(commandText);
                }
                catch (GameConsoleParseException ex)
                {
                    //TODO: handle parse exception
                }
            }
        }

        /// <summary>
        /// Forces the current <see cref="GameConsoleForm"/> to reflect it's current state.
        /// </summary>
        private void Reset()
        {
            this.input.Clear();
            this.input.ClearUndo();
            this.output.Nodes.Clear();

            this.input.Enabled = (this.Console != null);
            this.output.Enabled = (this.Console != null);

            this.Invalidate(true);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the active <see cref="GameConsole"/>.
        /// </summary>
        public GameConsole Console
        {
            get
            {
                return this.console;
            }
            set
            {
                this.console = value;

                this.Reset();
            }
        }
        #endregion

      
    }
}
