using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

using MMSPlayground.Model;

namespace MMSPlayground.Filters
{
    public class BrightnessFilter : IFilter
    {
        private int m_bias = 0;
        private Bitmap m_prevBitmap = null;

        private IImageModel m_model = null;

        public BrightnessFilter(IImageModel model, int bias)
        {
            m_bias = bias;

            m_model = model;
        }

        public void Apply()
        {
            Bitmap bitmap = m_model.GetBitmap();
            m_prevBitmap = (Bitmap)bitmap.Clone();

            if (m_model.GetWin32CoreUsageMode())
            {
                Rectangle bmpRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bmd = bitmap.LockBits(bmpRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

                int bpp = ImageUtils.GetComponentsPerPixel(bmd);

                unsafe
                {
                    for (int y = 0; y < bmd.Height; y++)
                    {
                        byte* dataRow = (byte*)bmd.Scan0 + (y * bmd.Stride);

                        for (int x = 0; x < bmd.Width; x++)
                        {
                            int index = x * bpp;

                            int newR = ImageUtils.Clamp(dataRow[index + 2] + m_bias, 0, 255);
                            int newG = ImageUtils.Clamp(dataRow[index + 1] + m_bias, 0, 255);
                            int newB = ImageUtils.Clamp(dataRow[index + 0] + m_bias, 0, 255);

                            dataRow[index + 2] = (byte)newR;
                            dataRow[index + 1] = (byte)newG;
                            dataRow[index + 0] = (byte)newB;
                        }
                    }
                }

                bitmap.UnlockBits(bmd);
            }
            else
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color currPixel = bitmap.GetPixel(x, y);

                        int newR = ImageUtils.Clamp(currPixel.R + m_bias, 0, 255);
                        int newG = ImageUtils.Clamp(currPixel.G + m_bias, 0, 255);
                        int newB = ImageUtils.Clamp(currPixel.B + m_bias, 0, 255);

                        Color newPixel = Color.FromArgb(newR, newG, newB);

                        bitmap.SetPixel(x, y, newPixel);
                    }
                }
            }

            m_model.SetBitmap(bitmap);
        }

        public void Undo()
        {
            m_model.SetBitmap(m_prevBitmap);
        }

        public IFilter Clone()
        {
            BrightnessFilter clone = new BrightnessFilter(m_model, m_bias);

            return clone;
        }

        public string FilterName
        {
            get
            {
                return "Brightness";
            }
        }
    }
}
