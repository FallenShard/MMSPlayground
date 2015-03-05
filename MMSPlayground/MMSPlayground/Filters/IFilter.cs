using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSPlayground.Filters
{
    public interface IFilter
    {
        void Apply();
        void Undo();
        IFilter Clone();
    }
}
