﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using MMSPlayground.Model;

namespace MMSPlayground.Views.Controls
{
    public partial class HistogramBox : UserControl
    {
        private static int LeftMargin = 22;
        private static int TopMargin = 25;
        private static int RightMargin = 12;
        private static int BottomMargin = 47;
        private static int MarkerYOffset = -7;

        private Bitmap histoBmp = new Bitmap(256, 256);
        private Histogram m_histogram = null;

        public string Title
        {
            get
            {
                return mainLabel.Text;
            }

            set
            {
                mainLabel.Text = value;
            }
        }

        public Histogram Histogram
        {
            get
            {
                return m_histogram;
            }

            set
            {
                m_histogram = value;

                Graphics g = Graphics.FromImage(histoBmp);

                g.Clear(Color.FromKnownColor(KnownColor.Control));

                Pen pen = new Pen(Color.Black);

                if (m_histogram != null && m_histogram.Data.Count > 0)
                {
                    float scaleFactor = 255.0f / m_histogram.MaxValue;

                    for (int index = 0; index < m_histogram.Data.Count; index++)
                    {
                        g.DrawLine(pen, new Point(index, 255), new Point(index, 255 - (int)(m_histogram.Data[index] * scaleFactor)));
                    }
                }

                histogramBox.Image = histoBmp;
                histogramBox.Refresh();

                pen.Dispose();
                g.Dispose();
            }
        }


        public HistogramBox()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            UpdateStyles();

            Histogram = new Histogram();
        }

        private void Histogram_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            SmoothingMode smoothing = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            DrawMarkers(g);
            DrawScale(g);
            DrawSelections(g);

            g.SmoothingMode = smoothing;
        }

        private void DrawMarkers(Graphics g)
        {
            int histoWidth = Width - LeftMargin - RightMargin;
            int valuesHeight = Height - BottomMargin;

            AdjustableArrowCap bigArrow = new AdjustableArrowCap(3, 5);
            Pen arrowPen = new Pen(Color.Black);
            arrowPen.CustomStartCap = bigArrow;
            arrowPen.CustomEndCap = bigArrow;

            Font myFont = new Font("Segoe UI", 9);
            SolidBrush textBrush = new SolidBrush(Color.FromArgb(50, 50, 50));

            SizeF shadowsSize = g.MeasureString("Shadows", myFont);
            SizeF midtonesSize = g.MeasureString("Midtones", myFont);
            SizeF highlightsSize = g.MeasureString("Highlights", myFont);

            int y = TopMargin + MarkerYOffset;
            g.DrawString("Shadows", myFont, textBrush, new PointF(LeftMargin + histoWidth / 6 - shadowsSize.Width / 2, 0));
            g.DrawLine(arrowPen, new Point(LeftMargin, y), new Point(LeftMargin + histoWidth / 3, y));

            g.DrawString("Midtones", myFont, textBrush, new PointF(LeftMargin + histoWidth / 2 - midtonesSize.Width / 2, 0));
            g.DrawLine(arrowPen, new Point(LeftMargin + histoWidth / 3, y), new Point(LeftMargin + histoWidth * 2 / 3, y));

            g.DrawString("Highlights", myFont, textBrush, new PointF(LeftMargin + histoWidth * 5 / 6 - highlightsSize.Width / 2, 0));
            g.DrawLine(arrowPen, new Point(LeftMargin + histoWidth * 2 / 3, y), new Point(LeftMargin + histoWidth, y));

            arrowPen.CustomStartCap = new AdjustableArrowCap(0, 0);

            g.DrawString(m_histogram.MinBucket.ToString(), myFont, textBrush, new PointF(LeftMargin, valuesHeight));
            SizeF minSize = g.MeasureString(m_histogram.MinBucket.ToString(), myFont);
            SizeF maxSize = g.MeasureString(m_histogram.MaxBucket.ToString(), myFont);
            g.DrawLine(arrowPen,
                new Point(LeftMargin + (int)minSize.Width, valuesHeight + (int)minSize.Height / 2),
                new Point(Width - RightMargin - (int)maxSize.Width, valuesHeight + (int)minSize.Height / 2));
            g.DrawString(m_histogram.MaxBucket.ToString(), myFont, textBrush, new PointF(Width - RightMargin - maxSize.Width, valuesHeight));

            StringFormat format = new StringFormat();
            format.FormatFlags = StringFormatFlags.DirectionVertical;

            SizeF vertTextSize = g.MeasureString("Pixel Number", myFont);
            g.DrawString("Pixel Number", myFont, textBrush, 0, Height - BottomMargin - vertTextSize.Width, format);
            g.DrawLine(arrowPen,
                new PointF(vertTextSize.Height / 2, Height - BottomMargin - vertTextSize.Width),
                new PointF(vertTextSize.Height / 2, TopMargin));

            textBrush.Dispose();
            arrowPen.Dispose();
        }

        private void DrawScale(Graphics g)
        {
            int histoWidth = Width - LeftMargin - RightMargin;

            Rectangle r = new Rectangle(LeftMargin, Height - BottomMargin + 15, histoWidth, 12);

            LinearGradientBrush linGrBrush = new LinearGradientBrush(
                new Point(r.Left, r.Top),
                new Point(r.Right, r.Bottom),
                Color.FromArgb(0, 0, 0), 
                Color.FromArgb(255, 255, 255));

            g.FillRectangle(linGrBrush, r);

            linGrBrush.Dispose();
        }

        private void DrawSelections(Graphics g)
        {

        }

        private void Histogram_Resize(object sender, EventArgs e)
        {
            int histoWidth = Width - LeftMargin - RightMargin;
            int histoHeight = Height - TopMargin - BottomMargin;

            histogramBox.Location = new Point(LeftMargin, TopMargin);
            histogramBox.Size = new Size(histoWidth, histoHeight);

            mainLabel.Location = new Point((Width - mainLabel.Width) / 2, Height - mainLabel.Height);
        }
    }
}
