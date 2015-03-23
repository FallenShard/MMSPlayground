using System;
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

        private string title;
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                mainLabel.Text = title;
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
                Invalidate();
            }
        }

        public bool HandlesEnabled
        { 
            get
            {
                return m_handlesEnabled;
            }
            set
            {
                m_handlesEnabled = value;
                Invalidate();
            }
        }

        private bool m_handlesEnabled = false;

        private bool m_greenSelected = false;
        private Color m_blue = Color.FromKnownColor(KnownColor.Blue);
        private int m_greenLeft = 0;
        private int m_greenLeftBucket = 0;
        private float m_greenLeftBucketF = 0.0f;

        private bool m_blueSelected = false;
        private Color m_green = Color.FromKnownColor(KnownColor.Green);
        private int m_blueRight = 0;
        private int m_blueRightBucket = 255;
        private float m_blueRightBucketF = 255.0f;

        public int LowerBound
        {
            get
            {
                return Math.Max(0, m_greenLeftBucket);
            }
        }

        public int UpperBound
        {
            get
            {
                return Math.Min(255, m_blueRightBucket);
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

            m_greenLeft = LeftMargin;
            m_blueRight = Width - RightMargin;
            m_greenLeftBucketF = 0.0f;
            m_blueRightBucketF = 255.0f;
            
            mainLabel.Text = title;
        }

        private void Histogram_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            SmoothingMode smoothing = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            DrawMarkers(g);
            DrawScale(g);
            DrawHistogram(g);

            if (m_handlesEnabled)
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

        private void DrawHistogram(Graphics g)
        {
            Graphics histoGraphics = Graphics.FromImage(histoBmp);

            histoGraphics.Clear(Color.FromKnownColor(KnownColor.Control));

            Pen pen = new Pen(Color.Black);

            if (m_histogram != null && m_histogram.Data.Count > 0)
            {
                float scaleFactor = 255.0f / m_histogram.MaxValue;

                for (int index = 0; index < m_histogram.Data.Count; index++)
                {
                    histoGraphics.DrawLine(pen, new Point(index, 255), new Point(index, 255 - (int)(m_histogram.Data[index] * scaleFactor)));
                }
            }

            int histoWidth = Width - LeftMargin - RightMargin;
            int histoHeight = Height - TopMargin - BottomMargin;
            g.DrawImage(histoBmp, new Rectangle(LeftMargin, TopMargin, histoWidth, histoHeight), new Rectangle(0, 0, 256, 256), GraphicsUnit.Pixel);
        }

        private void DrawSelections(Graphics g)
        {
            Pen greenPen = new Pen(m_green, 3);
            Pen bluePen = new Pen(m_blue, 3);

            int histoHeight = Height - TopMargin - BottomMargin;
            int histoWidth = Width - LeftMargin - RightMargin;
            g.DrawRectangle(greenPen, m_greenLeft, TopMargin, histoWidth / 2 - (m_greenLeft - LeftMargin), histoHeight);
            g.DrawRectangle(bluePen, LeftMargin + histoWidth / 2, TopMargin, m_blueRight - LeftMargin - histoWidth / 2, histoHeight);

            greenPen.Dispose();
            bluePen.Dispose();
        }

        private void Histogram_Resize(object sender, EventArgs e)
        {
            int histoWidth = Width - LeftMargin - RightMargin;
            int histoHeight = Height - TopMargin - BottomMargin;

            m_greenLeft = ToControlSpace(m_greenLeftBucketF);
            m_blueRight = ToControlSpace(m_blueRightBucketF);

            mainLabel.Location = new Point((Width - mainLabel.Width) / 2, Height - mainLabel.Height);
        }

        private void HistogramBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_handlesEnabled)
            {
                if (e.X > m_greenLeft - 3 && e.X < m_greenLeft + 3)
                {
                    m_green = Color.FromArgb(128, 255, 128);
                    m_greenSelected = true;
                    mainLabel.Text = "(" + m_greenLeftBucket + ")";
                    Invalidate();
                }
                else if (e.X > m_blueRight - 3 && e.X < m_blueRight + 3)
                {
                    m_blue = Color.FromArgb(128, 128, 255);
                    m_blueSelected = true;
                    mainLabel.Text = "(" + m_blueRightBucket + ")";
                    Invalidate();
                }
                else
                {
                    m_green = Color.FromKnownColor(KnownColor.Green);
                    m_greenSelected = false;

                    m_blue = Color.FromKnownColor(KnownColor.Blue);
                    m_blueSelected = false;

                    Invalidate();
                }
            }
        }

        private void HistogramBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_handlesEnabled)
            {
                if (m_greenSelected)
                {
                    m_greenLeft = Math.Min(Math.Max(e.X, LeftMargin), (Width - LeftMargin - RightMargin) / 2 + LeftMargin - 5);
                    m_green = Color.FromKnownColor(KnownColor.Green);
                    ToHistogramSpace(m_greenLeft, ref m_greenLeftBucket, ref m_greenLeftBucketF);
                    m_greenSelected = false;
                    Invalidate();

                    mainLabel.Text = title;
                }
                else if (m_blueSelected)
                {
                    int blueLeftMargin = (Width - LeftMargin - RightMargin) / 2 + LeftMargin + 5;
                    m_blueRight = Math.Min(Math.Max(e.X, blueLeftMargin), Width - RightMargin);
                    m_blue = Color.FromKnownColor(KnownColor.Blue);
                    ToHistogramSpace(m_blueRight, ref m_blueRightBucket, ref m_blueRightBucketF);
                    m_blueSelected = false;
                    Invalidate();

                    mainLabel.Text = title;
                }
            }
        }

        private void HistogramBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_handlesEnabled)
            {
                if (!(e.X < LeftMargin || e.X > Width - RightMargin || e.Y < TopMargin || e.Y > Height - BottomMargin))
                {
                    if (m_greenSelected)
                    {
                        m_greenLeft = Math.Min(Math.Max(e.X, LeftMargin), (Width - LeftMargin - RightMargin) / 2 + LeftMargin - 5);

                        ToHistogramSpace(m_greenLeft, ref m_greenLeftBucket, ref m_greenLeftBucketF);
                        mainLabel.Text = "(" + m_greenLeftBucket + ")";

                        Refresh();
                    }
                    else if (m_blueSelected)
                    {
                        int blueLeftMargin = (Width - LeftMargin - RightMargin) / 2 + LeftMargin + 5;
                        m_blueRight = Math.Min(Math.Max(e.X, blueLeftMargin), Width - RightMargin);

                        ToHistogramSpace(m_blueRight, ref m_blueRightBucket, ref m_blueRightBucketF);
                        mainLabel.Text = "(" + m_blueRightBucket + ")";

                        Refresh();
                    }
                }
            }
        }

        private void ToHistogramSpace(int xCoord, ref int iRescaledX, ref float fRescaledX)
        {
            float histoWidth = Width - LeftMargin - RightMargin;

            fRescaledX = (xCoord - LeftMargin) / histoWidth * 256.0f;

            iRescaledX = (int)fRescaledX;
        }

        private int ToControlSpace(float histBucket)
        {
            float x = LeftMargin;
            int histoWidth = Width - LeftMargin - RightMargin;
            float xScale = histoWidth / 256.0f;
            x += histBucket * xScale;

            return (int)x;
        }
    }
}
