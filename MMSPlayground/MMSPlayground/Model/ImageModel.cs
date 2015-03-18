﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MMSPlayground.Model
{
    public class ImageModel : IImageModel
    {
        public event BitmapChangedEventHandler BitmapChanged;

        private bool m_win32Mode = true;

        private Bitmap m_bitmap = null;
        private int m_memorySize = 0;

        private Bitmap[] m_channelBmps = new Bitmap[3];

        private IList<int>[] m_channelHistograms = new IList<int>[3];

        public ImageModel()
        {
        }

        public void SetBitmap(Bitmap bitmap)
        {
            m_bitmap = bitmap;
            BitmapData data = m_bitmap.LockBits(new Rectangle(0, 0, m_bitmap.Width, m_bitmap.Height), ImageLockMode.ReadWrite, m_bitmap.PixelFormat);
            m_memorySize = data.Stride * data.Height * ImageUtils.GetComponentsPerPixel(data);
            m_bitmap.UnlockBits(data);

            ImageUtils.ComputeYCbCr(m_bitmap, ref m_channelBmps, ref m_channelHistograms);

            BitmapChanged(this, new BitmapChangedEventArgs(m_bitmap, m_channelBmps, m_channelHistograms));
        }

        public Bitmap GetBitmap()
        {
            return m_bitmap;
        }

        public Bitmap[] GetChannels()
        {
            return m_channelBmps;
        }

        public Size GetSize()
        {
            return m_bitmap.Size;
        }

        public int GetBitmapByteSize()
        {
            return m_memorySize;
        }

        public bool GetWin32CoreUsageMode()
        {
            return m_win32Mode;
        }

        public void SetWin32CoreUsageMode(bool enabled)
        {
            m_win32Mode = enabled;
        }
    }
}
