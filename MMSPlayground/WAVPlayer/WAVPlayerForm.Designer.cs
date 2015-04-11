namespace WAVPlayer
{
    partial class WAVPlayerForm
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
            this.openButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.applyOffsetButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(399, 12);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(399, 41);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // playButton
            // 
            this.playButton.Enabled = false;
            this.playButton.Location = new System.Drawing.Point(399, 208);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 2;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(13, 13);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(380, 218);
            this.mainPanel.TabIndex = 3;
            // 
            // applyOffsetButton
            // 
            this.applyOffsetButton.Enabled = false;
            this.applyOffsetButton.Location = new System.Drawing.Point(399, 70);
            this.applyOffsetButton.Name = "applyOffsetButton";
            this.applyOffsetButton.Size = new System.Drawing.Size(75, 23);
            this.applyOffsetButton.TabIndex = 4;
            this.applyOffsetButton.Text = "Apply Offset";
            this.applyOffsetButton.UseVisualStyleBackColor = true;
            this.applyOffsetButton.Click += new System.EventHandler(this.applyOffsetButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(400, 179);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 5;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // WAVPlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 243);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.applyOffsetButton);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.openButton);
            this.Name = "WAVPlayerForm";
            this.Text = "WAVPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WAVPlayerForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button applyOffsetButton;
        private System.Windows.Forms.Button stopButton;
    }
}

