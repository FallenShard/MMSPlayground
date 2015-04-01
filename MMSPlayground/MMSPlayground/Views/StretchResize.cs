using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMSPlayground.Views
{
    public class StretchResize : IResizeMode
    {
        public void Resize(Control control, float aspectRatio, int leftMargin, int topMargin, int rightMargin, int bottomMargin)
        {
            Size parentSize = control.Parent.ClientSize;

            int adjHeight = parentSize.Height - topMargin - bottomMargin;
            int adjWidth = parentSize.Width - leftMargin - rightMargin;

            control.Size = new Size(adjWidth, adjHeight);
            control.Location = new Point(leftMargin, topMargin);
        }
    }
}
