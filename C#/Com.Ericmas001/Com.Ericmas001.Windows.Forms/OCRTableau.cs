using System;
using System.Drawing;
using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms
{
    public class OcrTableau : PictureBox
    {
        private int m_M = 5;
        private int m_N = 5;
        private bool[,] m_Values;

        public int M
        {
            get { return m_M; }
            set
            {
                if (m_M != value && m_M > 0)
                {
                    m_M = value;
                    m_Values = new bool[m_N, m_M];
                    Invalidate();
                }
            }
        }

        public int N
        {
            get { return m_N; }
            set
            {
                if (m_N != value && m_N > 0)
                {
                    m_N = value;
                    m_Values = new bool[m_N, m_M];
                    Invalidate();
                }
            }
        }

        public OcrTableau()
        {
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            m_Values = new bool[m_N, m_M];
            Clear();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Clear();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (Image != null)
            {
                var grfx = pe.Graphics;
                grfx.Clear(BackColor);
                var width = Width / m_M;
                var height = Height / m_N;
                for (var i = 1; i < m_M; ++i)
                    grfx.DrawLine(Pens.Black, new Point(i * width, 0), new Point(i * width, Height));
                for (var i = 1; i < m_N; ++i)
                    grfx.DrawLine(Pens.Black, new Point(0, i * height), new Point(Width, i * height));
                for (var i = 0; i < m_N; ++i)
                    for (var j = 0; j < m_M; ++j)
                        if (m_Values[i, j])
                            grfx.FillRectangle(Brushes.Black, i * width, j * height, width, height);
            }
        }

        private void Clear()
        {
            Image = new Bitmap(Width, Height);
            var grfx = Graphics.FromImage(Image);
            grfx.Clear(BackColor);
            Invalidate();
        }

        public void Reset()
        {
            m_Values = new bool[m_N, m_M];
            Clear();
        }

        public void SetValue(int i, int j, bool trueOrFalse)
        {
            if (i >= m_N || j >= m_M || i < 0 || j < 0)
                return;
            m_Values[i, j] = trueOrFalse;
            Invalidate();
        }

        public void ConvertImage(Bitmap newBmp)
        {
            Reset();
            if (newBmp == null)
                return;

            var height = newBmp.Height / m_N;
            var width = newBmp.Width / m_M;
            for (var l = 0; l < m_N; ++l)
                for (var c = 0; c < m_M; ++c)
                {
                    var color = false;
                    for (var i = 0; i < width && i + l * width < newBmp.Width; ++i)
                        for (var j = 0; j < height && j + c * height < newBmp.Height; ++j)
                            if (newBmp.GetPixel(i + l * width, j + c * height) != Color.FromArgb(255, Color.White))
                                color = true;
                    SetValue(l, c, color);
                }
        }
    }
}