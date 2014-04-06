using System;
using System.Drawing;
using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms
{
    public class OCRTableau : PictureBox
    {
        private int m_M = 5;
        private int m_N = 5;
        private bool[,] values;

        public int M
        {
            get { return m_M; }
            set
            {
                if (m_M != value && m_M > 0)
                {
                    m_M = value;
                    values = new bool[m_N, m_M];
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
                    values = new bool[m_N, m_M];
                    Invalidate();
                }
            }
        }

        public OCRTableau()
            : base()
        {
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            values = new bool[m_N, m_M];
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
                Graphics grfx = pe.Graphics;
                grfx.Clear(BackColor);
                int width = Width / m_M;
                int height = Height / m_N;
                for (int i = 1; i < m_M; ++i)
                    grfx.DrawLine(Pens.Black, new Point(i * width, 0), new Point(i * width, Height));
                for (int i = 1; i < m_N; ++i)
                    grfx.DrawLine(Pens.Black, new Point(0, i * height), new Point(Width, i * height));
                for (int i = 0; i < m_N; ++i)
                    for (int j = 0; j < m_M; ++j)
                        if (values[i, j])
                            grfx.FillRectangle(Brushes.Black, i * width, j * height, width, height);
            }
        }

        private void Clear()
        {
            Image = new Bitmap(Width, Height);
            Graphics grfx = Graphics.FromImage(Image);
            grfx.Clear(BackColor);
            Invalidate();
        }

        public void Reset()
        {
            values = new bool[m_N, m_M];
            Clear();
        }

        public void SetValue(int i, int j, bool trueOrFalse)
        {
            if (i >= m_N || j >= m_M || i < 0 || j < 0)
                return;
            values[i, j] = trueOrFalse;
            Invalidate();
        }

        public void ConvertImage(Bitmap newBmp)
        {
            Reset();
            if (newBmp == null)
                return;

            int height = newBmp.Height / m_N;
            int width = newBmp.Width / m_M;
            for (int l = 0; l < m_N; ++l)
                for (int c = 0; c < m_M; ++c)
                {
                    bool color = false;
                    for (int i = 0; i < width && i + l * width < newBmp.Width; ++i)
                        for (int j = 0; j < height && j + c * height < newBmp.Height; ++j)
                            if (newBmp.GetPixel(i + l * width, j + c * height) != Color.FromArgb(255, Color.White))
                                color = true;
                    SetValue(l, c, color);
                }
        }
    }
}