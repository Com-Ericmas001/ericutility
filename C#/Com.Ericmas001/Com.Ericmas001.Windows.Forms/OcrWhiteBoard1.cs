using System;
using System.Drawing;
using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms
{
    public class OcrWhiteBoard1 : PictureBox
    {
        private Pen m_Pen = Pens.Black;
        private Graphics m_Grfx;
        private Point m_LastPoint = Point.Empty;

        public float PenSize
        {
            get { return m_Pen.Width; }
            set { m_Pen = new Pen(m_Pen.Color, value); }
        }

        public Color PenColor
        {
            get { return m_Pen.Color; }
            set { m_Pen = new Pen(value, m_Pen.Width); }
        }

        public OcrWhiteBoard1()
        {
            Image = new Bitmap(Width, Height);
            m_Grfx = Graphics.FromImage(Image);
            m_Grfx.Clear(Color.White);
            Cursor = Cursors.Cross;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Right)
            {
                m_Grfx.Clear(Color.White);
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                if (m_LastPoint == Point.Empty)
                    m_LastPoint = e.Location;
                m_Grfx.DrawLine(m_Pen, e.Location, m_LastPoint);
                m_LastPoint = e.Location;
                Invalidate();
            }
            else
                m_LastPoint = Point.Empty;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var lastOne = Image;
            Image = new Bitmap(Width, Height);
            m_Grfx = Graphics.FromImage(Image);
            m_Grfx.Clear(Color.White);
            m_Grfx.DrawImage(lastOne, Point.Empty);
        }

        public Bitmap GetChargeUtile()
        {
            var bmp = (Bitmap)Image;
            int left, right, top, bottom;
            left = right = top = bottom = -1;

            for (var j = 0; j < bmp.Height; ++j)
                for (var i = 0; i < bmp.Width && (top == -1 || bottom == -1); ++i)
                {
                    if (top == -1 && bmp.GetPixel(i, j) != Color.FromArgb(255, Color.White))
                        top = j;
                    if (bottom == -1 && bmp.GetPixel(i, bmp.Height - 1 - j) != Color.FromArgb(255, Color.White))
                        bottom = bmp.Height - 1 - j;
                }
            for (var i = 0; i < bmp.Width; ++i)
                for (var j = 0; j < bmp.Height && (left == -1 || right == -1); ++j)
                {
                    if (left == -1 && bmp.GetPixel(i, j) != Color.FromArgb(255, Color.White))
                        left = i;
                    if (right == -1 && bmp.GetPixel(bmp.Width - 1 - i, j) != Color.FromArgb(255, Color.White))
                        right = bmp.Width - 1 - i;
                }
            Bitmap newBmp;
            if (right < 0 || left < 0 || top < 0 || bottom < 0)
                return null;
            newBmp = new Bitmap(right - left + 1, bottom - top + 1);
            var g = Graphics.FromImage(newBmp);
            g.DrawImage(bmp, 0 - left, 0 - top);
            return newBmp;
        }
    }
}