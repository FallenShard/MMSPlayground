using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMSPlayground
{
    public partial class ChannelForm : Form
    {
        public ChannelForm()
        {
            InitializeComponent();
        }

        public void SetImage(Bitmap bitmap)
        {
            Bitmap redBmp   = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppRgb);
            Bitmap greenBmp = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppRgb);
            Bitmap blueBmp  = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppRgb);

            Rectangle bmpRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmdFull  = bitmap.LockBits(bmpRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            BitmapData bmdRed   = redBmp.LockBits(bmpRect, ImageLockMode.ReadWrite, redBmp.PixelFormat);
            BitmapData bmdGreen = greenBmp.LockBits(bmpRect, ImageLockMode.ReadWrite, greenBmp.PixelFormat);
            BitmapData bmdBlue  = blueBmp.LockBits(bmpRect, ImageLockMode.ReadWrite, blueBmp.PixelFormat);

            int origBpp = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int newBpp = Image.GetPixelFormatSize(redBmp.PixelFormat) / 8;

            unsafe
            {
                for (int y = 0; y < bmdFull.Height; y++)
                {
                    byte* fullRow  = (byte*)bmdFull.Scan0 + (y * bmdFull.Stride);
                    byte* redRow   = (byte*)bmdRed.Scan0 + (y * bmdRed.Stride);
                    byte* greenRow = (byte*)bmdGreen.Scan0 + (y * bmdGreen.Stride);
                    byte* blueRow  = (byte*)bmdBlue.Scan0 + (y * bmdBlue.Stride);

                    for (int x = 0; x < bmdFull.Width; x++)
                    {
                        redRow[x * newBpp + 0]   = fullRow[x * origBpp + 0];
                        greenRow[x * newBpp + 1] = fullRow[x * origBpp + 1];
                        blueRow[x * newBpp + 2]  = fullRow[x * origBpp + 2];
                    }
                }
            }

            bitmap.UnlockBits(bmdFull);
            redBmp.UnlockBits(bmdRed);
            greenBmp.UnlockBits(bmdGreen);
            blueBmp.UnlockBits(bmdBlue);
            
            fullPictureBox.Image = bitmap;
            redPictureBox.Image = redBmp;
            greenPictureBox.Image = greenBmp;
            bluePictureBox.Image = blueBmp;

            ResizeComponents(bitmap.Width, bitmap.Height);
        }

        private void ResizeComponents(int width, int height)
        {
            fullPictureBox.Size  = new Size(width / 2, height / 2);
            redPictureBox.Size   = new Size(width / 2, height / 2);
            greenPictureBox.Size = new Size(width / 2, height / 2);
            bluePictureBox.Size  = new Size(width / 2, height / 2);

            this.ClientSize = new Size(width + 15, height + 15);

            fullPictureBox.Location  = new Point(5, 5);
            redPictureBox.Location   = new Point(10 + width / 2, 5);
            greenPictureBox.Location = new Point(5, 10 + height / 2);
            bluePictureBox.Location  = new Point(10 + width / 2, 10 + height / 2);
        }
    }
}
