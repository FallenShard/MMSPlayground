namespace MMSPlayground.Views.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelsForm));
            this.fullPictureBox = new System.Windows.Forms.PictureBox();
            this.redPictureBox = new System.Windows.Forms.PictureBox();
            this.greenPictureBox = new System.Windows.Forms.PictureBox();
            this.bluePictureBox = new System.Windows.Forms.PictureBox();
            this.imagePanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.histogram1 = new MMSPlayground.Views.Controls.Histogram();
            this.histogram2 = new MMSPlayground.Views.Controls.Histogram();
            this.histogram3 = new MMSPlayground.Views.Controls.Histogram();
            this.channelsMenuStrip = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preserveAspectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stretchToFitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelsViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.fullPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bluePictureBox)).BeginInit();
            this.imagePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.channelsMenuStrip.SuspendLayout();
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
            this.imagePanel.Location = new System.Drawing.Point(12, 40);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(490, 386);
            this.imagePanel.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.histogram3);
            this.panel1.Controls.Add(this.histogram2);
            this.panel1.Controls.Add(this.histogram1);
            this.panel1.Location = new System.Drawing.Point(508, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 386);
            this.panel1.TabIndex = 5;
            // 
            // histogram1
            // 
            this.histogram1.Data = ((System.Collections.Generic.IList<int>)(resources.GetObject("histogram1.Data")));
            this.histogram1.Location = new System.Drawing.Point(234, 14);
            this.histogram1.MaxValue = 256;
            this.histogram1.MinValue = 0;
            this.histogram1.Name = "histogram1";
            this.histogram1.Size = new System.Drawing.Size(212, 182);
            this.histogram1.TabIndex = 0;
            // 
            // histogram2
            // 
            this.histogram2.Data = ((System.Collections.Generic.IList<int>)(resources.GetObject("histogram2.Data")));
            this.histogram2.Location = new System.Drawing.Point(13, 222);
            this.histogram2.MaxValue = 256;
            this.histogram2.MinValue = 0;
            this.histogram2.Name = "histogram2";
            this.histogram2.Size = new System.Drawing.Size(212, 145);
            this.histogram2.TabIndex = 1;
            // 
            // histogram3
            // 
            this.histogram3.Data = ((System.Collections.Generic.IList<int>)(resources.GetObject("histogram3.Data")));
            this.histogram3.Location = new System.Drawing.Point(234, 222);
            this.histogram3.MaxValue = 256;
            this.histogram3.MinValue = 0;
            this.histogram3.Name = "histogram3";
            this.histogram3.Size = new System.Drawing.Size(212, 145);
            this.histogram3.TabIndex = 2;
            // 
            // channelsMenuStrip
            // 
            this.channelsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.channelsMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.channelsMenuStrip.Name = "channelsMenuStrip";
            this.channelsMenuStrip.Size = new System.Drawing.Size(995, 24);
            this.channelsMenuStrip.TabIndex = 6;
            this.channelsMenuStrip.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resizeModeToolStripMenuItem,
            this.channelsViewToolStripMenuItem,
            this.histogramsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // resizeModeToolStripMenuItem
            // 
            this.resizeModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preserveAspectToolStripMenuItem,
            this.stretchToFitToolStripMenuItem});
            this.resizeModeToolStripMenuItem.Name = "resizeModeToolStripMenuItem";
            this.resizeModeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.resizeModeToolStripMenuItem.Text = "Resize Mode";
            // 
            // preserveAspectToolStripMenuItem
            // 
            this.preserveAspectToolStripMenuItem.Name = "preserveAspectToolStripMenuItem";
            this.preserveAspectToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.preserveAspectToolStripMenuItem.Text = "Preserve Aspect";
            this.preserveAspectToolStripMenuItem.Click += new System.EventHandler(this.preserveAspectToolStripMenuItem_Click);
            // 
            // stretchToFitToolStripMenuItem
            // 
            this.stretchToFitToolStripMenuItem.Name = "stretchToFitToolStripMenuItem";
            this.stretchToFitToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.stretchToFitToolStripMenuItem.Text = "Stretch To Fit";
            this.stretchToFitToolStripMenuItem.Click += new System.EventHandler(this.stretchToFitToolStripMenuItem_Click);
            // 
            // channelsViewToolStripMenuItem
            // 
            this.channelsViewToolStripMenuItem.Name = "channelsViewToolStripMenuItem";
            this.channelsViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.channelsViewToolStripMenuItem.Text = "Channels";
            // 
            // histogramsToolStripMenuItem
            // 
            this.histogramsToolStripMenuItem.Name = "histogramsToolStripMenuItem";
            this.histogramsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.histogramsToolStripMenuItem.Text = "Histograms";
            // 
            // ChannelsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 598);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.imagePanel);
            this.Controls.Add(this.channelsMenuStrip);
            this.MainMenuStrip = this.channelsMenuStrip;
            this.Name = "ChannelsForm";
            this.Text = "Channels View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChannelsForm_FormClosing);
            this.Resize += new System.EventHandler(this.ChannelsForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.fullPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bluePictureBox)).EndInit();
            this.imagePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.channelsMenuStrip.ResumeLayout(false);
            this.channelsMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox fullPictureBox;
        private System.Windows.Forms.PictureBox redPictureBox;
        private System.Windows.Forms.PictureBox greenPictureBox;
        private System.Windows.Forms.PictureBox bluePictureBox;
        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.Panel panel1;
        private Controls.Histogram histogram3;
        private Controls.Histogram histogram2;
        private Controls.Histogram histogram1;
        private System.Windows.Forms.MenuStrip channelsMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preserveAspectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stretchToFitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channelsViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramsToolStripMenuItem;
    }
}