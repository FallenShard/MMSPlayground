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
    public class BpfReader : IImageReader
    {
        private readonly int WidthOffset = 0;
        private readonly int HeightOffset = 4;
        private readonly int StrideOffset = 8;
        private readonly int SamplingModeOffset = 12;
        private readonly int DataOffset = 16;

        public BpfReader()
        {

        }

        public Bitmap ReadFromFile(string fileName)
        {
            byte[] buff;

            using (FileStream file = File.Open(fileName, FileMode.Open))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                buff = memoryStream.ToArray();
            }

            return ReadFromBytes(buff);
        }

        public Bitmap ReadFromBytes(byte[] buff)
        {
            int width = BitConverter.ToInt32(buff, WidthOffset);
            int height = BitConverter.ToInt32(buff, HeightOffset);
            int stride = BitConverter.ToInt32(buff, StrideOffset);
            DownsamplingMode samplingMode = GetDownsamplingMode(BitConverter.ToInt32(buff, SamplingModeOffset));

            byte[][] channels = GetChannelData(buff, width, height, samplingMode);

            Bitmap bitmap = new Bitmap(width, height, GetPixelFormat(width, stride));

            Rectangle bmpRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmd = bitmap.LockBits(bmpRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            int bpp = ImageUtils.GetComponentsPerPixel(bmd);

            byte[] yCbCr = new byte[3];
            byte[] rgb = new byte[3];
            int fullIter = -1;
            int shortIter = -1;

            unsafe
            {
                for (int y = 0; y < bmd.Height; y++)
                {
                    byte* dataRow = (byte*)bmd.Scan0 + (y * bmd.Stride);

                    for (int x = 0; x < bmd.Width; x++)
                    {
                        int index = x * bpp;
                        fullIter++;
                        if (x % 4 == 0)
                            shortIter++;

                        ReadFromChannels(yCbCr, channels, fullIter, shortIter, samplingMode);

                        ColorSpace.YCbCrToRgb(yCbCr, rgb);

                        dataRow[index + 2] = rgb[0];
                        dataRow[index + 1] = rgb[1];
                        dataRow[index + 0] = rgb[2];
                    }
                }
            }

            bitmap.UnlockBits(bmd);

            return bitmap;
        }

        private byte[][] GetChannelData(byte[] buff, int width, int height, DownsamplingMode mode)
        {
            IList<byte> yData = new List<byte>();
            IList<byte> cbData = new List<byte>();
            IList<byte> crData = new List<byte>();

            int iter = DataOffset;
            int shortLen = height * (width % 4 == 0 ? width / 4 : width / 4 + 1);
            int len = height * width;

            int yEnd = 0, cbEnd = 0, crEnd = 0;

            switch(mode)
            {
                case DownsamplingMode.Y:
                    {
                        yEnd = iter + len;
                        cbEnd = yEnd + shortLen;
                        crEnd = cbEnd + shortLen;
                        break;
                    }

                case DownsamplingMode.Cb:
                    {
                        yEnd = iter + shortLen;
                        cbEnd = yEnd + len;
                        crEnd = cbEnd + shortLen;
                        break;
                    }

                case DownsamplingMode.Cr:
                    {
                        yEnd = iter + shortLen;
                        cbEnd = yEnd + shortLen;
                        crEnd = cbEnd + len;
                        break;
                    }
            }

            while (iter < yEnd)
            {
                yData.Add(buff[iter++]);
            }

            while (iter < cbEnd)
            {
                cbData.Add(buff[iter++]);
            }

            while (iter < crEnd)
            {
                crData.Add(buff[iter++]);
            }

            byte[][] channels = new byte[3][];
            channels[0] = yData.ToArray();
            channels[1] = cbData.ToArray();
            channels[2] = crData.ToArray();

            return channels;
        }

        private void ReadFromChannels(byte[] yCbCr, byte[][] channels, int fullIter, int shortIter, DownsamplingMode mode)
        {
            switch(mode)
            {
                case DownsamplingMode.Y:
                    {
                        yCbCr[0] = channels[0][fullIter];
                        yCbCr[1] = channels[1][shortIter];
                        yCbCr[2] = channels[2][shortIter];
                        break;
                    }

                case DownsamplingMode.Cb:
                    {
                        yCbCr[0] = channels[0][shortIter];
                        yCbCr[1] = channels[1][fullIter];
                        yCbCr[2] = channels[2][shortIter];
                        break;
                    }

                case DownsamplingMode.Cr:
                    {
                        yCbCr[0] = channels[0][shortIter];
                        yCbCr[1] = channels[1][shortIter];
                        yCbCr[2] = channels[2][fullIter];
                        break;
                    }
            }
        }

        private static PixelFormat GetPixelFormat(int width, int stride)
        {
            int bpp = stride / width;
            switch (bpp)
            {
                case 1:
                    return PixelFormat.Format8bppIndexed;
                case 3:
                    return PixelFormat.Format24bppRgb;
                default:
                    return PixelFormat.Format24bppRgb;
            }
        }

        private static DownsamplingMode GetDownsamplingMode(int samplingMode)
        {
            return (DownsamplingMode)samplingMode;
        }
    }
}
