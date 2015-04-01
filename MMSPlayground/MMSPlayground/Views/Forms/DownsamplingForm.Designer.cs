namespace MMSPlayground.Views.Forms
{
    partial class DownsamplingForm
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
            this.imagePanel = new System.Windows.Forms.Panel();
            this.crFullPictureBox = new System.Windows.Forms.PictureBox();
            this.cbFullPictureBox = new System.Windows.Forms.PictureBox();
            this.yFullPictureBox = new System.Windows.Forms.PictureBox();
            this.originalPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crFullPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbFullPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yFullPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imagePanel
            // 
            this.imagePanel.Controls.Add(this.crFullPictureBox);
            this.imagePanel.Controls.Add(this.cbFullPictureBox);
            this.imagePanel.Controls.Add(this.yFullPictureBox);
            this.imagePanel.Controls.Add(this.originalPictureBox);
            this.imagePanel.Location = new System.Drawing.Point(116, 45);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(522, 383);
            this.imagePanel.TabIndex = 1;
            // 
            // crFullPictureBox
            // 
            this.crFullPictureBox.Location = new System.Drawing.Point(242, 186);
            this.crFullPictureBox.Name = "crFullPictureBox";
            this.crFullPictureBox.Size = new System.Drawing.Size(268, 164);
            this.crFullPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.crFullPictureBox.TabIndex = 3;
            this.crFullPictureBox.TabStop = false;
            this.crFullPictureBox.Click += new System.EventHandler(this.crFullPictureBox_Click);
            // 
            // cbFullPictureBox
            // 
            this.cbFullPictureBox.Location = new System.Drawing.Point(4, 186);
            this.cbFullPictureBox.Name = "cbFullPictureBox";
            this.cbFullPictureBox.Size = new System.Drawing.Size(218, 164);
            this.cbFullPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cbFullPictureBox.TabIndex = 2;
            this.cbFullPictureBox.TabStop = false;
            this.cbFullPictureBox.Click += new System.EventHandler(this.cbFullPictureBox_Click);
            // 
            // yFullPictureBox
            // 
            this.yFullPictureBox.Location = new System.Drawing.Point(242, 4);
            this.yFullPictureBox.Name = "yFullPictureBox";
            this.yFullPictureBox.Size = new System.Drawing.Size(277, 149);
            this.yFullPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.yFullPictureBox.TabIndex = 1;
            this.yFullPictureBox.TabStop = false;
            this.yFullPictureBox.Click += new System.EventHandler(this.yFullPictureBox_Click);
            // 
            // originalPictureBox
            // 
            this.originalPictureBox.Location = new System.Drawing.Point(4, 4);
            this.originalPictureBox.Name = "originalPictureBox";
            this.originalPictureBox.Size = new System.Drawing.Size(218, 149);
            this.originalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.originalPictureBox.TabIndex = 0;
            this.originalPictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please select a downsampling method to use:";
            // 
            // DownsamplingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 473);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imagePanel);
            this.Name = "DownsamplingForm";
            this.Text = "Downsampling Comparison";
            this.Resize += new System.EventHandler(this.DownsamplingForm_Resize);
            this.imagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.crFullPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbFullPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yFullPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.PictureBox crFullPictureBox;
        private System.Windows.Forms.PictureBox cbFullPictureBox;
        private System.Windows.Forms.PictureBox yFullPictureBox;
        private System.Windows.Forms.PictureBox originalPictureBox;
        private System.Windows.Forms.Label label1;
    }
}