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
    public class TabStyleDefaultProvider : TabStyleProvider
    {
        public TabStyleDefaultProvider(CustomTabControl tabControl)
            : base(tabControl)
        {
            m_FocusTrack = true;
            m_Radius = 2;
        }

        public override void AddTabBorder(GraphicsPath path, Rectangle tabBounds)
        {
            switch (m_TabControl.Alignment)
            {
                case TabAlignment.Top:
                    path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X, tabBounds.Y);
                    path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right, tabBounds.Y);
                    path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom);
                    break;

                case TabAlignment.Bottom:
                    path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom);
                    path.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X, tabBounds.Bottom);
                    path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X, tabBounds.Y);
                    break;

                case TabAlignment.Left:
                    path.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X, tabBounds.Bottom);
                    path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X, tabBounds.Y);
                    path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right, tabBounds.Y);
                    break;

                case TabAlignment.Right:
                    path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right, tabBounds.Y);
                    path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom);
                    path.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X, tabBounds.Bottom);
                    break;
            }
        }

        public override Rectangle GetTabRect(int index)
        {
            if (index < 0)
            {
                return new Rectangle();
            }

            var tabBounds = base.GetTabRect(index);
            var firstTabinRow = m_TabControl.IsFirstTabInRow(index);

            //	Make non-SelectedTabs smaller and selected tab bigger
            if (index != m_TabControl.SelectedIndex)
            {
                switch (m_TabControl.Alignment)
                {
                    case TabAlignment.Top:
                        tabBounds.Y += 1;
                        tabBounds.Height -= 1;
                        break;

                    case TabAlignment.Bottom:
                        tabBounds.Height -= 1;
                        break;

                    case TabAlignment.Left:
                        tabBounds.X += 1;
                        tabBounds.Width -= 1;
                        break;

                    case TabAlignment.Right:
                        tabBounds.Width -= 1;
                        break;
                }
            }
            else
            {
                switch (m_TabControl.Alignment)
                {
                    case TabAlignment.Top:
                        if (tabBounds.Y > 0)
                        {
                            tabBounds.Y -= 1;
                            tabBounds.Height += 1;
                        }

                        if (firstTabinRow)
                        {
                            tabBounds.Width += 1;
                        }
                        else
                        {
                            tabBounds.X -= 1;
                            tabBounds.Width += 2;
                        }
                        break;

                    case TabAlignment.Bottom:
                        if (tabBounds.Bottom < m_TabControl.Bottom)
                        {
                            tabBounds.Height += 1;
                        }
                        if (firstTabinRow)
                        {
                            tabBounds.Width += 1;
                        }
                        else
                        {
                            tabBounds.X -= 1;
                            tabBounds.Width += 2;
                        }
                        break;

                    case TabAlignment.Left:
                        if (tabBounds.X > 0)
                        {
                            tabBounds.X -= 1;
                            tabBounds.Width += 1;
                        }

                        if (firstTabinRow)
                        {
                            tabBounds.Height += 1;
                        }
                        else
                        {
                            tabBounds.Y -= 1;
                            tabBounds.Height += 2;
                        }
                        break;

                    case TabAlignment.Right:
                        if (tabBounds.Right < m_TabControl.Right)
                        {
                            tabBounds.Width += 1;
                        }
                        if (firstTabinRow)
                        {
                            tabBounds.Height += 1;
                        }
                        else
                        {
                            tabBounds.Y -= 1;
                            tabBounds.Height += 2;
                        }
                        break;
                }
            }

            //	Adjust first tab in the row to align with tabpage
            EnsureFirstTabIsInView(ref tabBounds, index);

            return tabBounds;
        }

        protected override void DrawTabCloser(int index, Graphics graphics)
        {
            if (m_ShowTabCloser && !IsTabPinned(index))
                base.DrawTabCloser(index, graphics);
        }
    }
}