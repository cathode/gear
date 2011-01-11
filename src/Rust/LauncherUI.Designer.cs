namespace Rust
{
    partial class LauncherUI
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
            this.gameListBox = new System.Windows.Forms.ListBox();
            this.gameBannerPictureBox = new System.Windows.Forms.PictureBox();
            this.playButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gameBannerPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // gameListBox
            // 
            this.gameListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.gameListBox.FormattingEnabled = true;
            this.gameListBox.Location = new System.Drawing.Point(0, 0);
            this.gameListBox.Name = "gameListBox";
            this.gameListBox.Size = new System.Drawing.Size(240, 400);
            this.gameListBox.TabIndex = 0;
            // 
            // gameBannerPictureBox
            // 
            this.gameBannerPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.gameBannerPictureBox.InitialImage = null;
            this.gameBannerPictureBox.Location = new System.Drawing.Point(240, 0);
            this.gameBannerPictureBox.Name = "gameBannerPictureBox";
            this.gameBannerPictureBox.Size = new System.Drawing.Size(560, 120);
            this.gameBannerPictureBox.TabIndex = 1;
            this.gameBannerPictureBox.TabStop = false;
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(588, 340);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(200, 48);
            this.playButton.TabIndex = 2;
            this.playButton.Text = "Play Game";
            this.playButton.UseVisualStyleBackColor = true;
            // 
            // exitButton
            // 
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Location = new System.Drawing.Point(246, 340);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(120, 48);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(246, 126);
            this.descriptionBox.Multiline = true;
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(542, 208);
            this.descriptionBox.TabIndex = 4;
            this.descriptionBox.Text = "Description of Game Plugin";
            // 
            // LauncherUI
            // 
            this.AcceptButton = this.playButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.exitButton;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.gameBannerPictureBox);
            this.Controls.Add(this.gameListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LauncherUI";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LauncherUI";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.gameBannerPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox gameListBox;
        private System.Windows.Forms.PictureBox gameBannerPictureBox;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox descriptionBox;
    }
}