using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using MMSPlayground.Model;
using System.Drawing.Imaging;

namespace MMSPlayground.Filters
{
    public class ContrastFilter : IFilter
    {
        private double m_coeff = 0;
        private Bitmap m_prevBitmap = null;

        private IImageModel m_model = null;

        public ContrastFilter(IImageModel model, double coeff)
        {
            m_coeff = coeff;

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

                            dataRow[index + 2] = Transform(dataRow[index + 2]);
                            dataRow[index + 1] = Transform(dataRow[index + 1]);
                            dataRow[index + 0] = Transform(dataRow[index + 0]);
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

                        int newR = Transform(currPixel.R);
                        int newG = Transform(currPixel.G);
                        int newB = Transform(currPixel.B);

                        Color newPixel = Color.FromArgb(newR, newG, newB);

                        bitmap.SetPixel(x, y, newPixel);
                    }
                }
            }

            m_model.SetBitmap(bitmap);
        }

        private byte Transform(byte val)
        {
            double valNorm = val / 255.0 - 0.5;
            valNorm *= m_coeff;
            valNorm += 0.5;
            valNorm *= 255;

            return (byte)ImageUtils.Clamp((int)valNorm, 0, 255);
        }

        public void Undo()
        {
            m_model.SetBitmap(m_prevBitmap);
        }

        public IFilter Clone()
        {
            ContrastFilter clone = new ContrastFilter(m_model, m_coeff);

            return clone;
        }

        public string FilterName
        {
            get
            {
                return "Contrast";
            }
        }
    }
}
