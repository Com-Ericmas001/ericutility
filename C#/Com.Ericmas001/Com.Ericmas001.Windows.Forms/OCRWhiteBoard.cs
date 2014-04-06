using System;
using System.Drawing;
using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms
{
    public class OCRWhiteBoard : PictureBox
    {
        private Pen m_Pen = Pens.Black;
        private Graphics grfx;
        private Point lastPoint = Point.Empty;

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

        public OCRWhiteBoard()
            : base()
        {
            Image = new Bitmap(Width, Height);
            grfx = Graphics.FromImage(Image);
            grfx.Clear(Color.White);
            Cursor = Cursors.Cross;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Right)
            {
                grfx.Clear(Color.White);
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                if (lastPoint == Point.Empty)
                    lastPoint = e.Location;
                grfx.DrawLine(m_Pen, e.Location, lastPoint);
                lastPoint = e.Location;
                Invalidate();
            }
            else
                lastPoint = Point.Empty;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Image lastOne = Image;
            Image = new Bitmap(Width, Height);
            grfx = Graphics.FromImage(Image);
            grfx.Clear(Color.White);
            grfx.DrawImage(lastOne, Point.Empty);
        }

        public Bitmap GetChargeUtile()
        {
            Bitmap bmp = (Bitmap)Image;
            int left, right, top, bottom;
            left = right = top = bottom = -1;

            for (int j = 0; j < bmp.Height; ++j)
                for (int i = 0; i < bmp.Width && (top == -1 || bottom == -1); ++i)
                {
                    if (top == -1 && bmp.GetPixel(i, j) != Color.FromArgb(255, Color.White))
                        top = j;
                    if (bottom == -1 && bmp.GetPixel(i, bmp.Height - 1 - j) != Color.FromArgb(255, Color.White))
                        bottom = bmp.Height - 1 - j;
                }
            for (int i = 0; i < bmp.Width; ++i)
                for (int j = 0; j < bmp.Height && (left == -1 || right == -1); ++j)
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
            Graphics g = Graphics.FromImage(newBmp);
            g.DrawImage(bmp, 0 - left, 0 - top);
            return newBmp;
        }
    }
}