using MMSPlayground.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace MMSPlayground.IO
{
    public enum DownsamplingMode
    {
        Y = 0,
        Cb = 1,
        Cr = 2
    }
    public class Downsampler
    {
        public Downsampler()
        {
        }

        public Bitmap DownsamplePreview(Bitmap srcBmp, DownsamplingMode downsamplingMode)
        {
            Bitmap bitmap = (Bitmap)srcBmp.Clone();

            Rectangle bmpRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmd = bitmap.LockBits(bmpRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);


            int bpp = ImageUtils.GetComponentsPerPixel(bmd);

            byte down1 = 0;
            byte down2 = 0;

            byte[] rgb = new byte[3];
            byte[] ycbcr = new byte[3];

            unsafe
            {
                for (int y = 0; y < bmd.Height; y++)
                {
                    byte* dataRow = (byte*)bmd.Scan0 + (y * bmd.Stride);

                    for (int x = 0; x < bmd.Width; x++)
                    {
                        int index = x * bpp;

                        rgb[0] = dataRow[index + 2];
                        rgb[1] = dataRow[index + 1];
                        rgb[2] = dataRow[index + 0];

                        ColorSpace.RgbToYCbCr(rgb, ycbcr);

                        DownsampleTransform(ycbcr, x, ref down1, ref down2, downsamplingMode);

                        ColorSpace.YCbCrToRgb(ycbcr, rgb);

                        dataRow[index + 2] = rgb[0];
                        dataRow[index + 1] = rgb[1];
                        dataRow[index + 0] = rgb[2];
                    }
                }
            }

            bitmap.UnlockBits(bmd);

            return bitmap;
        }

        private void DownsampleTransform(byte[] ycbcr, int x, ref byte down1, ref byte down2, DownsamplingMode mode)
        {
            switch(mode)
            {
                case DownsamplingMode.Y:
                    {
                        if (x % 4 == 0)
                        {
                            down1 = ycbcr[1];
                            down2 = ycbcr[2];
                        }

                        ycbcr[1] = down1;
                        ycbcr[2] = down2;
                        break;
                    }

                case DownsamplingMode.Cb:
                    {
                        if (x % 4 == 0)
                        {
                            down1 = ycbcr[0];
                            down2 = ycbcr[2];
                        }

                        ycbcr[0] = down1;
                        ycbcr[2] = down2;
                        break;
                    }

                case DownsamplingMode.Cr:
                    {
                        if (x % 4 == 0)
                        {
                            down1 = ycbcr[0];
                            down2 = ycbcr[1];
                        }

                        ycbcr[0] = down1;
                        ycbcr[1] = down2;
                        break;
                    }
            }
        }
    }
}
