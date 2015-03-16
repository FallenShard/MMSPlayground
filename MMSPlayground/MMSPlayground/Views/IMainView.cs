using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MMSPlayground.Views
{
    public interface IMainView
    {
        void DisplayImage(Bitmap bitmap);
        void ResizeImage(Size size);
        void SaveImage(Bitmap bitmap);
        void DisplayErrorMessage(string message, string title);
        void SetChannelsViewStatus(bool status);
        void InvalidateView();
        void SetUndoEnabled(bool enabled, string undoActionName);
        void SetRedoEnabled(bool enabled, string redoActionName);
    }
}
