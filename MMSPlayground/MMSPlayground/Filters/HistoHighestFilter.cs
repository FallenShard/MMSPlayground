﻿using MMSPlayground.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace MMSPlayground.Filters
{
    public class HistoHighestFilter : IFilter
    {
        private HistoFilterPackage m_package;
        private Bitmap m_prevBitmap = null;

        private IImageModel m_model = null;

        public HistoHighestFilter(IImageModel model, HistoFilterPackage package)
        {
            m_model = model;
            m_package = package;
        }

        public void Apply()
        {
            Bitmap bitmap = m_model.GetBitmap();
            m_prevBitmap = (Bitmap)bitmap.Clone();

            if (m_model.GetWin32CoreUsageMode())
            {
                Histogram[] histos = m_model.GetHistograms();

                int[] highestLow = new int[3];
                int[] highestHigh = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    IEnumerable<int> selection = histos[i].Data.Skip(m_package.lower[i]).Take(128 - m_package.lower[i]);
                    IEnumerator<int> iter = selection.GetEnumerator();
                    int k = m_package.lower[i];
                    int current = Int32.MinValue;
                    while (iter.MoveNext())
                    {
                        if (iter.Current > current)
                            highestLow[i] = k + m_package.lower[i];
                        k++;
                    }

                    selection = histos[i].Data.Skip(128).Take(m_package.upper[i] - 128);
                    iter = selection.GetEnumerator();
                    k = 128;
                    current = Int32.MinValue;
                    while (iter.MoveNext())
                    {
                        if (iter.Current > current)
                            highestLow[i] = k;
                        k++;
                    }
                }

                Rectangle bmpRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bmd = bitmap.LockBits(bmpRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

                int bpp = ImageUtils.GetComponentsPerPixel(bmd);

                byte[] yCbCr = new byte[3];
                byte[] rgb = new byte[3];
                bool changed = false;

                unsafe
                {
                    for (int y = 0; y < bmd.Height; y++)
                    {
                        byte* dataRow = (byte*)bmd.Scan0 + (y * bmd.Stride);

                        for (int x = 0; x < bmd.Width; x++)
                        {
                            changed = false;
                            int index = x * bpp;
                            rgb[0] = dataRow[index + 2];
                            rgb[1] = dataRow[index + 1];
                            rgb[2] = dataRow[index + 0];

                            ImageUtils.RgbToYCbCr(rgb, yCbCr);

                            for (int i = 0; i < 3; i++)
                            {
                                if (yCbCr[i] < m_package.lower[i])
                                {
                                    yCbCr[i] = (byte)highestLow[i];
                                    changed = true;
                                }

                                if (yCbCr[i] > m_package.upper[i])
                                {
                                    yCbCr[i] = (byte)highestHigh[i];
                                    changed = true;
                                }
                            }

                            if (changed)
                                ImageUtils.YCbCrToRgb(yCbCr, rgb);

                            dataRow[index + 0] = rgb[2];
                            dataRow[index + 1] = rgb[1];
                            dataRow[index + 2] = rgb[0];
                        }
                    }
                }

                bitmap.UnlockBits(bmd);
            }
            else
            {
                //for (int y = 0; y < bitmap.Height; y++)
                //{
                //    for (int x = 0; x < bitmap.Width; x++)
                //    {
                //        Color currPixel = bitmap.GetPixel(x, y);

                //        int newR = ImageUtils.Clamp(currPixel.R + m_bias, 0, 255);
                //        int newG = ImageUtils.Clamp(currPixel.G + m_bias, 0, 255);
                //        int newB = ImageUtils.Clamp(currPixel.B + m_bias, 0, 255);

                //        Color newPixel = Color.FromArgb(newR, newG, newB);

                //        bitmap.SetPixel(x, y, newPixel);
                //    }
                //}
            }

            m_model.SetBitmap(bitmap);
        }

        public void Undo()
        {
            m_model.SetBitmap(m_prevBitmap);
        }

        public IFilter Clone()
        {
            HistoHighestFilter clone = new HistoHighestFilter(m_model, m_package);

            return clone;
        }

        public string FilterName
        {
            get
            {
                return "Highest Replacement";
            }
        }
    }
}
