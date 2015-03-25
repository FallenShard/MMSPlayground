using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using MMSPlayground.Utils;

namespace MMSPlayground.IO
{
    public class BpfReader
    {
        private readonly int WidthOffset = 0;
        private readonly int HeightOffset = 4;
        private readonly int StrideOffset = 8;

        public BpfReader()
        {

        }

        public Bitmap ReadFromFile(string fileName)
        {
            byte[] buff;

            using (FileStream file = File.Open(fileName, FileMode.Open))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                buff = memoryStream.ToArray();
            }

            int width = BitConverter.ToInt32(buff, WidthOffset);
            int height = BitConverter.ToInt32(buff, HeightOffset);
            int stride = BitConverter.ToInt32(buff, StrideOffset);

            IList<byte> yData = new List<byte>();
            IList<byte> cbData = new List<byte>();
            IList<byte> crData = new List<byte>();

            int iter = 12;
            int end = iter + width * height;

            while (iter < end)
            {
                yData.Add(buff[iter++]);
            }

            int step = width * height / 4;
            end += step;
            while (iter < end)
            {
                cbData.Add(buff[iter]);
                crData.Add(buff[iter + step]);
                iter++;
            }

            byte[] yBytes = yData.ToArray();
            byte[] cbBytes = cbData.ToArray();
            byte[] crBytes = crData.ToArray();

            int memPadding = width * 3 % 4;
            int rowDataLength = memPadding == 0 ? width * 3 : width * 3 + 4 - memPadding;
            byte[] mainData = new byte[height * rowDataLength];

            byte[] yCbCr = new byte[3];
            byte[] rgb = new byte[3];


            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            int chromaIter = 0;
            for (int i = 0; i < yBytes.Length; i++)
            {
                yCbCr[0] = yBytes[i];
                yCbCr[1] = cbBytes[chromaIter];
                yCbCr[2] = crBytes[chromaIter];

                ColorSpace.YCbCrToRgb(yCbCr, rgb);

                mainData[3 * i + 2] = rgb[0];
                mainData[3 * i + 1] = rgb[1];
                mainData[3 * i + 0] = rgb[2];

                if ((i + 1) % 4 == 0)
                    chromaIter++;
            }

            IntPtr unmanagedPointer = Marshal.AllocHGlobal(mainData.Length);
            Marshal.Copy(mainData, 0, unmanagedPointer, mainData.Length);

            //Bitmap bitmap = new Bitmap(width, height, stride, PixelFormat.Format24bppRgb, unmanagedPointer);

            return bitmap;
        }

        private static PixelFormat GetPixelFormat(int width, int stride)
        {
            return PixelFormat.Format24bppRgb;
        }
    }
}
