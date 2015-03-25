using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using MMSPlayground.Utils;

namespace MMSPlayground.IO
{
    public class BpfReader
    {
        private readonly int WidthOffset = 0;
        private readonly int HeightOffset = 4;
        private readonly int StrideOffset = 8;

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

            int width = BitConverter.ToInt32(buff, WidthOffset);
            int height = BitConverter.ToInt32(buff, HeightOffset);
            int stride = BitConverter.ToInt32(buff, StrideOffset);

            IList<byte> yData = new List<byte>();
            IList<byte> cbData = new List<byte>();
            IList<byte> crData = new List<byte>();

            int iter = 12;
            int end = iter + width * height;

            while (iter < end)
            {
                yData.Add(buff[iter++]);
            }

            int step = (buff.Length - iter) / 2;
            end += step;
            while (iter < end)
            {
                cbData.Add(buff[iter]);
                crData.Add(buff[iter + step]);
                iter++;
            }

            byte[] yBytes = yData.ToArray();
            byte[] cbBytes = cbData.ToArray();
            byte[] crBytes = crData.ToArray();

            Bitmap bitmap = new Bitmap(width, height, GetPixelFormat(width, stride));

            Rectangle bmpRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmd = bitmap.LockBits(bmpRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            int bpp = ImageUtils.GetComponentsPerPixel(bmd);

            byte[] yCbCr = new byte[3];
            byte[] rgb = new byte[3];
            int yIter = 0;
            int chromaIter = 0;

            unsafe
            {
                for (int y = 0; y < bmd.Height; y++)
                {
                    byte* dataRow = (byte*)bmd.Scan0 + (y * bmd.Stride);

                    for (int x = 0; x < bmd.Width; x++)
                    {
                        int index = x * bpp;

                        yCbCr[0] = yBytes[yIter];
                        yCbCr[1] = cbBytes[chromaIter];
                        yCbCr[2] = crBytes[chromaIter];

                        ColorSpace.YCbCrToRgb(yCbCr, rgb);

                        dataRow[index + 2] = rgb[0];
                        dataRow[index + 1] = rgb[1];
                        dataRow[index + 0] = rgb[2];

                        yIter++;
                        if ((x + 1) % 4 == 0)
                            chromaIter++;
                    }
                }
            }

            bitmap.UnlockBits(bmd);

            return bitmap;
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
    }
}
