using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace MMSPlayground.Utils
{
    public class ImageUtils
    {
        public enum PaddingMode
        {
            Zero,
            Halftone
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value > max)
                return max;

            if (value < min)
                return min;

            return value;
        }

        public static int GetComponentsPerPixel(Bitmap bitmap)
        {
            return Image.GetPixelFormatSize(bitmap.PixelFormat) >> 3;
        }

        public static int GetComponentsPerPixel(BitmapData bitmapData)
        {
            return Image.GetPixelFormatSize(bitmapData.PixelFormat) >> 3;
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
