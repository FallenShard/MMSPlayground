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
    public partial class SharpenDialog : Form
    {
        public SharpenDialog()
        {
            InitializeComponent();

            numericUpDown.Select();
            numericUpDown.Select(0, numericUpDown.Text.Length);
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public int GetBaseFactor()
        {
            return (int)numericUpDown.Value;
        }

        public int GetKernelSize()
        {
            if (radioButton3x3.Checked)
                return 3;
            if (radioButton5x5.Checked)
                return 5;
            if (radioButton7x7.Checked)
                return 7;

            return 3;
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
