namespace Gear.Launcher
{
    partial class LauncherForm
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
            this.closeButton = new System.Windows.Forms.Button();
            this.launchClientButton = new System.Windows.Forms.Button();
            this.launchServerButton = new System.Windows.Forms.Button();
            this.launchEditorButton = new System.Windows.Forms.Button();
            this.websiteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(188, 428);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(120, 40);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // launchClientButton
            // 
            this.launchClientButton.Location = new System.Drawing.Point(40, 140);
            this.launchClientButton.Name = "launchClientButton";
            this.launchClientButton.Size = new System.Drawing.Size(240, 60);
            this.launchClientButton.TabIndex = 1;
            this.launchClientButton.Text = "Launch Client";
            this.launchClientButton.UseVisualStyleBackColor = true;
            this.launchClientButton.Click += new System.EventHandler(this.launchClientButton_Click);
            // 
            // launchServerButton
            // 
            this.launchServerButton.Location = new System.Drawing.Point(40, 220);
            this.launchServerButton.Name = "launchServerButton";
            this.launchServerButton.Size = new System.Drawing.Size(240, 60);
            this.launchServerButton.TabIndex = 2;
            this.launchServerButton.Text = "Launch Server";
            this.launchServerButton.UseVisualStyleBackColor = true;
            this.launchServerButton.Click += new System.EventHandler(this.launchServerButton_Click);
            // 
            // launchEditorButton
            // 
            this.launchEditorButton.Location = new System.Drawing.Point(40, 300);
            this.launchEditorButton.Name = "launchEditorButton";
            this.launchEditorButton.Size = new System.Drawing.Size(240, 60);
            this.launchEditorButton.TabIndex = 3;
            this.launchEditorButton.Text = "Launch Editor";
            this.launchEditorButton.UseVisualStyleBackColor = true;
            this.launchEditorButton.Click += new System.EventHandler(this.launchEditorButton_Click);
            // 
            // websiteButton
            // 
            this.websiteButton.Location = new System.Drawing.Point(12, 428);
            this.websiteButton.Name = "websiteButton";
            this.websiteButton.Size = new System.Drawing.Size(120, 40);
            this.websiteButton.TabIndex = 4;
            this.websiteButton.Text = "Website";
            this.websiteButton.UseVisualStyleBackColor = true;
            this.websiteButton.Click += new System.EventHandler(this.websiteButton_Click);
            // 
            // LauncherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Gear.Launcher.Properties.Resources.LauncherBackground;
            this.ClientSize = new System.Drawing.Size(320, 480);
            this.Controls.Add(this.websiteButton);
            this.Controls.Add(this.launchEditorButton);
            this.Controls.Add(this.launchServerButton);
            this.Controls.Add(this.launchClientButton);
            this.Controls.Add(this.closeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(320, 480);
            this.MinimumSize = new System.Drawing.Size(320, 480);
            this.Name = "LauncherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GearEngineLauncher";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button launchClientButton;
        private System.Windows.Forms.Button launchServerButton;
        private System.Windows.Forms.Button launchEditorButton;
        private System.Windows.Forms.Button websiteButton;
    }
}