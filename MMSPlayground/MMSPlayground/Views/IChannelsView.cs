using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using MMSPlayground.Model;

namespace MMSPlayground.Views
{
    public interface IChannelsView
    {
        void DisplayImages(Bitmap bitmap, Bitmap[] channels, Histogram[] channelHistograms);
        void SetVisible(bool visible);
    }
}
