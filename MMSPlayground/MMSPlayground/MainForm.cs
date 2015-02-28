using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MMSPlayground
{
    public partial class MainForm : Form
    {
        private ChannelForm m_channelsView = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void mainPictureBox_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Image";
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(dlg.FileName);
                SetImage(bitmap);
            }
            dlg.Dispose();
        }

        private void SetImage(Bitmap bitmap)
        {
            mainPictureBox.Image = bitmap;

            if (m_channelsView == null)
                m_channelsView = new ChannelForm();

            m_channelsView.SetImage(bitmap);
            m_channelsView.Show();

            ResizeComponents(bitmap.Width, bitmap.Height);
        }

        private void ResizeComponents(int width, int height)
        {
            mainPictureBox.Size = new Size(width, height);
            mainPictureBox.Parent.ClientSize = new Size(width + 10, height + 10);

            Point picLocation = new Point();
            picLocation.X = (mainPictureBox.Parent.ClientSize.Width - mainPictureBox.Size.Width) / 2;
            picLocation.Y = (mainPictureBox.Parent.ClientSize.Height - mainPictureBox.Size.Height) / 2;
            mainPictureBox.Location = picLocation;
        }
    }
}
