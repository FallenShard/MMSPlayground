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
    }
}
