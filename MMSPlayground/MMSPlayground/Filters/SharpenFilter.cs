using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

using MMSPlayground.Model;
using MMSPlayground.Filters.Convolution;

namespace MMSPlayground.Filters
{
    public class SharpenFilter : ConvolutionFilter
    {
        private int m_baseFactor = 0;

        public SharpenFilter(IImageModel model, int kernelSize, int baseFactor)
        {
            m_kernelSize = kernelSize;
            m_baseFactor = baseFactor;

            m_model = model;
        }

        public override IFilter Clone()
        {
            SharpenFilter clone = new SharpenFilter(m_model, m_kernelSize, m_baseFactor);

            return clone;
        }

        protected override Kernel GetConvolutionKernel(int kernelSize)
        {
            int n = m_baseFactor;
            int[,] ker = new int[kernelSize, kernelSize];

            if (kernelSize == 3)
            {
                ker[0, 0] = +0; ker[0, 1] = -2; ker[0, 2] = +0;
                ker[1, 0] = -2; ker[1, 1] = +n; ker[1, 2] = -2;
                ker[2, 0] = +0; ker[2, 1] = -2; ker[2, 2] = +0;
            }
            else if (kernelSize == 5)
            {
                ker[0, 0] = +0; ker[0, 1] = +0; ker[0, 2] = -2; ker[0, 3] = -2; ker[0, 4] = +0;
                ker[1, 0] = -2; ker[1, 1] = +0; ker[1, 2] = -2; ker[1, 3] = +0; ker[1, 4] = +0;
                ker[2, 0] = -2; ker[2, 1] = -2; ker[2, 2] = +n; ker[2, 3] = -2; ker[2, 4] = -2;
                ker[3, 0] = +0; ker[3, 1] = +0; ker[3, 2] = -2; ker[3, 3] = +0; ker[3, 4] = -2;
                ker[4, 0] = +0; ker[4, 1] = -2; ker[4, 2] = -2; ker[4, 3] = +0; ker[4, 4] = +0;
            }
            else if (kernelSize == 7)
            {
                ker[0, 0] = +0; ker[0, 1] = +0; ker[0, 2] = +0; ker[0, 3] = -2; ker[0, 4] = -2; ker[0, 5] = +0; ker[0, 6] = +0;
                ker[1, 0] = +0; ker[1, 1] = +0; ker[1, 2] = +0; ker[1, 3] = -2; ker[1, 4] = -2; ker[1, 5] = +0; ker[1, 6] = +0;
                ker[2, 0] = -2; ker[2, 1] = -2; ker[2, 2] = +0; ker[2, 3] = -2; ker[2, 4] = +0; ker[2, 5] = +0; ker[2, 6] = +0;
                ker[3, 0] = -2; ker[3, 1] = -2; ker[3, 2] = -2; ker[3, 3] = +n; ker[3, 4] = -2; ker[3, 5] = -2; ker[3, 6] = -2;
                ker[4, 0] = +0; ker[4, 1] = +0; ker[4, 2] = +0; ker[4, 3] = -2; ker[4, 4] = +0; ker[4, 5] = -2; ker[4, 6] = -2;
                ker[5, 0] = +0; ker[5, 1] = +0; ker[5, 2] = -2; ker[5, 3] = -2; ker[5, 4] = +0; ker[5, 5] = +0; ker[5, 6] = +0;
                ker[6, 0] = +0; ker[6, 1] = +0; ker[6, 2] = -2; ker[6, 3] = -2; ker[6, 4] = +0; ker[6, 5] = +0; ker[6, 6] = +0;
            }

            return new Kernel(ker, n - 8, 0);
        }
    }
}
