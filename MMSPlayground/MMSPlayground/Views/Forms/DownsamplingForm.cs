using MMSPlayground.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMSPlayground.Views.Forms
{
    public partial class DownsamplingForm : Form
    {
        private int m_leftMargin = 5;
        private int m_topMargin = 31;
        private int m_rightMargin = 5;
        private int m_bottomMargin = 5;
        private int m_middleMargin = 5;
        private float m_cachedAspectRatio = 1.0f;
        private IResizeMode m_resizeMode = new PreserveAspectResize();

        private DownsamplingMode m_samplingMode = DownsamplingMode.Y;

        public DownsamplingForm()
        {
            InitializeComponent();
        }

        public void DisplayDownsampled(Bitmap[] bitmaps)
        {
            m_cachedAspectRatio = (float)bitmaps[0].Width / (float)bitmaps[0].Height;

            originalPictureBox.Image = bitmaps[0];
            yFullPictureBox.Image = bitmaps[1];
            cbFullPictureBox.Image = bitmaps[2];
            crFullPictureBox.Image = bitmaps[3];

            ApplyResize();
        }

        public DownsamplingMode GetSelectedMode()
        {
            return m_samplingMode;
        }

        private void DownsamplingForm_Resize(object sender, EventArgs e)
        {
            ApplyResize();
        }

        private void ApplyResize()
        {
            m_resizeMode.Resize(imagePanel, m_cachedAspectRatio, m_leftMargin, m_topMargin, m_rightMargin, m_bottomMargin);

            int adjWidth = (imagePanel.Size.Width - m_middleMargin) / 2;
            int adjHeight = (imagePanel.Size.Height - m_middleMargin) / 2;

            Size picBoxSize = new Size(adjWidth, adjHeight);

            originalPictureBox.Size = picBoxSize;
            yFullPictureBox.Size = picBoxSize;
            cbFullPictureBox.Size = picBoxSize;
            crFullPictureBox.Size = picBoxSize;

            originalPictureBox.Location = new Point(0, 0);
            yFullPictureBox.Location = new Point(adjWidth + m_middleMargin, 0);
            cbFullPictureBox.Location = new Point(0, adjHeight + m_middleMargin);
            crFullPictureBox.Location = new Point(adjWidth + m_middleMargin, adjHeight + m_middleMargin);
        }

        private void yFullPictureBox_Click(object sender, EventArgs e)
        {
            m_samplingMode = DownsamplingMode.Y;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cbFullPictureBox_Click(object sender, EventArgs e)
        {
            m_samplingMode = DownsamplingMode.Cb;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void crFullPictureBox_Click(object sender, EventArgs e)
        {
            m_samplingMode = DownsamplingMode.Cr;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
