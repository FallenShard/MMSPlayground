using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MMSPlayground.Presenters;
using MMSPlayground.Views;

namespace MMSPlayground
{
    public partial class MainForm : Form, IMainView
    {
        private MainPresenter m_presenter = null;

        private int m_topMargin = 5;
        private int m_leftMargin = 5;
        private int m_rightMargin = 5;
        private int m_bottomMargin = 5;

        private float m_cachedAspectRatio = 1.0f;

        private ToolStripMenuItem m_activeResizeItem = null;
        private IResizeStrategy m_resizeMode = new PreserveAspectResize();

        public MainForm(MainPresenter presenter)
        {
            InitializeComponent();

            useWin32CoreToolStripMenuItem.Checked = true;
            m_activeResizeItem = preserveAspectToolStripMenuItem;
            m_activeResizeItem.Checked = true;

            m_topMargin += mainMenuStrip.Size.Height;
            m_bottomMargin += statusStrip.Size.Height;
            
            m_presenter = presenter;
            m_presenter.SetMainView(this);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ApplyResize();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Image";
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Text = dlg.SafeFileName;
                EnableMenuItems();

                m_presenter.SetBitmapFileName(dlg.FileName);
            }
            dlg.Dispose();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_presenter.RequestSaveBitmap();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void displayChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            displayChannelsToolStripMenuItem.Checked = !displayChannelsToolStripMenuItem.Checked;
            m_presenter.ShowChannelsView(displayChannelsToolStripMenuItem.Checked);
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_presenter.RequestBrightness();
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_presenter.RequestContrast();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_presenter.RequestUndo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_presenter.RequestRedo();
        }

        private void useWin32CoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            useWin32CoreToolStripMenuItem.Checked = !useWin32CoreToolStripMenuItem.Checked;
            m_presenter.UseWin32Core(useWin32CoreToolStripMenuItem.Checked);
        }

        private void resizeToOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_presenter.RequestResize();
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

        public void DisplayImage(Bitmap bitmap)
        {
            mainPictureBox.Image = bitmap;

            imgToolStripStatusLabel.Text = Text;
            dimToolStripStatusLabel.Text = bitmap.Width + " x " + bitmap.Height + " px";
            bppToolStripStatusLabel.Text = Image.GetPixelFormatSize(bitmap.PixelFormat).ToString();
            
            m_cachedAspectRatio = (float)bitmap.Size.Width / (float)bitmap.Size.Height;
            
            ApplyResize();
        }

        public void ResizeImage(Size imgSize)
        {
            mainPictureBox.Size = new Size(imgSize.Width, imgSize.Height);
            mainPictureBox.Parent.ClientSize = new Size(m_leftMargin + imgSize.Width + m_rightMargin,
                m_topMargin + imgSize.Height + m_bottomMargin);

            mainPictureBox.Location = new Point(m_leftMargin, m_topMargin);
        }

        public void SaveImage(Bitmap bitmap)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save Image";
            dlg.Filter = "Windows Bitmap(*.bmp)|*.bmp|JPG Image(*.jpg)|*.jpg|Portable Network Graphics (*.png)|*.png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_presenter.SaveBitmap(dlg.FileName);
            }
            dlg.Dispose();
        }

        public void DisplayErrorMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }

        public void SetChannelsViewStatus(bool status)
        {
            displayChannelsToolStripMenuItem.Checked = status;
        }

        public void InvalidateView()
        {
            mainPictureBox.Invalidate();
        }

        public void SetUndoEnabled(bool enabled)
        {
            undoToolStripMenuItem.Enabled = enabled;
            redoToolStripMenuItem.Enabled = enabled;
        }

        private void SetResizeMode(ToolStripMenuItem clickedItem, IResizeStrategy resizeMode)
        {
            m_resizeMode = resizeMode;
            m_activeResizeItem.Checked = false;
            m_activeResizeItem = clickedItem;
            m_activeResizeItem.Checked = true;

            m_presenter.ReceiveResizeMode(m_resizeMode);

            ApplyResize();
        }

        private void ApplyResize()
        {
            m_resizeMode.Resize(mainPictureBox, m_cachedAspectRatio, m_leftMargin, m_topMargin, m_rightMargin, m_bottomMargin);
        }

        private void EnableMenuItems()
        {
            saveToolStripMenuItem.Enabled = true;

            displayChannelsToolStripMenuItem.Enabled = true;
            brightnessToolStripMenuItem.Enabled = true;
            contrastToolStripMenuItem.Enabled = true;

            resizeToOriginalToolStripMenuItem.Enabled = true;
        }
    }
}
