namespace GearEngine.Winforms
{
    partial class GameShellForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Panel inputOrganizer;
            this.input = new System.Windows.Forms.TextBox();
            this.shellTargetLabel = new System.Windows.Forms.Label();
            this.output = new System.Windows.Forms.TextBox();
            inputOrganizer = new System.Windows.Forms.Panel();
            inputOrganizer.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputOrganizer
            // 
            inputOrganizer.Controls.Add(this.input);
            inputOrganizer.Controls.Add(this.shellTargetLabel);
            inputOrganizer.Dock = System.Windows.Forms.DockStyle.Bottom;
            inputOrganizer.Location = new System.Drawing.Point(0, 322);
            inputOrganizer.Name = "inputOrganizer";
            inputOrganizer.Size = new System.Drawing.Size(752, 24);
            inputOrganizer.TabIndex = 3;
            // 
            // input
            // 
            this.input.AcceptsReturn = true;
            this.input.AcceptsTab = true;
            this.input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.input.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.input.Location = new System.Drawing.Point(64, 0);
            this.input.Multiline = true;
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(688, 24);
            this.input.TabIndex = 0;
            this.input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.input_KeyDown);
            // 
            // shellTargetLabel
            // 
            this.shellTargetLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.shellTargetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shellTargetLabel.Location = new System.Drawing.Point(0, 0);
            this.shellTargetLabel.Name = "shellTargetLabel";
            this.shellTargetLabel.Size = new System.Drawing.Size(64, 24);
            this.shellTargetLabel.TabIndex = 2;
            this.shellTargetLabel.Text = "Shell:";
            this.shellTargetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // output
            // 
            this.output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output.Location = new System.Drawing.Point(0, 0);
            this.output.MaxLength = 0;
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(752, 322);
            this.output.TabIndex = 1;
            // 
            // GameShellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 346);
            this.ControlBox = false;
            this.Controls.Add(this.output);
            this.Controls.Add(inputOrganizer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameShellForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EngineResources.ShellUITitle";
            inputOrganizer.ResumeLayout(false);
            inputOrganizer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox input;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.Label shellTargetLabel;
    }
}