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
    public partial class BrightnessDialog : Form
    {
        public BrightnessDialog()
        {
            InitializeComponent();

            numericUpDown.Select();
            numericUpDown.Select(0, numericUpDown.Text.Length);
        }

        public int GetBrightnessBias()
        {
            return (int)numericUpDown.Value;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void numericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            if (numericUpDown.Value > numericUpDown.Maximum)
                numericUpDown.Value = numericUpDown.Maximum;

            if (numericUpDown.Value < numericUpDown.Minimum)
                numericUpDown.Value = numericUpDown.Minimum;
        }
    }
}
