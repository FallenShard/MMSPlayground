using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

using MMSPlayground.Utils;

namespace MMSPlayground.IO
{
    public class BpfWriter : IImageWriter
    {
        private DownsamplingMode m_samplingMode = DownsamplingMode.Y;

        public BpfWriter(DownsamplingMode samplingMode)
        {
            m_samplingMode = samplingMode;
        }

        public void SaveToFile(Bitmap bitmap, string fileName)
        {
            Bitmap yCbCrBmp = null;
            ColorSpace.ComputeYCbCr(bitmap, ref yCbCrBmp);

            Rectangle bmpRect = new Rectangle(0, 0, yCbCrBmp.Width, yCbCrBmp.Height);
            BitmapData bmdYCbCr = yCbCrBmp.LockBits(bmpRect, ImageLockMode.ReadWrite, yCbCrBmp.PixelFormat);

            IList<byte> yChannel = new List<byte>();
            IList<byte> cbChannel = new List<byte>();
            IList<byte> crChannel = new List<byte>();

            int bpp = ImageUtils.GetComponentsPerPixel(bmdYCbCr);

            unsafe
            {
                for (int y = 0; y < bmdYCbCr.Height; y++)
                {
                    byte* dataRow = (byte*)bmdYCbCr.Scan0 + (y * bmdYCbCr.Stride);

                    for (int x = 0; x < bmdYCbCr.Width; x++)
                    {
                        int index = x * bpp;

                        Downsample(yChannel, cbChannel, crChannel, dataRow, index, x);
                    }
                }
            }

            yCbCrBmp.UnlockBits(bmdYCbCr);

            byte[] yBytes = yChannel.ToArray();
            byte[] cbBytes = cbChannel.ToArray();
            byte[] crBytes = crChannel.ToArray();

            using (FileStream file = File.Create(fileName))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(BitConverter.GetBytes(bmdYCbCr.Width), 0, sizeof(int));
                memoryStream.Write(BitConverter.GetBytes(bmdYCbCr.Height), 0, sizeof(int));
                memoryStream.Write(BitConverter.GetBytes(bmdYCbCr.Stride), 0, sizeof(int));
                memoryStream.Write(BitConverter.GetBytes((int)m_samplingMode), 0, sizeof(int));
                memoryStream.Write(yBytes, 0, yBytes.Length);
                memoryStream.Write(cbBytes, 0, cbBytes.Length);
                memoryStream.Write(crBytes, 0, crBytes.Length);

                memoryStream.WriteTo(file);
            }
        }

        public byte[] GetRawBytes(Bitmap bitmap)
        {
            byte[] output = null;

            using (MemoryStream stream = new MemoryStream())
            {
                Bitmap yCbCrBmp = null;
                ColorSpace.ComputeYCbCr(bitmap, ref yCbCrBmp);

                Rectangle bmpRect = new Rectangle(0, 0, yCbCrBmp.Width, yCbCrBmp.Height);
                BitmapData bmdYCbCr = yCbCrBmp.LockBits(bmpRect, ImageLockMode.ReadWrite, yCbCrBmp.PixelFormat);

                IList<byte> yChannel = new List<byte>();
                IList<byte> cbChannel = new List<byte>();
                IList<byte> crChannel = new List<byte>();
                int bpp = ImageUtils.GetComponentsPerPixel(bmdYCbCr);

                unsafe
                {
                    for (int y = 0; y < bmdYCbCr.Height; y++)
                    {
                        byte* dataRow = (byte*)bmdYCbCr.Scan0 + (y * bmdYCbCr.Stride);

                        for (int x = 0; x < bmdYCbCr.Width; x++)
                        {
                            int index = x * bpp;

                            Downsample(yChannel, cbChannel, crChannel, dataRow, index, x);
                        }
                    }
                }

                yCbCrBmp.UnlockBits(bmdYCbCr);

                byte[] yBytes = yChannel.ToArray();
                byte[] cbBytes = cbChannel.ToArray();
                byte[] crBytes = crChannel.ToArray();

                stream.Write(BitConverter.GetBytes(bmdYCbCr.Width), 0, sizeof(int));
                stream.Write(BitConverter.GetBytes(bmdYCbCr.Height), 0, sizeof(int));
                stream.Write(BitConverter.GetBytes(bmdYCbCr.Stride), 0, sizeof(int));
                stream.Write(BitConverter.GetBytes((int)m_samplingMode), 0, sizeof(int));
                stream.Write(yBytes, 0, yBytes.Length);
                stream.Write(cbBytes, 0, cbBytes.Length);
                stream.Write(crBytes, 0, crBytes.Length);

                output = stream.ToArray();
            }

            return output;
        }

        private unsafe void Downsample(IList<byte> y, IList<byte> cb, IList<byte> cr, byte* data, int index, int x)
        {
            if (m_samplingMode == DownsamplingMode.Y)
            {
                y.Add(data[index + 0]);
                if (x % 4 == 0)
                {
                    cb.Add(data[index + 1]);
                    cr.Add(data[index + 2]);
                }
            }
            else if (m_samplingMode == DownsamplingMode.Cb)
            {
                cb.Add(data[index + 1]);
                if (x % 4 == 0)
                {
                    y.Add(data[index + 0]);
                    cr.Add(data[index + 2]);
                }
            }
            else if (m_samplingMode == DownsamplingMode.Cr)
            {
                cr.Add(data[index + 2]);
                if (x % 4 == 0)
                {
                    y.Add(data[index + 0]);
                    cb.Add(data[index + 1]);
                }
            }
        }
    }
}
