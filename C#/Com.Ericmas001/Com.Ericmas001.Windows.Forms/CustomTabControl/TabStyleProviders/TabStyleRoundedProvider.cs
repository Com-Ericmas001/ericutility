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
    public class TabStyleRoundedProvider : TabStyleProvider
    {
        public TabStyleRoundedProvider(CustomTabControl tabControl)
            : base(tabControl)
        {
            m_Radius = 10;

            //	Must set after the _Radius as this is used in the calculations of the actual padding
            Padding = new Point(6, 3);
        }

        protected override void DrawTabCloser(int index, Graphics graphics)
        {
            if (m_ShowTabCloser && !IsTabPinned(index))
                base.DrawTabCloser(index, graphics);
        }

        public override void AddTabBorder(GraphicsPath path, Rectangle tabBounds)
        {
            switch (m_TabControl.Alignment)
            {
                case TabAlignment.Top:
                    path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X, tabBounds.Y + m_Radius);
                    path.AddArc(tabBounds.X, tabBounds.Y, m_Radius * 2, m_Radius * 2, 180, 90);
                    path.AddLine(tabBounds.X + m_Radius, tabBounds.Y, tabBounds.Right - m_Radius, tabBounds.Y);
                    path.AddArc(tabBounds.Right - m_Radius * 2, tabBounds.Y, m_Radius * 2, m_Radius * 2, 270, 90);
                    path.AddLine(tabBounds.Right, tabBounds.Y + m_Radius, tabBounds.Right, tabBounds.Bottom);
                    break;

                case TabAlignment.Bottom:
                    path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom - m_Radius);
                    path.AddArc(tabBounds.Right - m_Radius * 2, tabBounds.Bottom - m_Radius * 2, m_Radius * 2, m_Radius * 2, 0, 90);
                    path.AddLine(tabBounds.Right - m_Radius, tabBounds.Bottom, tabBounds.X + m_Radius, tabBounds.Bottom);
                    path.AddArc(tabBounds.X, tabBounds.Bottom - m_Radius * 2, m_Radius * 2, m_Radius * 2, 90, 90);
                    path.AddLine(tabBounds.X, tabBounds.Bottom - m_Radius, tabBounds.X, tabBounds.Y);
                    break;

                case TabAlignment.Left:
                    path.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X + m_Radius, tabBounds.Bottom);
                    path.AddArc(tabBounds.X, tabBounds.Bottom - m_Radius * 2, m_Radius * 2, m_Radius * 2, 90, 90);
                    path.AddLine(tabBounds.X, tabBounds.Bottom - m_Radius, tabBounds.X, tabBounds.Y + m_Radius);
                    path.AddArc(tabBounds.X, tabBounds.Y, m_Radius * 2, m_Radius * 2, 180, 90);
                    path.AddLine(tabBounds.X + m_Radius, tabBounds.Y, tabBounds.Right, tabBounds.Y);
                    break;

                case TabAlignment.Right:
                    path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right - m_Radius, tabBounds.Y);
                    path.AddArc(tabBounds.Right - m_Radius * 2, tabBounds.Y, m_Radius * 2, m_Radius * 2, 270, 90);
                    path.AddLine(tabBounds.Right, tabBounds.Y + m_Radius, tabBounds.Right, tabBounds.Bottom - m_Radius);
                    path.AddArc(tabBounds.Right - m_Radius * 2, tabBounds.Bottom - m_Radius * 2, m_Radius * 2, m_Radius * 2, 0, 90);
                    path.AddLine(tabBounds.Right - m_Radius, tabBounds.Bottom, tabBounds.X, tabBounds.Bottom);
                    break;
            }
        }
    }
}