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
            this.yPictureBox = new System.Windows.Forms.PictureBox();
            this.cbPictureBox = new System.Windows.Forms.PictureBox();
            this.crPictureBox = new System.Windows.Forms.PictureBox();
            this.imagePanel = new System.Windows.Forms.Panel();
            this.yHistogram = new MMSPlayground.Views.Controls.HistogramBox();
            this.cbHistogram = new MMSPlayground.Views.Controls.HistogramBox();
            this.crHistogram = new MMSPlayground.Views.Controls.HistogramBox();
            this.channelsMenuStrip = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preserveAspectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stretchToFitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelsViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.fullPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.crPictureBox)).BeginInit();
            this.imagePanel.SuspendLayout();
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
            // yPictureBox
            // 
            this.yPictureBox.Location = new System.Drawing.Point(234, 14);
            this.yPictureBox.Name = "yPictureBox";
            this.yPictureBox.Size = new System.Drawing.Size(188, 182);
            this.yPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.yPictureBox.TabIndex = 1;
            this.yPictureBox.TabStop = false;
            // 
            // cbPictureBox
            // 
            this.cbPictureBox.Location = new System.Drawing.Point(32, 222);
            this.cbPictureBox.Name = "cbPictureBox";
            this.cbPictureBox.Size = new System.Drawing.Size(163, 133);
            this.cbPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cbPictureBox.TabIndex = 2;
            this.cbPictureBox.TabStop = false;
            // 
            // crPictureBox
            // 
            this.crPictureBox.Location = new System.Drawing.Point(234, 222);
            this.crPictureBox.Name = "crPictureBox";
            this.crPictureBox.Size = new System.Drawing.Size(188, 145);
            this.crPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.crPictureBox.TabIndex = 3;
            this.crPictureBox.TabStop = false;
            // 
            // imagePanel
            // 
            this.imagePanel.Controls.Add(this.cbHistogram);
            this.imagePanel.Controls.Add(this.crHistogram);
            this.imagePanel.Controls.Add(this.fullPictureBox);
            this.imagePanel.Controls.Add(this.crPictureBox);
            this.imagePanel.Controls.Add(this.yHistogram);
            this.imagePanel.Controls.Add(this.yPictureBox);
            this.imagePanel.Controls.Add(this.cbPictureBox);
            this.imagePanel.Location = new System.Drawing.Point(12, 40);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(490, 386);
            this.imagePanel.TabIndex = 4;
            // 
            // yHistogram
            // 
            this.yHistogram.Location = new System.Drawing.Point(275, 14);
            this.yHistogram.Name = "yHistogram";
            this.yHistogram.Size = new System.Drawing.Size(212, 182);
            this.yHistogram.TabIndex = 0;
            this.yHistogram.Visible = false;
            // 
            // cbHistogram
            // 
            this.cbHistogram.Location = new System.Drawing.Point(57, 222);
            this.cbHistogram.Name = "cbHistogram";
            this.cbHistogram.Size = new System.Drawing.Size(212, 145);
            this.cbHistogram.TabIndex = 1;
            this.cbHistogram.Visible = false;
            // 
            // crHistogram
            // 
            this.crHistogram.Location = new System.Drawing.Point(295, 222);
            this.crHistogram.Name = "crHistogram";
            this.crHistogram.Size = new System.Drawing.Size(212, 145);
            this.crHistogram.TabIndex = 2;
            this.crHistogram.Visible = false;
            // 
            // channelsMenuStrip
            // 
            this.channelsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.channelsMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.channelsMenuStrip.Name = "channelsMenuStrip";
            this.channelsMenuStrip.Size = new System.Drawing.Size(559, 24);
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
            this.channelsViewToolStripMenuItem.Click += new System.EventHandler(this.channelsViewToolStripMenuItem_Click);
            // 
            // histogramsToolStripMenuItem
            // 
            this.histogramsToolStripMenuItem.Name = "histogramsToolStripMenuItem";
            this.histogramsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.histogramsToolStripMenuItem.Text = "Histograms";
            this.histogramsToolStripMenuItem.Click += new System.EventHandler(this.histogramsToolStripMenuItem_Click);
            // 
            // ChannelsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 469);
            this.Controls.Add(this.imagePanel);
            this.Controls.Add(this.channelsMenuStrip);
            this.MainMenuStrip = this.channelsMenuStrip;
            this.Name = "ChannelsForm";
            this.Text = "Channels View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChannelsForm_FormClosing);
            this.Resize += new System.EventHandler(this.ChannelsForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.fullPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.crPictureBox)).EndInit();
            this.imagePanel.ResumeLayout(false);
            this.channelsMenuStrip.ResumeLayout(false);
            this.channelsMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox fullPictureBox;
        private System.Windows.Forms.PictureBox yPictureBox;
        private System.Windows.Forms.PictureBox cbPictureBox;
        private System.Windows.Forms.PictureBox crPictureBox;
        private System.Windows.Forms.Panel imagePanel;
        private Controls.HistogramBox crHistogram;
        private Controls.HistogramBox cbHistogram;
        private Controls.HistogramBox yHistogram;
        private System.Windows.Forms.MenuStrip channelsMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preserveAspectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stretchToFitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channelsViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramsToolStripMenuItem;
    }
}