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
            Zero
        }

        public static void RgbToYCbCr(byte[] rgb, byte[] ycbcr)
        {
            ycbcr[0] = (byte)((0.299 * (float)rgb[0] + 0.587 * (float)rgb[1] + 0.114 * (float)rgb[2]));
            ycbcr[1] = (byte)(128 + (byte)((-0.16874 * (float)rgb[0] - 0.33126 * (float)rgb[1] + 0.5 * (float)rgb[2])));
            ycbcr[2] = (byte)(128 + (byte)((0.5 * (float)rgb[0] - 0.41869 * (float)rgb[1] - 0.08131 * (float)rgb[2])));
        }

        public static void ComputeYCbCr(Bitmap bitmap, ref Bitmap[] channels, ref IList<int>[] histograms)
        {
            channels[0] = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
            channels[1] = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
            channels[2] = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);

            histograms[0] = new List<int>(256);
            histograms[1] = new List<int>(256);
            histograms[2] = new List<int>(256);

            for (int i = 0; i < 256; i++)
            {
                histograms[0].Add(0);
                histograms[1].Add(0);
                histograms[2].Add(0);
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

                        histograms[0][yCbCr[0]]++;
                        histograms[1][yCbCr[1]]++;
                        histograms[2][yCbCr[2]]++;

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
        }

        public static void ComputeYCbCr(Bitmap bitmap, ref Bitmap[] channels)
        {
            channels[0] = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
            channels[1] = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
            channels[2] = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);

            Rectangle bmpRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmd = bitmap.LockBits(bmpRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            BitmapData bmdY = channels[0].LockBits(bmpRect, ImageLockMode.ReadWrite, channels[0].PixelFormat);
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
                    byte* YRow = (byte*)bmdY.Scan0 + (y * bmdY.Stride);
                    byte* CbRow = (byte*)bmdCb.Scan0 + (y * bmdCb.Stride);
                    byte* CrRow = (byte*)bmdCr.Scan0 + (y * bmdCr.Stride);

                    for (int x = 0; x < bmd.Width; x++)
                    {
                        int index = x * bpp;
                        rgb[0] = enhRow[index + 2];
                        rgb[1] = enhRow[index + 1];
                        rgb[2] = enhRow[index + 0];

                        ImageUtils.RgbToYCbCr(rgb, yCbCr);

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
            return Math.Max(min, Math.Min(value, max));
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
    }
}
