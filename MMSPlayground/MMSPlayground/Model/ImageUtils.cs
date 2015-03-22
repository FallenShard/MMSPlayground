using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace MMSPlayground.Model
{
    public class ImageUtils
    {
        public enum PaddingMode
        {
            Zero,
            Halftone
        }

        public static void RgbToYCbCr(byte[] rgb, byte[] ycbcr)
        {
            ycbcr[0] = (byte)(0.299 * (float)rgb[0] + 0.587 * (float)rgb[1] + 0.114 * (float)rgb[2]);
            ycbcr[1] = (byte)(128 + (byte)(-0.169 * (float)rgb[0] - 0.331 * (float)rgb[1] + 0.5 * (float)rgb[2]));
            ycbcr[2] = (byte)(128 + (byte)(0.5 * (float)rgb[0] - 0.419 * (float)rgb[1] - 0.081 * (float)rgb[2]));
        }

        public static void YCbCrToRgb(byte[] ycbcr, byte[] rgb)
        {
            rgb[0] = (byte)Clamp((int)(ycbcr[0] + 1.4 * (float)(ycbcr[2] - 128)), 0, 255);
            rgb[1] = (byte)Clamp((int)(ycbcr[0] - 0.343 * (float)(ycbcr[1] - 128) - 0.711 * (float)(ycbcr[2] - 128)), 0, 255);
            rgb[2] = (byte)Clamp((int)(ycbcr[0] + 1.765 * (float)(ycbcr[1] - 128)), 0, 255);
        }

        public static void ComputeYCbCr(Bitmap bitmap, ref Bitmap[] channels, ref Histogram[] histograms)
        {
            IList<int>[] histData = new IList<int>[3];
            histData[0] = new List<int>(256);
            histData[1] = new List<int>(256);
            histData[2] = new List<int>(256);

            for (int i = 0; i < 256; i++)
            {
                histData[0].Add(0);
                histData[1].Add(0);
                histData[2].Add(0);
            }

            Rectangle bmpRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmd = bitmap.LockBits(bmpRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            BitmapData bmdY  = channels[0].LockBits(bmpRect, ImageLockMode.ReadWrite, channels[0].PixelFormat);
            BitmapData bmdCb = channels[1].LockBits(bmpRect, ImageLockMode.ReadWrite, channels[1].PixelFormat);
            BitmapData bmdCr = channels[2].LockBits(bmpRect, ImageLockMode.ReadWrite, channels[2].PixelFormat);

            int bpp = GetComponentsPerPixel(bmd);

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

                        histData[0][yCbCr[0]]++;
                        histData[1][yCbCr[1]]++;
                        histData[2][yCbCr[2]]++;

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

            bitmap.UnlockBits(bmd);
            channels[0].UnlockBits(bmdY);
            channels[1].UnlockBits(bmdCb);
            channels[2].UnlockBits(bmdCr);

            histograms[0].Data = histData[0];
            histograms[1].Data = histData[1];
            histograms[2].Data = histData[2];
        }

        public static void ComputeYCbCr(Bitmap bitmap, ref Bitmap yCbCrBmp)
        {
            yCbCrBmp = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);

            Rectangle bmpRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmd = bitmap.LockBits(bmpRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            BitmapData bmdComputed = yCbCrBmp.LockBits(bmpRect, ImageLockMode.ReadWrite, yCbCrBmp.PixelFormat);

            int bpp = GetComponentsPerPixel(bmd);

            byte[] rgb = new byte[3];
            byte[] yCbCr = new byte[3];

            unsafe
            {
                for (int y = 0; y < bmd.Height; y++)
                {
                    byte* dataRow = (byte*)bmd.Scan0 + (y * bmd.Stride);
                    byte* compRow = (byte*)bmdComputed.Scan0 + (y * bmdComputed.Stride);

                    for (int x = 0; x < bmd.Width; x++)
                    {
                        int index = x * bpp;
                        rgb[0] = dataRow[index + 2];
                        rgb[1] = dataRow[index + 1];
                        rgb[2] = dataRow[index + 0];

                        ImageUtils.RgbToYCbCr(rgb, yCbCr);

                        compRow[index + 0] = yCbCr[0];
                        compRow[index + 1] = yCbCr[1];
                        compRow[index + 2] = yCbCr[2];
                    }
                }
            }

            bitmap.UnlockBits(bmd);
            yCbCrBmp.UnlockBits(bmdComputed);
        }

        public static int GetComponentsPerPixel(Bitmap bitmap)
        {
            return Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
        }

        public static int GetComponentsPerPixel(BitmapData bitmapData)
        {
            return Image.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value > max)
                return max;

            if (value < min)
                return min;

            return value;
        }

        public unsafe static void ExtractNeighbourhood(BitmapData data, int bpp, int x, int y, int kernelSize, int[][][] neighbourhood)
        {
            int halfSize = kernelSize / 2;
            for (int row = -halfSize; row <= halfSize; row++)
            {
                byte* dataRow = (byte*)data.Scan0 + ((y + row) * data.Stride);

                for (int col = -halfSize; col <= halfSize; col++)
                {
                    int index = (x + col) * bpp;

                    neighbourhood[0][row + halfSize][col + halfSize] = dataRow[index + 0];
                    neighbourhood[1][row + halfSize][col + halfSize] = dataRow[index + 1];
                    neighbourhood[2][row + halfSize][col + halfSize] = dataRow[index + 2];
                }
            }
        }

        public static void ExtractNeighbourhood(Bitmap bmp, int x, int y, int kernelSize, int[][][] neighbourhood)
        {
            int halfSize = kernelSize / 2;
            for (int row = -halfSize; row <= halfSize; row++)
            {
                for (int col = -halfSize; col <= halfSize; col++)
                {
                    neighbourhood[0][row + halfSize][col + halfSize] = bmp.GetPixel(x + col, y + row).B;
                    neighbourhood[1][row + halfSize][col + halfSize] = bmp.GetPixel(x + col, y + row).G;
                    neighbourhood[2][row + halfSize][col + halfSize] = bmp.GetPixel(x + col, y + row).R;
                }
            }
        }

        public static Bitmap PadBitmap(Bitmap orig, int paddingSize, PaddingMode paddingMode)
        {
            switch (paddingMode)
            {
                case PaddingMode.Zero:
                    return PadWithZeros(orig, paddingSize);

                case PaddingMode.Halftone:
                    return PadWithHalftone(orig, paddingSize);

                default:
                    return null;
            }
        }

        private static Bitmap PadWithZeros(Bitmap orig, int size)
        {
            Bitmap paddedBitmap = new Bitmap(orig.Width + 2 * size, orig.Height + 2 * size, orig.PixelFormat);

            Graphics g = Graphics.FromImage(paddedBitmap);

            g.DrawImage(orig, size, size, orig.Width, orig.Height);

            g.Dispose();

            return paddedBitmap;
        }

        private static Bitmap PadWithHalftone(Bitmap orig, int size)
        {
            Bitmap paddedBitmap = new Bitmap(orig.Width + 2 * size, orig.Height + 2 * size, orig.PixelFormat);
            ClearBorder(paddedBitmap, Color.FromArgb(127, 127, 127), size);            

            Graphics g = Graphics.FromImage(paddedBitmap);

            g.DrawImage(orig, size, size, orig.Width, orig.Height);

            g.Dispose();

            return paddedBitmap;
        }

        public static unsafe void ClearBitmap(Bitmap bmp, Color color)
        {
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            int bpp = GetComponentsPerPixel(bmd);

            for (int y = 0; y < bmd.Height; y++)
            {
                byte* dataPtr = (byte*)(bmd.Scan0 + y * bmd.Stride);
                for (int x = 0; x < bmd.Width; x++)
                {
                    int index = x * bpp;

                    dataPtr[index + 0] = color.B;
                    dataPtr[index + 1] = color.G;
                    dataPtr[index + 2] = color.R;
                }
            }

            bmp.UnlockBits(bmd);
        }

        public static unsafe void ClearBorder(Bitmap bmp, Color color, int borderSize)
        {
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            int bpp = GetComponentsPerPixel(bmd);

            for (int y = 0; y < bmd.Height; y++)
            {
                byte* dataPtr = (byte*)(bmd.Scan0 + y * bmd.Stride);
                for (int x = 0; x < bmd.Width; x++)
                {
                    if (x >= borderSize && x < bmd.Width - borderSize && y >= borderSize && y < bmd.Height - borderSize)
                        continue;
                    int index = x * bpp;

                    dataPtr[index + 0] = color.B;
                    dataPtr[index + 1] = color.G;
                    dataPtr[index + 2] = color.R;
                }
            }

            bmp.UnlockBits(bmd);
        }
    }
}
