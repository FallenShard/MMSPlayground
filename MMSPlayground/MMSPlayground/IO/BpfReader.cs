using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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

            IList<byte> pixelData = new List<byte>();

            for (int i = 8; i < buff.Length; i++)
            {
                pixelData.Add(buff[i]);
                pixelData.Add(150);
                pixelData.Add(175);
            }

            byte[] pixelBytes = pixelData.ToArray();

            IntPtr unmanagedPointer = Marshal.AllocHGlobal(pixelBytes.Length);
            Marshal.Copy(pixelBytes, 0, unmanagedPointer, pixelBytes.Length);

            Bitmap bitmap = new Bitmap(width, height, stride, PixelFormat.Format24bppRgb, unmanagedPointer);

            return bitmap;
        }
    }
}
