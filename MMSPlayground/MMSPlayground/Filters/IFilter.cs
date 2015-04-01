using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MMSPlayground.Filters
{
    public interface IFilter
    {
        void Apply();
        Bitmap GetRawResults();
        void Undo();
        IFilter Clone();
        string FilterName { get; }
    }
}
