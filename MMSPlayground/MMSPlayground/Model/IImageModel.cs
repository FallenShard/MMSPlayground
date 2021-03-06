﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using MMSPlayground.Data;

namespace MMSPlayground.Model
{
    public delegate void BitmapChangedEventHandler(IImageModel model, BitmapChangedEventArgs args);

    public interface IImageModel
    {
        event BitmapChangedEventHandler BitmapChanged;

        void SetBitmap(Bitmap bitmap);
        Bitmap GetBitmap();
        Bitmap[] GetChannels();
        Histogram[] GetHistograms();
        Size GetSize();
        int GetBitmapByteSize();

        bool GetWin32CoreUsageMode();
        void SetWin32CoreUsageMode(bool enabled);
    }
}
