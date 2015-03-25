using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MMSPlayground.Utils;

namespace MMSPlayground.Filters.Convolution
{
    public class Kernel
    {
        private int[][] m_coeffs;

        public int Factor { get; set; }
        public int Offset { get; set; }
        public int[][] Coeff
        {
            get
            {
                return m_coeffs;
            }
        }

        public Kernel(int[][] coeffs)
        {
            m_coeffs = coeffs;

            Factor = 1;
            Offset = 0;
        }

        public Kernel(int[][] coeffs, int factor, int offset)
        {
            m_coeffs = coeffs;

            Factor = factor;
            Offset = offset;
        }

        public int Convolve(int[][] pixelValues)
        {
            float sum = 0;

            for (int y = 0; y < pixelValues.Length; y++)
            {
                for (int x = 0; x < pixelValues[y].Length; x++)
                {
                    sum += m_coeffs[y][x] * pixelValues[y][x];
                }
            }

            return ImageUtils.Clamp((int)(sum / Factor) + Offset, 0, 255);
        }
    }
}
