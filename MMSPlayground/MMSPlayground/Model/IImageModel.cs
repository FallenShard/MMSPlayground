using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MMSPlayground.Model
{
    public delegate void BitmapChangedEventHandler(ImageModel model, BitmapChangedEventArgs args);

    public interface IImageModel
    {
        event BitmapChangedEventHandler BitmapChanged;

        void SetBitmap(Bitmap bitmap);
        Bitmap GetBitmap();
        Bitmap[] GetChannels();
        Size GetSize();
        int GetBitmapByteSize();

        bool GetWin32CoreUsageMode();
        void SetWin32CoreUsageMode(bool enabled);
    }
}
