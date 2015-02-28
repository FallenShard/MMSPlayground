namespace MMSPlayground
{
    partial class ChannelForm
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
            ((System.ComponentModel.ISupportInitialize)(this.fullPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bluePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // fullPictureBox
            // 
            this.fullPictureBox.Location = new System.Drawing.Point(13, 13);
            this.fullPictureBox.Name = "fullPictureBox";
            this.fullPictureBox.Size = new System.Drawing.Size(256, 256);
            this.fullPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fullPictureBox.TabIndex = 0;
            this.fullPictureBox.TabStop = false;
            // 
            // redPictureBox
            // 
            this.redPictureBox.Location = new System.Drawing.Point(275, 12);
            this.redPictureBox.Name = "redPictureBox";
            this.redPictureBox.Size = new System.Drawing.Size(256, 256);
            this.redPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.redPictureBox.TabIndex = 1;
            this.redPictureBox.TabStop = false;
            // 
            // greenPictureBox
            // 
            this.greenPictureBox.Location = new System.Drawing.Point(13, 275);
            this.greenPictureBox.Name = "greenPictureBox";
            this.greenPictureBox.Size = new System.Drawing.Size(256, 256);
            this.greenPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.greenPictureBox.TabIndex = 2;
            this.greenPictureBox.TabStop = false;
            // 
            // bluePictureBox
            // 
            this.bluePictureBox.Location = new System.Drawing.Point(275, 275);
            this.bluePictureBox.Name = "bluePictureBox";
            this.bluePictureBox.Size = new System.Drawing.Size(256, 256);
            this.bluePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bluePictureBox.TabIndex = 3;
            this.bluePictureBox.TabStop = false;
            // 
            // ChannelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 541);
            this.Controls.Add(this.bluePictureBox);
            this.Controls.Add(this.greenPictureBox);
            this.Controls.Add(this.redPictureBox);
            this.Controls.Add(this.fullPictureBox);
            this.Name = "ChannelForm";
            this.Text = "ChannelForm";
            ((System.ComponentModel.ISupportInitialize)(this.fullPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bluePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox fullPictureBox;
        private System.Windows.Forms.PictureBox redPictureBox;
        private System.Windows.Forms.PictureBox greenPictureBox;
        private System.Windows.Forms.PictureBox bluePictureBox;
    }
}