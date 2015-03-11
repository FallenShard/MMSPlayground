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
    public class MainPresenter
    {
        private ImageModel m_model = null;

        private IMainView m_mainView = null;

        private ChannelsPresenter m_channelsPresenter = null;

        private Stack<IFilter> m_filterHistory = new Stack<IFilter>();
        private IFilter m_redoFilter = null;

        public MainPresenter(ImageModel model)
        {
            m_model = model;
            m_model.BitmapChanged += new ImageModel.BitmapChangedEventHandler(this.OnBitmapChanged);

            m_channelsPresenter = new ChannelsPresenter(model, this);
            IChannelsView channelsView = new ChannelsForm(m_channelsPresenter);
        }

        public void SetMainView(IMainView view)
        {
            m_mainView = view;
        }

        public void SetBitmapFileName(string fileName)
        {
            Bitmap bitmap = new Bitmap(fileName);
            m_model.SetBitmap(bitmap);
        }

        private void OnBitmapChanged(ImageModel model, BitmapChangedEventArgs args)
        {
            m_mainView.DisplayImage(args.Bitmap);
        }

        public void RequestSaveBitmap()
        {
            Bitmap bitmap = m_model.GetBitmap();

            if (bitmap == null)
                m_mainView.DisplayErrorMessage("Could not save image. No image has been opened yet.", "Save Image");
            else
                m_mainView.SaveImage(bitmap);
        }

        public void SaveBitmap(string fileName)
        {
            Bitmap bitmap = m_model.GetBitmap();
            bitmap.Save(fileName);
        }

        public void ShowChannelsView(bool show)
        {
            m_channelsPresenter.ShowChannelsView(show);
        }

        public void RequestResize()
        {
            Size modelSize = m_model.GetSize();

            if (modelSize == ImageModel.ErrorSize)
                m_mainView.DisplayErrorMessage("Could not resize image. No image has been opened yet.", "Resize Image");
            else
                m_mainView.ResizeImage(modelSize);
        }

        public void SetChannelsViewStatus(bool visible)
        {
            m_mainView.SetChannelsViewStatus(visible);
        }

        public void UseWin32Core(bool enable)
        {
            m_model.UseWin32Core(enable);
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

        public void RequestEdgeEnhancement(double coefficient)
        {
            //ApplyFilter(new EdgeEnhancementFilter(m_model, coefficient));
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
    }
}
