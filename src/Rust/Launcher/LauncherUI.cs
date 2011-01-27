using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rust.Assets;

namespace Rust
{
    public partial class LauncherUI : Form
    {
        public LauncherUI()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            Package pkg = Package.Create("test.package");
        }
    }
}
