namespace MMSPlayground
{
    partial class ChannelsForm
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
            this.fullPictureBox = new System.Windows.Forms.PictureBox();
            this.redPictureBox = new System.Windows.Forms.PictureBox();
            this.greenPictureBox = new System.Windows.Forms.PictureBox();
            this.bluePictureBox = new System.Windows.Forms.PictureBox();
            this.imagePanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.fullPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bluePictureBox)).BeginInit();
            this.imagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // fullPictureBox
            // 
            this.fullPictureBox.Location = new System.Drawing.Point(32, 14);
            this.fullPictureBox.Name = "fullPictureBox";
            this.fullPictureBox.Size = new System.Drawing.Size(178, 182);
            this.fullPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fullPictureBox.TabIndex = 0;
            this.fullPictureBox.TabStop = false;
            // 
            // redPictureBox
            // 
            this.redPictureBox.Location = new System.Drawing.Point(234, 14);
            this.redPictureBox.Name = "redPictureBox";
            this.redPictureBox.Size = new System.Drawing.Size(188, 182);
            this.redPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.redPictureBox.TabIndex = 1;
            this.redPictureBox.TabStop = false;
            // 
            // greenPictureBox
            // 
            this.greenPictureBox.Location = new System.Drawing.Point(32, 222);
            this.greenPictureBox.Name = "greenPictureBox";
            this.greenPictureBox.Size = new System.Drawing.Size(163, 133);
            this.greenPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.greenPictureBox.TabIndex = 2;
            this.greenPictureBox.TabStop = false;
            // 
            // bluePictureBox
            // 
            this.bluePictureBox.Location = new System.Drawing.Point(234, 222);
            this.bluePictureBox.Name = "bluePictureBox";
            this.bluePictureBox.Size = new System.Drawing.Size(188, 145);
            this.bluePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bluePictureBox.TabIndex = 3;
            this.bluePictureBox.TabStop = false;
            // 
            // imagePanel
            // 
            this.imagePanel.Controls.Add(this.fullPictureBox);
            this.imagePanel.Controls.Add(this.bluePictureBox);
            this.imagePanel.Controls.Add(this.redPictureBox);
            this.imagePanel.Controls.Add(this.greenPictureBox);
            this.imagePanel.Location = new System.Drawing.Point(12, 12);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(490, 386);
            this.imagePanel.TabIndex = 4;
            // 
            // ChannelsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 584);
            this.Controls.Add(this.imagePanel);
            this.Name = "ChannelsForm";
            this.Text = "Channels View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChannelsForm_FormClosing);
            this.Resize += new System.EventHandler(this.ChannelsForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.fullPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bluePictureBox)).EndInit();
            this.imagePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox fullPictureBox;
        private System.Windows.Forms.PictureBox redPictureBox;
        private System.Windows.Forms.PictureBox greenPictureBox;
        private System.Windows.Forms.PictureBox bluePictureBox;
        private System.Windows.Forms.Panel imagePanel;
    }
}