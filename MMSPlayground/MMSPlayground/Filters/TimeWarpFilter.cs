using MMSPlayground.Model;
using MMSPlayground.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace MMSPlayground.Filters
{
    public class TimeWarpFilter : IFilter
    {
        private int m_factor = 0;
        private Bitmap m_prevBitmap = null;
        private IImageModel m_model = null;

        public TimeWarpFilter(IImageModel model, int factor)
        {
            m_factor = factor;

            m_model = model;
        }

        public void Apply()
        {
            m_prevBitmap = (Bitmap)m_model.GetBitmap().Clone();

            m_model.SetBitmap(GetRawResults());
        }

        public Bitmap GetRawResults()
        {
            Bitmap bmpOrig = m_model.GetBitmap();
            Bitmap bitmap = new Bitmap(bmpOrig.Width, bmpOrig.Height, bmpOrig.PixelFormat);

            Point[][] offsetMat = new Point[bitmap.Height][];
            Point mid = new Point();
            mid.X = bitmap.Width / 2;
            mid.Y = bitmap.Height / 2;

            for (int i = 0; i < bitmap.Height; i++)
            {
                offsetMat[i] = new Point[bitmap.Width];
            }

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    int trueX = x - mid.X;
                    int trueY = y - mid.Y;
                    double theta = Math.Atan2((double)trueY, (double)trueX);
                    double radius = Math.Sqrt(trueX * trueX + trueY * trueY);
                    double newRadius = Math.Sqrt(radius) * m_factor;

                    int newX = mid.X + (int)(newRadius * Math.Cos(theta));
                    if (newX > 0 && newX < bitmap.Width)
                        offsetMat[y][x].X = (int)newX;
                    else
                        offsetMat[y][x].X = 0;

                    int newY = mid.Y + (int)(newRadius * Math.Sin(theta));
                    if (newY > 0 && newY < bitmap.Height)
                        offsetMat[y][x].Y = (int)newY;
                    else
                        offsetMat[y][x].Y = 0;
                }
            }

            if (m_model.GetWin32CoreUsageMode())
            {
                Rectangle bmpRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bmd = bitmap.LockBits(bmpRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
                BitmapData bmdOrig = bmpOrig.LockBits(bmpRect, ImageLockMode.ReadWrite, bmpOrig.PixelFormat);

                int bpp = ImageUtils.GetComponentsPerPixel(bmd);

                unsafe
                {
                    for (int y = 0; y < bmd.Height; y++)
                    {
                        byte* dataRow = (byte*)bmd.Scan0 + (y * bmd.Stride);

                        for (int x = 0; x < bmd.Width; x++)
                        {
                            int dstIndex = x * bpp;

                            int xOffset = offsetMat[y][x].X;
                            int yOffset = offsetMat[y][x].Y;

                            int origX = xOffset;
                            int origY = yOffset;

                            if (origX >= 0 && origX < bmdOrig.Width && origY >= 0 && origY < bmdOrig.Height)
                            {
                                byte* origPtr = (byte*)bmdOrig.Scan0 + (origY * bmdOrig.Stride);

                                dataRow[dstIndex + 2] = origPtr[origX * bpp + 2];
                                dataRow[dstIndex + 1] = origPtr[origX * bpp + 1];
                                dataRow[dstIndex + 0] = origPtr[origX * bpp + 0];
                            }
                        }
                    }
                }

                bmpOrig.UnlockBits(bmdOrig);
                bitmap.UnlockBits(bmd);
            }
            else
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color currPixel = bitmap.GetPixel(x, y);

                        int newR = ImageUtils.Clamp(currPixel.R + m_factor, 0, 255);
                        int newG = ImageUtils.Clamp(currPixel.G + m_factor, 0, 255);
                        int newB = ImageUtils.Clamp(currPixel.B + m_factor, 0, 255);

                        Color newPixel = Color.FromArgb(newR, newG, newB);

                        bitmap.SetPixel(x, y, newPixel);
                    }
                }
            }

            return bitmap;
        }

        public void Undo()
        {
            m_model.SetBitmap(m_prevBitmap);
        }

        public IFilter Clone()
        {
            TimeWarpFilter clone = new TimeWarpFilter(m_model, m_factor);

            return clone;
        }

        public string FilterName
        { 
            get
            {
                return "TimeWarp";
            }
        }
    }
}
