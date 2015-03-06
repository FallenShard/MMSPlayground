using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMSPlayground.Views
{
    public class PreserveAspectResize : IResizeStrategy
    {
        public void Resize(Control control, float aspectRatio, int leftMargin, int topMargin, int rightMargin, int bottomMargin)
        {
            Size parentSize = control.Parent.ClientSize;

            int adjHeight = parentSize.Height - topMargin - bottomMargin;
            int adjWidth = parentSize.Width - leftMargin - rightMargin;

            float resizeRatio = (float)adjWidth / (float)adjHeight;

            int newHeight;
            int newWidth;

            if (resizeRatio > aspectRatio)
            {
                newHeight = adjHeight;
                newWidth = (int)(aspectRatio * adjHeight);
            }
            else
            {
                newWidth = adjWidth;
                newHeight = (int)(adjWidth / aspectRatio);
            }

            int x = leftMargin;
            int y = topMargin;

            if (newHeight == adjHeight)
            {
                x += (adjWidth - newWidth) / 2;
            }
            else
            {
                y += (adjHeight - newHeight) / 2;
            }

            control.Size = new Size(newWidth, newHeight);
            control.Location = new Point(x, y);
        }
    }
}
