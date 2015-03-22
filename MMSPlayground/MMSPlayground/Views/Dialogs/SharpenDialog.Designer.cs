namespace MMSPlayground.Views.Dialogs
{
    partial class SharpenDialog
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
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.radioButton3x3 = new System.Windows.Forms.RadioButton();
            this.radioButton5x5 = new System.Windows.Forms.RadioButton();
            this.radioButton7x7 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(12, 35);
            this.numericUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(185, 20);
            this.numericUpDown.TabIndex = 4;
            this.numericUpDown.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Base Factor (-20 to 20)";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(122, 160);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(12, 160);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 5;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // radioButton3x3
            // 
            this.radioButton3x3.AutoSize = true;
            this.radioButton3x3.Checked = true;
            this.radioButton3x3.Location = new System.Drawing.Point(12, 78);
            this.radioButton3x3.Name = "radioButton3x3";
            this.radioButton3x3.Size = new System.Drawing.Size(48, 17);
            this.radioButton3x3.TabIndex = 8;
            this.radioButton3x3.TabStop = true;
            this.radioButton3x3.Text = "3 x 3";
            this.radioButton3x3.UseVisualStyleBackColor = true;
            // 
            // radioButton5x5
            // 
            this.radioButton5x5.AutoSize = true;
            this.radioButton5x5.Location = new System.Drawing.Point(12, 101);
            this.radioButton5x5.Name = "radioButton5x5";
            this.radioButton5x5.Size = new System.Drawing.Size(48, 17);
            this.radioButton5x5.TabIndex = 9;
            this.radioButton5x5.TabStop = true;
            this.radioButton5x5.Text = "5 x 5";
            this.radioButton5x5.UseVisualStyleBackColor = true;
            // 
            // radioButton7x7
            // 
            this.radioButton7x7.AutoSize = true;
            this.radioButton7x7.Location = new System.Drawing.Point(12, 124);
            this.radioButton7x7.Name = "radioButton7x7";
            this.radioButton7x7.Size = new System.Drawing.Size(48, 17);
            this.radioButton7x7.TabIndex = 10;
            this.radioButton7x7.TabStop = true;
            this.radioButton7x7.Text = "7 x 7";
            this.radioButton7x7.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Kernel Size";
            // 
            // SharpenDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(209, 195);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton7x7);
            this.Controls.Add(this.radioButton5x5);
            this.Controls.Add(this.radioButton3x3);
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.applyButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SharpenDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sharpen";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.RadioButton radioButton3x3;
        private System.Windows.Forms.RadioButton radioButton5x5;
        private System.Windows.Forms.RadioButton radioButton7x7;
        private System.Windows.Forms.Label label2;

    }
}