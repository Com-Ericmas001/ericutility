using System.Drawing;

namespace Com.Ericmas001.Drawing
{
    public class ImageUtility
    {
        public static Bitmap ResizedImage(Image src, int newWidth, int newHeight)
        {
            Bitmap nimg = new Bitmap(newWidth, newHeight);
            Graphics g = Graphics.FromImage(nimg);
            g.DrawImage(src, 0, 0, newWidth, newHeight);
            return nimg;
        }
    }
}