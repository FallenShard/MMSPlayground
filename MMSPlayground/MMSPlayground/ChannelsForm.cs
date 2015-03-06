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

namespace MMSPlayground
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

        private IResizeStrategy m_resizeMode = new PreserveAspectResize();

        public ChannelsForm(ChannelsPresenter presenter)
        {
            InitializeComponent();

            m_presenter = presenter;
            m_presenter.SetChannelsView(this);
        }

        public void DisplayImages(Bitmap bitmap, Bitmap[] channels)
        {
            fullPictureBox.Image = bitmap;
            redPictureBox.Image = channels[0];
            greenPictureBox.Image = channels[1];
            bluePictureBox.Image = channels[2];

            m_cachedAspectRatio = (float)bitmap.Width / (float)bitmap.Height;
            //ResizeComponents(bitmap.Width, bitmap.Height);

            ApplyResize();
        }

        public void SetVisible(bool visible)
        {
            if (visible)
                Show();
            else
                Hide();
        }

        public void SetResizeMode(IResizeStrategy resizeMode)
        {
            m_resizeMode = resizeMode;

            ApplyResize();
        }

        private void ChannelsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                Hide();
                m_presenter.SetChannelsViewStatus(false);
            }
        }

        private void ApplyResize()
        {
            Console.WriteLine("Resizing Channels!");
            m_resizeMode.Resize(imagePanel, m_cachedAspectRatio, m_leftMargin, m_topMargin, m_rightMargin, m_bottomMargin);

            int adjWidth = (imagePanel.Size.Width - m_middleMargin) / 2;
            int adjHeight = (imagePanel.Size.Height - m_middleMargin) / 2;

            Size picBoxSize = new Size(adjWidth, adjHeight);

            fullPictureBox.Size = picBoxSize;
            redPictureBox.Size = picBoxSize;
            greenPictureBox.Size = picBoxSize;
            bluePictureBox.Size = picBoxSize;

            fullPictureBox.Location = new Point(0, 0);
            redPictureBox.Location = new Point(adjWidth + m_middleMargin, 0);
            greenPictureBox.Location = new Point(0, adjHeight + m_middleMargin);
            bluePictureBox.Location = new Point(adjWidth + m_middleMargin, adjHeight + m_middleMargin);
        }

        private void ChannelsForm_Resize(object sender, EventArgs e)
        {
            ApplyResize();
        }
    }
}
