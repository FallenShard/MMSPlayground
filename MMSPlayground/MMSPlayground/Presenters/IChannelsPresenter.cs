using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MMSPlayground.Views;

namespace MMSPlayground.Presenters
{
    public interface IChannelsPresenter
    {
        void SetChannelsView(IChannelsView view);
        void ShowChannelsView(bool visible);
        void SendChannelsViewStatus(bool visibility);
    }
}
