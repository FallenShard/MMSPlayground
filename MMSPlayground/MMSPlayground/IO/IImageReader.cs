using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MMSPlayground.IO
{
    public interface IImageReader
    {
        Bitmap ReadFromFile(string fileName);
    }
}
