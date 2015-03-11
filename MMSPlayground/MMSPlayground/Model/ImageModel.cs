using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace MMSPlayground.Model
{
    public class ImageModel : IImageModel
    {
        public event BitmapChangedEventHandler BitmapChanged;

        private bool m_win32Mode = true;

        private Bitmap m_bitmap = null;

        private Bitmap[] m_channelBmps = new Bitmap[3];

        private IList<int>[] m_channelHistograms = new IList<int>[3];

        public ImageModel()
        {

        }

        public void SetBitmap(Bitmap bitmap)
        {
            m_bitmap = bitmap;

            ComputeChannels();

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

        public bool GetWin32CoreUsageMode()
        {
            return m_win32Mode;
        }

        public void SetWin32CoreUsageMode(bool enabled)
        {
            m_win32Mode = enabled;
        }

        private void ComputeChannels()
        {
            InitializeChannelBitmaps();
            InitializeChannelHistograms();

            Rectangle bmpRect = new Rectangle(0, 0, m_bitmap.Width, m_bitmap.Height);
            BitmapData bmd = m_bitmap.LockBits(bmpRect, ImageLockMode.ReadWrite, m_bitmap.PixelFormat);
            BitmapData bmdY  = m_channelBmps[0].LockBits(bmpRect, ImageLockMode.ReadWrite, m_channelBmps[0].PixelFormat);
            BitmapData bmdCb = m_channelBmps[1].LockBits(bmpRect, ImageLockMode.ReadWrite, m_channelBmps[1].PixelFormat);
            BitmapData bmdCr = m_channelBmps[2].LockBits(bmpRect, ImageLockMode.ReadWrite, m_channelBmps[2].PixelFormat);

            int bpp = Image.GetPixelFormatSize(bmd.PixelFormat) / 8;

            byte[] rgb = new byte[3];
            byte[] yCbCr = new byte[3];

            unsafe
            {
                for (int y = 0; y < bmd.Height; y++)
                {
                    byte* enhRow = (byte*)bmd.Scan0 + (y * bmd.Stride);
                    byte* YRow   = (byte*)bmdY.Scan0 + (y * bmdY.Stride);
                    byte* CbRow  = (byte*)bmdCb.Scan0 + (y * bmdCb.Stride);
                    byte* CrRow  = (byte*)bmdCr.Scan0 + (y * bmdCr.Stride);

                    for (int x = 0; x < bmd.Width; x++)
                    {
                        int index = x * bpp;
                        rgb[0] = enhRow[index + 2];
                        rgb[1] = enhRow[index + 1];
                        rgb[2] = enhRow[index + 0];

                        ImageUtils.RgbToYCbCr(rgb, yCbCr);

                        m_channelHistograms[0][yCbCr[0]]++;
                        m_channelHistograms[1][yCbCr[1]]++;
                        m_channelHistograms[2][yCbCr[2]]++;

                        YRow[index + 2] = yCbCr[0];
                        YRow[index + 1] = yCbCr[0];
                        YRow[index + 0] = yCbCr[0];

                        CbRow[index + 2] = 0;
                        CbRow[index + 1] = (byte)(0xFF - yCbCr[1]);
                        CbRow[index + 0] = yCbCr[1];

                        CrRow[index + 2] = yCbCr[2];
                        CrRow[index + 1] = (byte)(0xFF - yCbCr[2]);
                        CrRow[index + 0] = 0;
                    }
                }
            }

            m_bitmap.UnlockBits(bmd);
            m_channelBmps[0].UnlockBits(bmdY);
            m_channelBmps[1].UnlockBits(bmdCb);
            m_channelBmps[2].UnlockBits(bmdCr);
        }

        private void InitializeChannelBitmaps()
        {
            m_channelBmps[0] = new Bitmap(m_bitmap.Width, m_bitmap.Height, m_bitmap.PixelFormat);
            m_channelBmps[1] = new Bitmap(m_bitmap.Width, m_bitmap.Height, m_bitmap.PixelFormat);
            m_channelBmps[2] = new Bitmap(m_bitmap.Width, m_bitmap.Height, m_bitmap.PixelFormat);
        }

        private void InitializeChannelHistograms()
        {
            m_channelHistograms[0] = new List<int>(256);
            m_channelHistograms[1] = new List<int>(256);
            m_channelHistograms[2] = new List<int>(256);

            for (int i = 0; i < 256; i++)
            {
                m_channelHistograms[0].Add(0);
                m_channelHistograms[1].Add(0);
                m_channelHistograms[2].Add(0);
            }
        }
    }
}
