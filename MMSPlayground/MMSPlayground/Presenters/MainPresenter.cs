using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using MMSPlayground.Views;
using MMSPlayground.Views.Forms;
using MMSPlayground.Model;
using MMSPlayground.Filters;
using MMSPlayground.IO;


namespace MMSPlayground.Presenters
{
    public class MainPresenter : IMainPresenter
    {
        private IImageModel m_model = null;

        private IMainView m_mainView = null;

        private ChannelsPresenter m_channelsPresenter = null;

        private long m_memoryCapacity = 0;
        private long m_memorySize = 0;
        private LinkedList<IFilter> m_undoDeque = new LinkedList<IFilter>();
        private Stack<IFilter> m_redoStack = new Stack<IFilter>();
        private IFilter m_repeatFilter = null;

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
            Bitmap bitmap = BitmapIO.Open(fileName);
            m_model.SetBitmap(bitmap);

            m_undoDeque.Clear();
            m_mainView.SetUndoEnabled(false, "");

            m_redoStack.Clear();
            m_mainView.SetRedoEnabled(false, "");

            m_repeatFilter = null;
            m_mainView.SetRepeatEnabled(false, "");
        }

        public void SaveBitmap(string fileName)
        {
            Bitmap bitmap = m_model.GetBitmap();
            BitmapIO.Save(bitmap, fileName);
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

        public void RequestEdgeEnhancement(int threshold)
        {
            ApplyFilter(new EdgeEnhancementFilter(m_model, threshold));
        }

        public void RequestUndo()
        {
            if (m_undoDeque.Count > 0)
            {
                IFilter filter = m_undoDeque.Last();
                m_undoDeque.RemoveLast();
                filter.Undo();

                m_memorySize -= m_model.GetBitmapByteSize();

                if (m_undoDeque.Count == 0)
                    m_mainView.SetUndoEnabled(false, "");
                else
                    m_mainView.SetUndoEnabled(true, m_undoDeque.Last().FilterName);

                m_redoStack.Push(filter);
                m_mainView.SetRedoEnabled(true, m_redoStack.Peek().FilterName);
            }
        }

        public void RequestRedo()
        {
            if (m_redoStack.Count > 0)
            {
                IFilter filter = m_redoStack.Pop();
                filter.Apply();

                m_memorySize += m_model.GetBitmapByteSize();
                m_undoDeque.AddLast(filter);
                m_mainView.SetUndoEnabled(true, m_undoDeque.Last().FilterName);

                m_repeatFilter = filter.Clone();
                m_mainView.SetRepeatEnabled(true, m_repeatFilter.FilterName);

                if (m_redoStack.Count == 0)
                    m_mainView.SetRedoEnabled(false, "");
                else
                    m_mainView.SetRedoEnabled(true, m_redoStack.Peek().FilterName);
            }
        }

        public void RequestRepeat()
        {
            if (m_repeatFilter != null)
            {
                ApplyFilter(m_repeatFilter);
            }
        }

        public void SetUndoMemoryCapacity(int megabytes)
        {
            m_memoryCapacity = megabytes << 20;
        }

        public void ApplyFilter(IFilter filter)
        {
            filter.Apply();

            m_memorySize += m_model.GetBitmapByteSize();
            
            if (m_memoryCapacity != 0 && m_memorySize > m_memoryCapacity && m_undoDeque.Count > 0)
            {
                m_undoDeque.RemoveFirst();
                m_memorySize -= m_model.GetBitmapByteSize();
            }

            m_undoDeque.AddLast(filter);
            m_mainView.SetUndoEnabled(true, m_undoDeque.Last().FilterName);

            m_repeatFilter = filter.Clone();
            m_mainView.SetRepeatEnabled(true, m_repeatFilter.FilterName);

            m_redoStack.Clear();
            m_mainView.SetRedoEnabled(false, "");

            GC.Collect();
        }

        private void OnBitmapChanged(IImageModel model, BitmapChangedEventArgs args)
        {
            m_mainView.DisplayImage(args.Bitmap);
        }
    }
}
