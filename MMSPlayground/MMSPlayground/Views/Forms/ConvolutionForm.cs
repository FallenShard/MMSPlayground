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
    public partial class ConvolutionForm : Form
    {
        private int m_leftMargin = 5;
        private int m_topMargin = 5;
        private int m_rightMargin = 5;
        private int m_bottomMargin = 5;
        private int m_middleMargin = 5;
        private float m_cachedAspectRatio = 1.0f;
        private IResizeMode m_resizeMode = new PreserveAspectResize();

        public ConvolutionForm()
        {
            InitializeComponent();
        }

        public void DisplayConvolutionResults(Bitmap[] bitmaps)
        {
            m_cachedAspectRatio = (float)bitmaps[0].Width / (float)bitmaps[0].Height;

            originalPictureBox.Image = bitmaps[0];
            lowPictureBox.Image      = bitmaps[1];
            medPictureBox.Image      = bitmaps[2];
            highPictureBox.Image     = bitmaps[3];

            ApplyResize();
        }

        private void ConvolutionForm_Resize(object sender, EventArgs e)
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
            lowPictureBox.Size      = picBoxSize;
            medPictureBox.Size      = picBoxSize;
            highPictureBox.Size     = picBoxSize;

            originalPictureBox.Location = new Point(0, 0);
            lowPictureBox.Location = new Point(adjWidth + m_middleMargin, 0);
            medPictureBox.Location = new Point(0, adjHeight + m_middleMargin);
            highPictureBox.Location = new Point(adjWidth + m_middleMargin, adjHeight + m_middleMargin);
        }
    }
}
