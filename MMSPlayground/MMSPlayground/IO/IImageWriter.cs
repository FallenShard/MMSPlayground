using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MMSPlayground.IO
{
    public interface IImageWriter
    {
        void SaveToFile(Bitmap bitmap, string fileName);
    }
}
