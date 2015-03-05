using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMSPlayground
{
    public partial class ContrastDialog : Form
    {
        public ContrastDialog()
        {
            InitializeComponent();
        }

        public double GetContrastCoeff()
        {
            double contrast = (double)numericUpDown.Value;
            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;

            return contrast;
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
