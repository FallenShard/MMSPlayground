using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMSPlayground.Views
{
    public interface IResizeStrategy
    {
        void Resize(Control control, float aspectRatio, int leftMargin, int topMargin, int rightMargin, int bottomMargin);
    }
}
