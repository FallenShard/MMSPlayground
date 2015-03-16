using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;

using MMSPlayground.Model;
using MMSPlayground.Filters.Convolution;
using System.Drawing;


namespace MMSPlayground.Filters
{
    public class EdgeEnhancementFilter : ConvolutionFilter
    {
        private int m_threshold = 0;

        public EdgeEnhancementFilter(IImageModel model, int threshold)
        {
            m_kernelSize = 3;
            m_threshold = threshold;

            m_model = model;
        }

        public override IFilter Clone()
        {
            EdgeEnhancementFilter clone = new EdgeEnhancementFilter(m_model, m_threshold);

            return clone;
        }

        protected override Kernel GetConvolutionKernel(int kernelSize)
        {
            return null;
        }

        public override string FilterName
        {
            get
            {
                return "Edge Enhancement";
            }
        }

        protected override unsafe void TransformUnsafe(BitmapData bmd, BitmapData paddedBmd, int bpp, Kernel kernel, int[][][] neighbourhood, int paddingSize)
        {
            int[][] grayNb = new int[m_kernelSize][];
            for (int i = 0; i < grayNb.Length; i++)
            {
                grayNb[i] = new int[m_kernelSize];
            }


            for (int y = 0; y < bmd.Height; y++)
            {
                byte* dataRow = (byte*)bmd.Scan0 + (y * bmd.Stride);

                for (int x = 0; x < bmd.Width; x++)
                {
                    int index = x * bpp;

                    ImageUtils.ExtractNeighbourhood(paddedBmd, bpp, x + paddingSize, y + paddingSize, m_kernelSize, neighbourhood);

                    for (int row = 0; row < m_kernelSize; row++)
                        for (int col = 0; col < m_kernelSize; col++)
                        {
                            grayNb[row][col] = (byte)((0.299 * (float)neighbourhood[2][row][col] + 0.587 * (float)neighbourhood[1][row][col] + 0.114 * (float)neighbourhood[0][row][col]));
                        }

                    int diff1 = Math.Abs(grayNb[0][0] - grayNb[2][2]);
                    int diff2 = Math.Abs(grayNb[0][1] - grayNb[2][1]);
                    int diff3 = Math.Abs(grayNb[0][2] - grayNb[2][0]);
                    int diff4 = Math.Abs(grayNb[1][0] - grayNb[1][2]);

                    int max1 = Math.Max(diff1, diff2);
                    int max2 = Math.Max(diff3, diff4);
                    int max3 = Math.Max(max1, max2);

                    int finalVal = grayNb[1][1];
                    if (max3 > m_threshold && max3 > grayNb[1][1])
                    {
                        finalVal = max3;
                    }

                    dataRow[index + 2] = (byte)ImageUtils.Clamp(finalVal, 0, 255);
                    dataRow[index + 1] = (byte)ImageUtils.Clamp(finalVal, 0, 255);
                    dataRow[index + 0] = (byte)ImageUtils.Clamp(finalVal, 0, 255);
                }
            }
        }

        protected override void TransformSafe(Bitmap bmp, Bitmap paddedBmp, Kernel kernel, int[][][] neighbourhood, int paddingSize)
        {
            int[][] grayNb = new int[m_kernelSize][];
            for (int i = 0; i < grayNb.Length; i++)
            {
                grayNb[i] = new int[m_kernelSize];
            }

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    ImageUtils.ExtractNeighbourhood(paddedBmp, x + paddingSize, y + paddingSize, m_kernelSize, neighbourhood);

                    for (int row = 0; row < m_kernelSize; row++)
                        for (int col = 0; col < m_kernelSize; col++)
                        {
                            grayNb[row][col] = (byte)((0.299 * (float)neighbourhood[2][row][col] + 0.587 * (float)neighbourhood[1][row][col] + 0.114 * (float)neighbourhood[0][row][col]));
                        }

                    int diff1 = Math.Abs(grayNb[0][0] - grayNb[2][2]);
                    int diff2 = Math.Abs(grayNb[0][1] - grayNb[2][1]);
                    int diff3 = Math.Abs(grayNb[0][2] - grayNb[2][0]);
                    int diff4 = Math.Abs(grayNb[1][0] - grayNb[1][2]);

                    int max1 = Math.Max(diff1, diff2);
                    int max2 = Math.Max(diff3, diff4);
                    int max3 = Math.Max(max1, max2);

                    int finalVal = grayNb[1][1];
                    if (max3 > m_threshold && max3 > grayNb[1][1])
                    {
                        finalVal = max3;
                    }

                    bmp.SetPixel(x, y, Color.FromArgb(finalVal, finalVal, finalVal));
                }
            }
        }
    }
}
