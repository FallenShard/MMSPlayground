using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MMSPlayground.Views
{
    public interface IChannelsView
    {
        void DisplayImages(Bitmap bitmap, Bitmap[] channels);
        void SetVisible(bool visible);
        void SetResizeMode(IResizeStrategy resizeMode);
    }
}
