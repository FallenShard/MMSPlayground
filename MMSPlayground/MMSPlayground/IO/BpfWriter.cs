using MMSPlayground.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace MMSPlayground.IO
{
    public class BpfWriter
    {
        public BpfWriter()
        {
        }

        public void SaveToFile(Bitmap bitmap, string fileName)
        {
            Bitmap yCbCrBmp = null;
            ImageUtils.ComputeYCbCr(bitmap, ref yCbCrBmp);


            Rectangle bmpRect = new Rectangle(0, 0, yCbCrBmp.Width, yCbCrBmp.Height);
            BitmapData bmdYCbCr = yCbCrBmp.LockBits(bmpRect, ImageLockMode.ReadWrite, yCbCrBmp.PixelFormat);

            IList<byte> yChannel = new List<byte>();
            IList<byte> cbChannel = new List<byte>();
            IList<byte> crChannel = new List<byte>();

            unsafe
            {
                for (int y = 0; y < bmdYCbCr.Height; y++)
                {
                    byte* dataRow = (byte*)bmdYCbCr.Scan0 + (y * bmdYCbCr.Stride);

                    for (int x = 0; x < bmdYCbCr.Width; x++)
                    {
                        int index = x * 3;

                        yChannel.Add(dataRow[index + 0]);
                        if (x % 4 == 0)
                        {
                            cbChannel.Add(dataRow[index + 1]);
                            crChannel.Add(dataRow[index + 2]);
                        }
                    }
                }
            }

            byte[] yBytes = yChannel.ToArray();
            byte[] cbBytes = cbChannel.ToArray();
            byte[] crBytes = crChannel.ToArray();

            using (FileStream file = File.Create(fileName))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(BitConverter.GetBytes(bmdYCbCr.Width), 0, sizeof(int));
                memoryStream.Write(BitConverter.GetBytes(bmdYCbCr.Height), 0, sizeof(int));
                memoryStream.Write(BitConverter.GetBytes(bmdYCbCr.Stride), 0, sizeof(int));
                memoryStream.Write(yBytes, 0, yBytes.Length);
                memoryStream.Write(cbBytes, 0, cbBytes.Length);
                memoryStream.Write(crBytes, 0, crBytes.Length);

                memoryStream.WriteTo(file);
            }

            yCbCrBmp.UnlockBits(bmdYCbCr);
        }
    }
}
