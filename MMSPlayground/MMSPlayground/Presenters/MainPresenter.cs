using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using MMSPlayground.Views;
using MMSPlayground.Views.Forms;
using MMSPlayground.Model;
using MMSPlayground.Filters;


namespace MMSPlayground.Presenters
{
    public class MainPresenter : IMainPresenter
    {
        private IImageModel m_model = null;

        private IMainView m_mainView = null;

        private ChannelsPresenter m_channelsPresenter = null;

        private Stack<IFilter> m_filterHistory = new Stack<IFilter>();
        private IFilter m_redoFilter = null;

        public MainPresenter(IImageModel model)
        {
            m_model = model;
            m_model.BitmapChanged += new BitmapChangedEventHandler(this.OnBitmapChanged);

            m_channelsPresenter = new ChannelsPresenter(model, this);
            IChannelsView channelsView = new ChannelsForm(m_channelsPresenter);
        }

        public void SetMainView(IMainView view)
        {
            m_mainView = view;
        }

        public void ShowChannelsView(bool show)
        {
            m_channelsPresenter.ShowChannelsView(show);
        }

        public void ReceiveChannelsViewStatus(bool visible)
        {
            m_mainView.SetChannelsViewStatus(visible);
        }

        public void UseWin32Core(bool enable)
        {
            m_model.SetWin32CoreUsageMode(enable);
        }

        public void OpenBitmap(string fileName)
        {
            Bitmap bitmap = new Bitmap(fileName);
            m_model.SetBitmap(bitmap);
        }

        public void SaveBitmap(string fileName)
        {
            Bitmap bitmap = m_model.GetBitmap();
            bitmap.Save(fileName);
        }

        public void RequestSaveBitmap()
        {
            m_mainView.SaveImage(m_model.GetBitmap());
        }

        public void RequestResize()
        {
            m_mainView.ResizeImage(m_model.GetSize());
        }

        public void RequestBrightness(int bias)
        {
            ApplyFilter(new BrightnessFilter(m_model, bias));
        }

        public void RequestContrast(double coefficient)
        {
            ApplyFilter(new ContrastFilter(m_model, coefficient));
        }

        public void RequestSharpen(int kernelSize, int baseFactor)
        {
            ApplyFilter(new SharpenFilter(m_model, kernelSize, baseFactor));
        }

        public void RequestEdgeEnhancement(int kernelSize, int threshold)
        {
            ApplyFilter(new EdgeEnhancementFilter(m_model, kernelSize, threshold));
        }

        public void RequestUndo()
        {
            if (m_filterHistory.Count > 0)
            {
                IFilter filter = m_filterHistory.Pop();
                filter.Undo();

                if (m_filterHistory.Count == 0)
                    m_mainView.SetUndoEnabled(false);
            }
        }

        public void RequestRedo()
        {
            if (m_redoFilter != null)
            {
                ApplyFilter(m_redoFilter);
            }
        }

        private void ApplyFilter(IFilter filter)
        {
            filter.Apply();

            m_filterHistory.Push(filter);
            m_mainView.SetUndoEnabled(true);

            m_redoFilter = filter.Clone();
            m_mainView.SetRedoEnabled(true);
        }

        private void OnBitmapChanged(IImageModel model, BitmapChangedEventArgs args)
        {
            m_mainView.DisplayImage(args.Bitmap);
        }
    }
}
