/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms.CustomTabControl.TabStyleProviders
{
    [ToolboxItem(false)]
    public class TabStyleAngledProvider : TabStyleProvider
    {
        public TabStyleAngledProvider(CustomTabControl tabControl)
            : base(tabControl)
        {
            m_ImageAlign = ContentAlignment.MiddleRight;
            m_Overlap = 7;
            m_Radius = 10;

            //	Must set after the _Radius as this is used in the calculations of the actual padding
            Padding = new Point(10, 3);
        }

        public override void AddTabBorder(GraphicsPath path, Rectangle tabBounds)
        {
            switch (m_TabControl.Alignment)
            {
                case TabAlignment.Top:
                    path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X + m_Radius - 2, tabBounds.Y + 2);
                    path.AddLine(tabBounds.X + m_Radius, tabBounds.Y, tabBounds.Right - m_Radius, tabBounds.Y);
                    path.AddLine(tabBounds.Right - m_Radius + 2, tabBounds.Y + 2, tabBounds.Right, tabBounds.Bottom);
                    break;

                case TabAlignment.Bottom:
                    path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right - m_Radius + 2, tabBounds.Bottom - 2);
                    path.AddLine(tabBounds.Right - m_Radius, tabBounds.Bottom, tabBounds.X + m_Radius, tabBounds.Bottom);
                    path.AddLine(tabBounds.X + m_Radius - 2, tabBounds.Bottom - 2, tabBounds.X, tabBounds.Y);
                    break;

                case TabAlignment.Left:
                    path.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X + 2, tabBounds.Bottom - m_Radius + 2);
                    path.AddLine(tabBounds.X, tabBounds.Bottom - m_Radius, tabBounds.X, tabBounds.Y + m_Radius);
                    path.AddLine(tabBounds.X + 2, tabBounds.Y + m_Radius - 2, tabBounds.Right, tabBounds.Y);
                    break;

                case TabAlignment.Right:
                    path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right - 2, tabBounds.Y + m_Radius - 2);
                    path.AddLine(tabBounds.Right, tabBounds.Y + m_Radius, tabBounds.Right, tabBounds.Bottom - m_Radius);
                    path.AddLine(tabBounds.Right - 2, tabBounds.Bottom - m_Radius + 2, tabBounds.X, tabBounds.Bottom);
                    break;
            }
        }

        protected override void DrawTabCloser(int index, Graphics graphics)
        {
            if (m_ShowTabCloser && !IsTabPinned(index))
                base.DrawTabCloser(index, graphics);
        }
    }
}