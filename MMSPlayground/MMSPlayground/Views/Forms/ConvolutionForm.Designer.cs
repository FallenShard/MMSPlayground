namespace MMSPlayground.Views.Forms
{
    partial class ConvolutionForm
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
            this.originalPictureBox = new System.Windows.Forms.PictureBox();
            this.lowPictureBox = new System.Windows.Forms.PictureBox();
            this.medPictureBox = new System.Windows.Forms.PictureBox();
            this.highPictureBox = new System.Windows.Forms.PictureBox();
            this.imagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.medPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.highPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imagePanel
            // 
            this.imagePanel.Controls.Add(this.highPictureBox);
            this.imagePanel.Controls.Add(this.medPictureBox);
            this.imagePanel.Controls.Add(this.lowPictureBox);
            this.imagePanel.Controls.Add(this.originalPictureBox);
            this.imagePanel.Location = new System.Drawing.Point(13, 13);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(522, 383);
            this.imagePanel.TabIndex = 0;
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
            // lowPictureBox
            // 
            this.lowPictureBox.Location = new System.Drawing.Point(242, 4);
            this.lowPictureBox.Name = "lowPictureBox";
            this.lowPictureBox.Size = new System.Drawing.Size(277, 149);
            this.lowPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.lowPictureBox.TabIndex = 1;
            this.lowPictureBox.TabStop = false;
            // 
            // medPictureBox
            // 
            this.medPictureBox.Location = new System.Drawing.Point(4, 186);
            this.medPictureBox.Name = "medPictureBox";
            this.medPictureBox.Size = new System.Drawing.Size(218, 164);
            this.medPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.medPictureBox.TabIndex = 2;
            this.medPictureBox.TabStop = false;
            // 
            // highPictureBox
            // 
            this.highPictureBox.Location = new System.Drawing.Point(242, 186);
            this.highPictureBox.Name = "highPictureBox";
            this.highPictureBox.Size = new System.Drawing.Size(268, 164);
            this.highPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.highPictureBox.TabIndex = 3;
            this.highPictureBox.TabStop = false;
            // 
            // ConvolutionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 408);
            this.Controls.Add(this.imagePanel);
            this.Name = "ConvolutionForm";
            this.Text = "Convolution Comparison";
            this.Resize += new System.EventHandler(this.ConvolutionForm_Resize);
            this.imagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.medPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.highPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.PictureBox highPictureBox;
        private System.Windows.Forms.PictureBox medPictureBox;
        private System.Windows.Forms.PictureBox lowPictureBox;
        private System.Windows.Forms.PictureBox originalPictureBox;
    }
}