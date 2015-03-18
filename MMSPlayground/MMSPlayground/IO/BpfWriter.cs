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
            Bitmap[] bitmapChannels = new Bitmap[3];
            ImageUtils.ComputeYCbCr(bitmap, ref bitmapChannels);


            Rectangle bmpRect = new Rectangle(0, 0, bitmapChannels[0].Width, bitmapChannels[0].Height);
            BitmapData bmdY = bitmapChannels[0].LockBits(bmpRect, ImageLockMode.ReadWrite, bitmapChannels[0].PixelFormat);
            BitmapData bmdCb = bitmapChannels[1].LockBits(bmpRect, ImageLockMode.ReadWrite, bitmapChannels[1].PixelFormat);
            BitmapData bmdCr = bitmapChannels[2].LockBits(bmpRect, ImageLockMode.ReadWrite, bitmapChannels[2].PixelFormat);

            IList<byte> yChannel = new List<byte>();

            unsafe
            {
                for (int y = 0; y < bmdY.Height; y++)
                {
                    byte* YRow = (byte*)bmdY.Scan0 + (y * bmdY.Stride);

                    for (int x = 0; x < bmdY.Width; x++)
                    {
                        int index = x * 3;

                        yChannel.Add(YRow[index]);
                    }
                }
            }

            byte[] yBytes = yChannel.ToArray();

            using (FileStream file = File.Create(fileName))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(BitConverter.GetBytes(bmdY.Width), 0, sizeof(int));
                memoryStream.Write(BitConverter.GetBytes(bmdY.Height), 0, sizeof(int));
                memoryStream.Write(BitConverter.GetBytes(bmdY.Stride), 0, sizeof(int));
                memoryStream.Write(yBytes, 0, yBytes.Length);
                memoryStream.WriteTo(file);
            }

            bitmapChannels[0].UnlockBits(bmdY);
            bitmapChannels[1].UnlockBits(bmdCb);
            bitmapChannels[2].UnlockBits(bmdCr);
        }
    }
}
