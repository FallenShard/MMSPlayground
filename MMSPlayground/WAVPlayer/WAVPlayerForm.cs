using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WAVPlayer
{
    public partial class WAVPlayerForm : Form
    {
        private IList<Label> m_labels = new List<Label>();
        private IList<NumericUpDown> m_upDowns = new List<NumericUpDown>();

        private WAVPlayerPresenter m_presenter = null;

        public WAVPlayerForm(WAVPlayerPresenter presenter)
        {
            InitializeComponent();

            //BuildChannelControls(8);

            m_presenter = presenter;
        }

        public void BuildChannelControls(int channels)
        {
            RemoveControls();

            int channelCounter = 0;

            for (int i = 0; i < 20; i++)
            {
                int j;
                for (j = 0; j < 3; j++)
                {
                    Label label = new Label();
                    label.AutoSize = true;
                    label.Location = new Point(4 + j * 120, 8 + i * 50);
                    label.Name = "Label" + (i * 3 + j + 1);
                    label.TabIndex = 2 * (i * 3 + j) + 0;
                    label.Text = "Channel " + (i * 3 + j + 1);
                    m_labels.Add(label);
                    mainPanel.Controls.Add(label);

                    NumericUpDown numericUpDown = new NumericUpDown();
                    numericUpDown.Location = new System.Drawing.Point(7 + j * 120, 28 + i * 50);
                    numericUpDown.Name = "NumericUpDown" + i + j;
                    numericUpDown.Size = new System.Drawing.Size(100, 20);
                    numericUpDown.TabIndex = 2 * (i * 3 + j) + 1;
                    numericUpDown.Minimum = 0;
                    numericUpDown.Maximum = 255;
                    numericUpDown.KeyUp += new System.Windows.Forms.KeyEventHandler(NumericUpDown_KeyUp);
                    m_upDowns.Add(numericUpDown);
                    mainPanel.Controls.Add(numericUpDown);

                    channelCounter++;
                    if (channelCounter == channels)
                        break;
                }

                if (j < 3)
                    break;
            }
        }

        private void RemoveControls()
        {
            foreach (Label lab in m_labels)
            {
                mainPanel.Controls.Remove(lab);
            }
            m_labels.Clear();

            foreach (NumericUpDown upDown in m_upDowns)
            {
                mainPanel.Controls.Remove(upDown);
            }
            m_upDowns.Clear();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open WAV";
            dlg.Filter = "WAV Files(*.WAV)|*.WAV";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                applyOffsetButton.Enabled = true;
                playButton.Enabled = true;
                saveButton.Enabled = true;

                Text = dlg.SafeFileName;
                m_presenter.SetWavFile(dlg.FileName);
            }
            dlg.Dispose();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save WAV";
            dlg.Filter = "WAV Files(*.WAV)|*.WAV";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_presenter.SaveWavFile(dlg.FileName);
            }
            dlg.Dispose();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            m_presenter.Play();
        }

        private void WAVPlayerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_presenter.CleanUp();
        }

        private void applyOffsetButton_Click(object sender, EventArgs e)
        {
            byte[] offsets = new byte[m_upDowns.Count];

            for (int i = 0; i < offsets.Length; i++)
            {
                offsets[i] = (byte)m_upDowns[i].Value;
            }

            m_presenter.ApplyOffset(offsets);
        }

        private void NumericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            NumericUpDown upDown = sender as NumericUpDown;
            if (upDown.Value > upDown.Maximum)
                upDown.Value = upDown.Maximum;

            if (upDown.Value < upDown.Minimum)
                upDown.Value = upDown.Minimum;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            m_presenter.Stop();
        }
    }
}
