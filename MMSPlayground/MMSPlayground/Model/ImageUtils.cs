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
