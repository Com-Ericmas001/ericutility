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
    public class TabStyleVs2010Provider : TabStyleRoundedProvider
    {
        public TabStyleVs2010Provider(CustomTabControl tabControl)
            : base(tabControl)
        {
            m_Radius = 3;
            m_ShowTabCloser = true;
            m_CloserColorActive = Color.Black;
            m_CloserColor = Color.FromArgb(117, 99, 61);
            m_TextColor = Color.White;
            m_TextColorDisabled = Color.WhiteSmoke;
            m_BorderColor = Color.Transparent;
            m_BorderColorHot = Color.FromArgb(155, 167, 183);

            //	Must set after the _Radius as this is used in the calculations of the actual padding
            Padding = new Point(6, 5);
        }

        protected override Brush GetTabBackgroundBrush(int index)
        {
            LinearGradientBrush fillBrush = null;

            //	Capture the colours dependant on selection state of the tab
            var dark = Color.Transparent;
            var light = Color.Transparent;

            if (m_TabControl.SelectedIndex == index)
            {
                dark = Color.FromArgb(229, 195, 101);
                light = SystemColors.Window;
            }
            else if (!m_TabControl.TabPages[index].Enabled)
            {
                light = dark;
            }
            else if (HotTrack && index == m_TabControl.ActiveIndex)
            {
                //	Enable hot tracking
                dark = Color.FromArgb(108, 116, 118);
                light = dark;
            }

            //	Get the correctly aligned gradient
            var tabBounds = GetTabRect(index);
            tabBounds.Inflate(3, 3);
            tabBounds.X -= 1;
            tabBounds.Y -= 1;
            switch (m_TabControl.Alignment)
            {
                case TabAlignment.Top:
                    fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Vertical);
                    break;

                case TabAlignment.Bottom:
                    fillBrush = new LinearGradientBrush(tabBounds, dark, light, LinearGradientMode.Vertical);
                    break;

                case TabAlignment.Left:
                    fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Horizontal);
                    break;

                case TabAlignment.Right:
                    fillBrush = new LinearGradientBrush(tabBounds, dark, light, LinearGradientMode.Horizontal);
                    break;
            }

            //	Add the blend
            if (fillBrush != null)
            {
                fillBrush.Blend = GetBackgroundBlend();

            }
            return fillBrush;
        }

        private static Blend GetBackgroundBlend()
        {
            var relativeIntensities = new[] { 0f, 0.5f, 1f, 1f };
            var relativePositions = new[] { 0f, 0.5f, 0.51f, 1f };

            var blend = new Blend();
            blend.Factors = relativeIntensities;
            blend.Positions = relativePositions;

            return blend;
        }

        public override Brush GetPageBackgroundBrush(int index)
        {
            //	Capture the colours dependant on selection state of the tab
            var light = Color.Transparent;

            if (m_TabControl.SelectedIndex == index)
            {
                light = Color.FromArgb(229, 195, 101);
            }
            else if (!m_TabControl.TabPages[index].Enabled)
            {
                light = Color.Transparent;
            }
            else if (m_HotTrack && index == m_TabControl.ActiveIndex)
            {
                //	Enable hot tracking
                light = Color.Transparent;
            }

            return new SolidBrush(light);
        }

        protected override void DrawTabCloser(int index, Graphics graphics)
        {
            if (m_ShowTabCloser && !IsTabPinned(index))
            {
                var closerRect = m_TabControl.GetTabCloserRect(index);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                if (closerRect.Contains(m_TabControl.MousePosition))
                {
                    using (var closerPath = GetCloserButtonPath(closerRect))
                    {
                        graphics.FillPath(Brushes.White, closerPath);
                        using (var closerPen = new Pen(Color.FromArgb(229, 195, 101)))
                        {
                            graphics.DrawPath(closerPen, closerPath);
                        }
                    }
                    using (var closerPath = GetCloserPath(closerRect))
                    {
                        using (var closerPen = new Pen(m_CloserColorActive))
                        {
                            closerPen.Width = 2;
                            graphics.DrawPath(closerPen, closerPath);
                        }
                    }
                }
                else
                {
                    if (index == m_TabControl.SelectedIndex)
                    {
                        using (var closerPath = GetCloserPath(closerRect))
                        {
                            using (var closerPen = new Pen(m_CloserColor))
                            {
                                closerPen.Width = 2;
                                graphics.DrawPath(closerPen, closerPath);
                            }
                        }
                    }
                    else if (index == m_TabControl.ActiveIndex)
                    {
                        using (var closerPath = GetCloserPath(closerRect))
                        {
                            using (var closerPen = new Pen(Color.FromArgb(155, 167, 183)))
                            {
                                closerPen.Width = 2;
                                graphics.DrawPath(closerPen, closerPath);
                            }
                        }
                    }
                }
            }
        }

        private static GraphicsPath GetCloserButtonPath(Rectangle closerRect)
        {
            var closerPath = new GraphicsPath();
            closerPath.AddLine(closerRect.X - 1, closerRect.Y - 2, closerRect.Right + 1, closerRect.Y - 2);
            closerPath.AddLine(closerRect.Right + 2, closerRect.Y - 1, closerRect.Right + 2, closerRect.Bottom + 1);
            closerPath.AddLine(closerRect.Right + 1, closerRect.Bottom + 2, closerRect.X - 1, closerRect.Bottom + 2);
            closerPath.AddLine(closerRect.X - 2, closerRect.Bottom + 1, closerRect.X - 2, closerRect.Y - 1);
            closerPath.CloseFigure();
            return closerPath;
        }
    }
}