using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

using MMSPlayground.Filters.Convolution;
using MMSPlayground.Model;
using MMSPlayground.Utils;

namespace MMSPlayground.Filters
{
    public abstract class ConvolutionFilter : IFilter
    {
        protected int m_kernelSize = 0;

        protected Bitmap m_prevBitmap = null;
        protected IImageModel m_model = null;

        public void Apply()
        {
            m_prevBitmap = (Bitmap)m_model.GetBitmap().Clone();

            m_model.SetBitmap(GetRawResults());
        }

        public Bitmap GetRawResults()
        {
            Bitmap bitmap = (Bitmap)m_model.GetBitmap().Clone();

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
                int[][][] neighbourhood = AllocateNeighbourhood();

                TransformUnsafe(bmd, paddedBmd, bpp, kernel, neighbourhood, paddingSize);

                bitmap.UnlockBits(bmd);
                paddedBitmap.UnlockBits(paddedBmd);
            }
            else
            {
                Kernel kernel = GetConvolutionKernel(m_kernelSize);
                int[][][] neighbourhood = AllocateNeighbourhood();

                TransformSafe(bitmap, paddedBitmap, kernel, neighbourhood, paddingSize);
            }

            return bitmap;
        }

        public void Undo()
        {
            m_model.SetBitmap(m_prevBitmap);
        }

        private int[][][] AllocateNeighbourhood()
        {
            int[][][] neighbourhood = new int[3][][];

            for (int i = 0; i < 3; i++)
            {
                neighbourhood[i] = new int[m_kernelSize][];
                for (int j = 0; j < m_kernelSize; j++)
                    neighbourhood[i][j] = new int[m_kernelSize];
            }

            return neighbourhood;
        }


        public abstract IFilter Clone();

        protected abstract Kernel GetConvolutionKernel(int kernelSize);

        public abstract string FilterName
        {
            get;
        }

        protected unsafe virtual void TransformUnsafe(BitmapData bmd, BitmapData paddedBmd, int bpp, Kernel kernel, int[][][] neighbourhood, int paddingSize)
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

        protected virtual void TransformSafe(Bitmap bmp, Bitmap paddedBmp, Kernel kernel, int[][][] neighbourhood, int paddingSize)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    ImageUtils.ExtractNeighbourhood(paddedBmp, x + paddingSize, y + paddingSize, m_kernelSize, neighbourhood);

                    int newR = (byte)kernel.Convolve(neighbourhood[2]);
                    int newG = (byte)kernel.Convolve(neighbourhood[1]);
                    int newB = (byte)kernel.Convolve(neighbourhood[0]);

                    bmp.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }
        }
    }
}
