using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

using MMSPlayground.Filters.Convolution;
using MMSPlayground.Model;

namespace MMSPlayground.Filters
{
    public abstract class ConvolutionFilter : IFilter
    {
        protected int m_kernelSize = 0;

        protected Bitmap m_prevBitmap = null;
        protected IImageModel m_model = null;

        public void Apply()
        {
            Bitmap bitmap = m_model.GetBitmap();
            m_prevBitmap = (Bitmap)bitmap.Clone();

            int paddingSize = m_kernelSize / 2;
            Bitmap paddedBitmap = ImageUtils.PadBitmap(bitmap, paddingSize, ImageUtils.PaddingMode.Zero);

            if (m_model.GetWin32CoreUsageMode())
            {
                Rectangle paddedBmpRect = new Rectangle(0, 0, paddedBitmap.Width, paddedBitmap.Height);
                BitmapData paddedBmd = paddedBitmap.LockBits(paddedBmpRect, ImageLockMode.ReadWrite, paddedBitmap.PixelFormat);

                Rectangle bmpRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bmd = bitmap.LockBits(bmpRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

                int bpp = ImageUtils.GetComponentsPerPixel(bmd);
                Kernel kernel = GetConvolutionKernel(m_kernelSize);
                int[][,] neighbourhood = AllocateNeighbourhood();

                TransformUnsafe(bmd, paddedBmd, bpp, kernel, neighbourhood, paddingSize);

                bitmap.UnlockBits(bmd);
                paddedBitmap.UnlockBits(paddedBmd);
            }
            else
            {
                //for (int y = 0; y < bitmap.Height; y++)
                //{
                //    for (int x = 0; x < bitmap.Width; x++)
                //    {
                //        Color currPixel = bitmap.GetPixel(x, y);

                //        int newR = ImageUtils.Clamp(currPixel.R + m_bias, 0, 255);
                //        int newG = ImageUtils.Clamp(currPixel.G + m_bias, 0, 255);
                //        int newB = ImageUtils.Clamp(currPixel.B + m_bias, 0, 255);

                //        Color newPixel = Color.FromArgb(newR, newG, newB);

                //        bitmap.SetPixel(x, y, newPixel);
                //    }
                //}
            }

            m_model.SetBitmap(bitmap);
        }

        public void Undo()
        {
            m_model.SetBitmap(m_prevBitmap);
        }

        private int[][,] AllocateNeighbourhood()
        {
            int[][,] neighbourhood = new int[3][,];

            for (int i = 0; i < 3; i++)
                neighbourhood[i] = new int[m_kernelSize, m_kernelSize];

            return neighbourhood;
        }


        public abstract IFilter Clone();

        protected abstract Kernel GetConvolutionKernel(int kernelSize);

        protected unsafe virtual void TransformUnsafe(BitmapData bmd, BitmapData paddedBmd, int bpp, Kernel kernel, int[][,] neighbourhood, int paddingSize)
        {
            for (int y = 0; y < bmd.Height; y++)
            {
                byte* dataRow = (byte*)bmd.Scan0 + (y * bmd.Stride);

                for (int x = 0; x < bmd.Width; x++)
                {
                    int index = x * bpp;

                    ImageUtils.ExtractNeighbourhood(paddedBmd, bpp, x + paddingSize, y + paddingSize, m_kernelSize, neighbourhood);

                    dataRow[index + 2] = (byte)kernel.Convolve(neighbourhood[2]);
                    dataRow[index + 1] = (byte)kernel.Convolve(neighbourhood[1]);
                    dataRow[index + 0] = (byte)kernel.Convolve(neighbourhood[0]);
                }
            }
        }

        public abstract string FilterName
        {
             get;
        }
    }
}
