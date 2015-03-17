namespace MMSPlayground.Views.Forms
{
    partial class MainForm
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
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.brightnessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.sharpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeEnhancementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useWin32CoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeToOriginalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preserveAspectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stretchToFitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.imgMetaToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.imgToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dimMetaToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dimToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bppMetaToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bppToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.repeatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryCapacityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.mainMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.Location = new System.Drawing.Point(12, 27);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(512, 473);
            this.mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainPictureBox.TabIndex = 0;
            this.mainPictureBox.TabStop = false;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(541, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "mainMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.ToolTipText = "Provides a dialog to open an image in .BMP, .JPG or .PNG format.";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.ToolTipText = "Provides a dialog to save the current image in .BMP, .JPG or .PNG format.";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayChannelsToolStripMenuItem,
            this.toolStripSeparator2,
            this.brightnessToolStripMenuItem,
            this.contrastToolStripMenuItem,
            this.toolStripSeparator3,
            this.sharpenToolStripMenuItem,
            this.edgeEnhancementToolStripMenuItem,
            this.toolStripSeparator4,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.repeatToolStripMenuItem});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // displayChannelsToolStripMenuItem
            // 
            this.displayChannelsToolStripMenuItem.Enabled = false;
            this.displayChannelsToolStripMenuItem.Name = "displayChannelsToolStripMenuItem";
            this.displayChannelsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.displayChannelsToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.displayChannelsToolStripMenuItem.Text = "Show YCbCr Channels";
            this.displayChannelsToolStripMenuItem.Click += new System.EventHandler(this.displayChannelsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(261, 6);
            // 
            // brightnessToolStripMenuItem
            // 
            this.brightnessToolStripMenuItem.Enabled = false;
            this.brightnessToolStripMenuItem.Name = "brightnessToolStripMenuItem";
            this.brightnessToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.B)));
            this.brightnessToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.brightnessToolStripMenuItem.Text = "Brightness";
            this.brightnessToolStripMenuItem.Click += new System.EventHandler(this.brightnessToolStripMenuItem_Click);
            // 
            // contrastToolStripMenuItem
            // 
            this.contrastToolStripMenuItem.Enabled = false;
            this.contrastToolStripMenuItem.Name = "contrastToolStripMenuItem";
            this.contrastToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.contrastToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.contrastToolStripMenuItem.Text = "Contrast";
            this.contrastToolStripMenuItem.Click += new System.EventHandler(this.contrastToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(261, 6);
            // 
            // sharpenToolStripMenuItem
            // 
            this.sharpenToolStripMenuItem.Enabled = false;
            this.sharpenToolStripMenuItem.Name = "sharpenToolStripMenuItem";
            this.sharpenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.sharpenToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.sharpenToolStripMenuItem.Text = "Sharpen";
            this.sharpenToolStripMenuItem.Click += new System.EventHandler(this.sharpenToolStripMenuItem_Click);
            // 
            // edgeEnhancementToolStripMenuItem
            // 
            this.edgeEnhancementToolStripMenuItem.Enabled = false;
            this.edgeEnhancementToolStripMenuItem.Name = "edgeEnhancementToolStripMenuItem";
            this.edgeEnhancementToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.edgeEnhancementToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.edgeEnhancementToolStripMenuItem.Text = "Edge Enhancement";
            this.edgeEnhancementToolStripMenuItem.Click += new System.EventHandler(this.edgeEnhancementToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(261, 6);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Enabled = false;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.useWin32CoreToolStripMenuItem,
            this.resizeToOriginalToolStripMenuItem,
            this.resizeToolStripMenuItem,
            this.memoryCapacityToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // useWin32CoreToolStripMenuItem
            // 
            this.useWin32CoreToolStripMenuItem.Name = "useWin32CoreToolStripMenuItem";
            this.useWin32CoreToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.useWin32CoreToolStripMenuItem.Text = "Use Win32Core";
            this.useWin32CoreToolStripMenuItem.Click += new System.EventHandler(this.useWin32CoreToolStripMenuItem_Click);
            // 
            // resizeToOriginalToolStripMenuItem
            // 
            this.resizeToOriginalToolStripMenuItem.Enabled = false;
            this.resizeToOriginalToolStripMenuItem.Name = "resizeToOriginalToolStripMenuItem";
            this.resizeToOriginalToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.resizeToOriginalToolStripMenuItem.Text = "Resize To Original";
            this.resizeToOriginalToolStripMenuItem.Click += new System.EventHandler(this.resizeToOriginalToolStripMenuItem_Click);
            // 
            // resizeToolStripMenuItem
            // 
            this.resizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preserveAspectToolStripMenuItem,
            this.stretchToFitToolStripMenuItem});
            this.resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
            this.resizeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.resizeToolStripMenuItem.Text = "Resize Mode";
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
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imgMetaToolStripStatusLabel,
            this.imgToolStripStatusLabel,
            this.dimMetaToolStripStatusLabel,
            this.dimToolStripStatusLabel,
            this.bppMetaToolStripStatusLabel,
            this.bppToolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 521);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(541, 22);
            this.statusStrip.TabIndex = 2;
            // 
            // imgMetaToolStripStatusLabel
            // 
            this.imgMetaToolStripStatusLabel.Name = "imgMetaToolStripStatusLabel";
            this.imgMetaToolStripStatusLabel.Size = new System.Drawing.Size(46, 17);
            this.imgMetaToolStripStatusLabel.Text = "Image: ";
            // 
            // imgToolStripStatusLabel
            // 
            this.imgToolStripStatusLabel.Name = "imgToolStripStatusLabel";
            this.imgToolStripStatusLabel.Size = new System.Drawing.Size(101, 17);
            this.imgToolStripStatusLabel.Text = "No Image loaded.";
            // 
            // dimMetaToolStripStatusLabel
            // 
            this.dimMetaToolStripStatusLabel.Name = "dimMetaToolStripStatusLabel";
            this.dimMetaToolStripStatusLabel.Size = new System.Drawing.Size(72, 17);
            this.dimMetaToolStripStatusLabel.Text = "Dimensions:";
            // 
            // dimToolStripStatusLabel
            // 
            this.dimToolStripStatusLabel.Name = "dimToolStripStatusLabel";
            this.dimToolStripStatusLabel.Size = new System.Drawing.Size(29, 17);
            this.dimToolStripStatusLabel.Text = "N/A";
            // 
            // bppMetaToolStripStatusLabel
            // 
            this.bppMetaToolStripStatusLabel.Name = "bppMetaToolStripStatusLabel";
            this.bppMetaToolStripStatusLabel.Size = new System.Drawing.Size(31, 17);
            this.bppMetaToolStripStatusLabel.Text = "BPP:";
            // 
            // bppToolStripStatusLabel
            // 
            this.bppToolStripStatusLabel.Name = "bppToolStripStatusLabel";
            this.bppToolStripStatusLabel.Size = new System.Drawing.Size(29, 17);
            this.bppToolStripStatusLabel.Text = "N/A";
            // 
            // repeatToolStripMenuItem
            // 
            this.repeatToolStripMenuItem.Enabled = false;
            this.repeatToolStripMenuItem.Name = "repeatToolStripMenuItem";
            this.repeatToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.repeatToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.repeatToolStripMenuItem.Text = "Repeat";
            this.repeatToolStripMenuItem.Click += new System.EventHandler(this.repeatToolStripMenuItem_Click);
            // 
            // memoryCapacityToolStripMenuItem
            // 
            this.memoryCapacityToolStripMenuItem.Name = "memoryCapacityToolStripMenuItem";
            this.memoryCapacityToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.memoryCapacityToolStripMenuItem.Text = "Memory Capacity...";
            this.memoryCapacityToolStripMenuItem.Click += new System.EventHandler(this.memoryCapacityToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 543);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainPictureBox);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "MMS Playground";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightnessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contrastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useWin32CoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeToOriginalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preserveAspectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stretchToFitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel imgMetaToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel imgToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel dimMetaToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel dimToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel bppMetaToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel bppToolStripStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sharpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgeEnhancementToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem repeatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoryCapacityToolStripMenuItem;
    }
}

