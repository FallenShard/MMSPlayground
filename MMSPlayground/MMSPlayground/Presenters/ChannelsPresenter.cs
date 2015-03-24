using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MMSPlayground.Model;
using MMSPlayground.Views;
using MMSPlayground.Filters;

namespace MMSPlayground.Presenters
{
    public class ChannelsPresenter : IChannelsPresenter
    {
        IImageModel m_model = null;

        IChannelsView m_channelsView = null;

        MainPresenter m_mainPresenter = null;

        public ChannelsPresenter(IImageModel model, MainPresenter mainPresenter)
        {
            m_model = model;

            m_model.BitmapChanged += new BitmapChangedEventHandler(this.OnBitmapChanged);

            m_mainPresenter = mainPresenter;
        }

        public void SetChannelsView(IChannelsView view)
        {
            m_channelsView = view;
        }

        public void SendChannelsViewStatus(bool visible)
        {
            m_mainPresenter.ReceiveChannelsViewStatus(visible);
        }

        public void ShowChannelsView(bool visible)
        {
            m_channelsView.SetVisible(visible);
        }

        public void RequestAverageReplacement(int yLower, int yUpper, int cbLower, int cbUpper, int crLower, int crUpper)
        {
            HistoFilterPackage package = new HistoFilterPackage();
            package.lower[0] = yLower; package.lower[1] = cbLower; package.lower[2] = crLower;
            package.upper[0] = yUpper; package.upper[1] = cbUpper; package.upper[2] = crUpper;
            m_mainPresenter.ApplyFilter(new HistoAverageFilter(m_model, package));
        }

        public void RequestHighestReplacement(int yLower, int yUpper, int cbLower, int cbUpper, int crLower, int crUpper)
        {
            HistoFilterPackage package = new HistoFilterPackage();
            package.lower[0] = yLower; package.lower[1] = cbLower; package.lower[2] = crLower;
            package.upper[0] = yUpper; package.upper[1] = cbUpper; package.upper[2] = crUpper;
            m_mainPresenter.ApplyFilter(new HistoHighestFilter(m_model, package));
        }

        private void OnBitmapChanged(ImageModel model, BitmapChangedEventArgs args)
        {
            m_channelsView.DisplayImages(args.Bitmap, args.Channels, args.ChannelHistograms);
        }
    }
}
