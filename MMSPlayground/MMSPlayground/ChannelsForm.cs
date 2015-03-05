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

        public ChannelsForm(ChannelsPresenter presenter)
        {
            InitializeComponent();

            m_presenter = presenter;
            m_presenter.SetChannelsView(this);
        }

        private void ResizeComponents(int width, int height)
        {
            fullPictureBox.Size  = new Size(width / 2, height / 2);
            redPictureBox.Size   = new Size(width / 2, height / 2);
            greenPictureBox.Size = new Size(width / 2, height / 2);
            bluePictureBox.Size  = new Size(width / 2, height / 2);

            this.ClientSize = new Size(width + 15, height + 15);

            fullPictureBox.Location  = new Point(5, 5);
            redPictureBox.Location   = new Point(10 + width / 2, 5);
            greenPictureBox.Location = new Point(5, 10 + height / 2);
            bluePictureBox.Location  = new Point(10 + width / 2, 10 + height / 2);
        }

        public void DisplayImages(Bitmap bitmap, Bitmap[] channels)
        {
            fullPictureBox.Image = bitmap;
            redPictureBox.Image = channels[0];
            greenPictureBox.Image = channels[1];
            bluePictureBox.Image = channels[2];

            ResizeComponents(bitmap.Width, bitmap.Height);
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
                m_presenter.SetChannelsViewStatus(false);
            }
        }
    }
}
