using MMSPlayground.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace MMSPlayground.Utils
{
    public class ColorSpace
    {
        public static void RgbToYCbCr(byte[] rgb, byte[] ycbcr)
        {
            ycbcr[0] = (byte)(0.299 * (float)rgb[0] + 0.587 * (float)rgb[1] + 0.114 * (float)rgb[2]);
            ycbcr[1] = (byte)(128 + (byte)(-0.169 * (float)rgb[0] - 0.331 * (float)rgb[1] + 0.5 * (float)rgb[2]));
            ycbcr[2] = (byte)(128 + (byte)(0.5 * (float)rgb[0] - 0.419 * (float)rgb[1] - 0.081 * (float)rgb[2]));
        }

        public static void YCbCrToRgb(byte[] ycbcr, byte[] rgb)
        {
            rgb[0] = (byte)ImageUtils.Clamp((int)(ycbcr[0] + 1.4 * (float)(ycbcr[2] - 128)), 0, 255);
            rgb[1] = (byte)ImageUtils.Clamp((int)(ycbcr[0] - 0.343 * (float)(ycbcr[1] - 128) - 0.711 * (float)(ycbcr[2] - 128)), 0, 255);
            rgb[2] = (byte)ImageUtils.Clamp((int)(ycbcr[0] + 1.765 * (float)(ycbcr[1] - 128)), 0, 255);
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
            BitmapData bmdY = channels[0].LockBits(bmpRect, ImageLockMode.ReadWrite, channels[0].PixelFormat);
            BitmapData bmdCb = channels[1].LockBits(bmpRect, ImageLockMode.ReadWrite, channels[1].PixelFormat);
            BitmapData bmdCr = channels[2].LockBits(bmpRect, ImageLockMode.ReadWrite, channels[2].PixelFormat);

            int bpp = ImageUtils.GetComponentsPerPixel(bmd);

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

                        RgbToYCbCr(rgb, yCbCr);

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

            int bpp = ImageUtils.GetComponentsPerPixel(bmd);

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

                        RgbToYCbCr(rgb, yCbCr);

                        compRow[index + 0] = yCbCr[0];
                        compRow[index + 1] = yCbCr[1];
                        compRow[index + 2] = yCbCr[2];
                    }
                }
            }

            bitmap.UnlockBits(bmd);
            yCbCrBmp.UnlockBits(bmdComputed);
        }
    }
}
