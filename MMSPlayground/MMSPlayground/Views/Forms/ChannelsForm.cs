using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MMSPlayground.Presenters;
using MMSPlayground.Views;

namespace MMSPlayground.Views.Forms
{
    public partial class ChannelsForm : Form, IChannelsView
    {
        private ChannelsPresenter m_presenter;

        private int m_leftMargin = 5;
        private int m_topMargin = 5;
        private int m_rightMargin = 5;
        private int m_bottomMargin = 5;
        private int m_middleMargin = 5;
        private float m_cachedAspectRatio = 1.0f;

        private ToolStripMenuItem m_activeResizeItem = null;
        private IResizeStrategy m_resizeMode = new PreserveAspectResize();

        private ToolStripMenuItem m_activeViewItem = null;
        private IList<Control> m_activeControls = new List<Control>();

        public ChannelsForm(ChannelsPresenter presenter)
        {
            InitializeComponent();

            m_presenter = presenter;
            m_presenter.SetChannelsView(this);

            m_topMargin += channelsMenuStrip.Size.Height;

            m_activeResizeItem = preserveAspectToolStripMenuItem;
            m_activeResizeItem.Checked = true;

            m_activeViewItem = channelsViewToolStripMenuItem;
            m_activeViewItem.Checked = true;
            m_activeControls.Add(yPictureBox);
            m_activeControls.Add(cbPictureBox);
            m_activeControls.Add(crPictureBox);
        }

        public void DisplayImages(Bitmap bitmap, Bitmap[] channels, IList<int>[] histograms)
        {
            fullPictureBox.Image = bitmap;
            yPictureBox.Image = channels[0];
            cbPictureBox.Image = channels[1];
            crPictureBox.Image = channels[2];

            yHistogram.Data = histograms[0];
            cbHistogram.Data = histograms[1];
            crHistogram.Data = histograms[2];

            m_cachedAspectRatio = (float)bitmap.Width / (float)bitmap.Height;

            ApplyResize();
        }

        public void SetVisible(bool visible)
        {
            if (visible)
                Show();
            else
                Hide();
        }

        private void ChannelsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                Hide();
                m_presenter.SendChannelsViewStatus(false);
            }
        }

        private void ApplyResize()
        {
            m_resizeMode.Resize(imagePanel, m_cachedAspectRatio, m_leftMargin, m_topMargin, m_rightMargin, m_bottomMargin);

            int adjWidth = (imagePanel.Size.Width - m_middleMargin) / 2;
            int adjHeight = (imagePanel.Size.Height - m_middleMargin) / 2;

            Size picBoxSize = new Size(adjWidth, adjHeight);

            fullPictureBox.Size = picBoxSize;
            m_activeControls[0].Size = picBoxSize;
            m_activeControls[1].Size = picBoxSize;
            m_activeControls[2].Size = picBoxSize;                

            fullPictureBox.Location = new Point(0, 0);
            m_activeControls[0].Location = new Point(adjWidth + m_middleMargin, 0);
            m_activeControls[1].Location = new Point(0, adjHeight + m_middleMargin);
            m_activeControls[2].Location = new Point(adjWidth + m_middleMargin, adjHeight + m_middleMargin);
        }

        private void ChannelsForm_Resize(object sender, EventArgs e)
        {
            ApplyResize();
        }

        private void preserveAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_activeResizeItem != preserveAspectToolStripMenuItem)
                SetResizeMode(preserveAspectToolStripMenuItem, new PreserveAspectResize());
        }

        private void stretchToFitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_activeResizeItem != stretchToFitToolStripMenuItem)
                SetResizeMode(stretchToFitToolStripMenuItem, new StretchResize());
        }

        private void SetResizeMode(ToolStripMenuItem item, IResizeStrategy resizeMode)
        {
            m_resizeMode = resizeMode;
            m_activeResizeItem.Checked = false;
            m_activeResizeItem = item;
            m_activeResizeItem.Checked = true;

            ApplyResize();
        }

        private void channelsViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_activeViewItem != channelsViewToolStripMenuItem)
            {
                IList<Control> activeCtrls = new List<Control>();
                activeCtrls.Add(yPictureBox);
                activeCtrls.Add(cbPictureBox);
                activeCtrls.Add(crPictureBox);

                SetActiveView(channelsViewToolStripMenuItem, activeCtrls);
            }
        }

        private void histogramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_activeViewItem != histogramsToolStripMenuItem)
            {
                IList<Control> activeCtrls = new List<Control>();
                activeCtrls.Add(yHistogram);
                activeCtrls.Add(cbHistogram);
                activeCtrls.Add(crHistogram);

                SetActiveView(histogramsToolStripMenuItem, activeCtrls);
            }
        }

        private void SetActiveView(ToolStripMenuItem activeItem, IList<Control> activeControls)
        {
            m_activeViewItem.Checked = false;
            m_activeViewItem = activeItem;
            m_activeViewItem.Checked = true;

            foreach (Control ctrl in m_activeControls)
                ctrl.Visible = false;

            m_activeControls = activeControls;

            foreach (Control ctrl in m_activeControls)
                ctrl.Visible = true;

            ApplyResize();
        }
    }
}
