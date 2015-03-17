using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MMSPlayground.Views;

namespace MMSPlayground.Presenters
{
    public interface IMainPresenter
    {
        void SetMainView(IMainView view);
        void ShowChannelsView(bool show);
        void ReceiveChannelsViewStatus(bool visible);

        void UseWin32Core(bool enable);

        void OpenBitmap(string fileName);
        void SaveBitmap(string fileName);

        void RequestSaveBitmap();
        void RequestResize();

        void RequestBrightness(int bias);
        void RequestContrast(double coefficient);
        void RequestSharpen(int kernelSize, int baseFactor);
        void RequestEdgeEnhancement(int threshold);

        void RequestUndo();
        void RequestRedo();
        void RequestRepeat();
        void SetUndoMemoryCapacity(int megabytes);
    }
}
