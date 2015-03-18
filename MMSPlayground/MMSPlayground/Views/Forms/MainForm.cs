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

namespace MMSPlayground.Views.Forms
{
    public partial class MainForm : Form, IMainView
    {
        private IMainPresenter m_presenter = null;

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
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.PNG;*.BPF)|*.BMP;*.JPG;*.PNG;*.BPF";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Text = "MMS Playground - " + dlg.SafeFileName;
                EnableMenuItems();

                m_presenter.OpenBitmap(dlg.FileName);
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
            BrightnessDialog dialog = new BrightnessDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_presenter.RequestBrightness(dialog.GetBrightnessBias());
            }

            dialog.Dispose();
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContrastDialog dialog = new ContrastDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_presenter.RequestContrast(dialog.GetContrastCoeff());
            }

            dialog.Dispose();
        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharpenDialog dialog = new SharpenDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_presenter.RequestSharpen(dialog.GetKernelSize(), dialog.GetBaseFactor());
            }

            dialog.Dispose();
        }

        private void edgeEnhancementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EdgeEnhancementDialog dialog = new EdgeEnhancementDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_presenter.RequestEdgeEnhancement(dialog.GetThreshold());
            }

            dialog.Dispose();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_presenter.RequestUndo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_presenter.RequestRedo();
        }

        private void repeatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_presenter.RequestRepeat();
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

        private void memoryCapacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CapacityDialog dialog = new CapacityDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_presenter.SetUndoMemoryCapacity(dialog.GetMegabytes());
            }
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
            dlg.Filter = "Windows Bitmap(*.bmp)|*.bmp|JPG Image(*.jpg)|*.jpg|Portable Network Graphics (*.png)|*.png|Bart Picture Format (*.bpf)|*.bpf";
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

        public void SetUndoEnabled(bool enabled, string undoActionName)
        {
            undoToolStripMenuItem.Enabled = enabled;
            undoToolStripMenuItem.Text = "Undo " + undoActionName;
        }

        public void SetRedoEnabled(bool enabled, string redoActionName)
        {
            redoToolStripMenuItem.Enabled = enabled;
            redoToolStripMenuItem.Text = "Redo " + redoActionName;
        }

        public void SetRepeatEnabled(bool enabled, string repeatActionName)
        {
            repeatToolStripMenuItem.Enabled = enabled;
            repeatToolStripMenuItem.Text = "Repeat " + repeatActionName;
        }

        private void SetResizeMode(ToolStripMenuItem clickedItem, IResizeStrategy resizeMode)
        {
            m_resizeMode = resizeMode;
            m_activeResizeItem.Checked = false;
            m_activeResizeItem = clickedItem;
            m_activeResizeItem.Checked = true;

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
            sharpenToolStripMenuItem.Enabled = true;
            edgeEnhancementToolStripMenuItem.Enabled = true;

            resizeToOriginalToolStripMenuItem.Enabled = true;
        }
    }
}
