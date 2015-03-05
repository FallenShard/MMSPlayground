using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMSPlayground.Views
{
    public class StretchResize : IResizeStrategy
    {
        public void Resize(PictureBox pictureBox, float aspectRatio, int leftMargin, int topMargin, int rightMargin, int bottomMargin)
        {
            Size parentSize = pictureBox.Parent.ClientSize;

            int adjHeight = parentSize.Height - topMargin - bottomMargin;
            int adjWidth = parentSize.Width - leftMargin - rightMargin;

            pictureBox.Size = new Size(adjWidth, adjHeight);
            pictureBox.Location = new Point(leftMargin, topMargin);
        }
    }
}
