namespace MMSPlayground.Views.Controls
{
    partial class Histogram
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.histogramBox = new System.Windows.Forms.PictureBox();
            this.mainLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.histogramBox)).BeginInit();
            this.SuspendLayout();
            // 
            // histogramBox
            // 
            this.histogramBox.Location = new System.Drawing.Point(31, 49);
            this.histogramBox.Name = "histogramBox";
            this.histogramBox.Size = new System.Drawing.Size(209, 132);
            this.histogramBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.histogramBox.TabIndex = 0;
            this.histogramBox.TabStop = false;
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.Location = new System.Drawing.Point(231, 200);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(55, 13);
            this.mainLabel.TabIndex = 1;
            this.mainLabel.Text = "mainLabel";
            // 
            // Histogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainLabel);
            this.Controls.Add(this.histogramBox);
            this.Name = "Histogram";
            this.Size = new System.Drawing.Size(300, 225);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Histogram_Paint);
            this.Resize += new System.EventHandler(this.Histogram_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.histogramBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox histogramBox;
        private System.Windows.Forms.Label mainLabel;

    }
}
