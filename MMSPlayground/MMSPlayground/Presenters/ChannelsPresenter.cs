using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MMSPlayground.Model;
using MMSPlayground.Views;

namespace MMSPlayground.Presenters
{
    public class ChannelsPresenter
    {
        ImageModel m_model = null;

        IChannelsView m_channelsView = null;

        MainPresenter m_mainPresenter = null;

        public ChannelsPresenter(ImageModel model, MainPresenter mainPresenter)
        {
            m_model = model;

            m_model.BitmapChanged += new ImageModel.BitmapChangedEventHandler(this.BitmapChanged);

            m_mainPresenter = mainPresenter;
        }

        public void SetChannelsView(IChannelsView view)
        {
            m_channelsView = view;
        }

        public void ShowChannelsView(bool visible)
        {
            m_channelsView.SetVisible(visible);
        }

        private void BitmapChanged(ImageModel model, BitmapChangedEventArgs args)
        {
            m_channelsView.DisplayImages(args.Bitmap, args.Channels, args.ChannelHistograms);
        }

        public void SetChannelsViewStatus(bool visible)
        {
            m_mainPresenter.SetChannelsViewStatus(visible);
        }
    }
}
