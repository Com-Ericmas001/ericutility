/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Com.Ericmas001.Windows.Forms.CustomTabControl.TabStyleProviders;
using Com.Ericmas001.Windows.Forms.Properties;

namespace Com.Ericmas001.Windows.Forms.CustomTabControl
{
    [ToolboxItem(false)]
    public abstract class TabStyleProvider : Component
    {
        #region Constructor

        protected TabStyleProvider(CustomTabControl tabControl)
        {
            m_TabControl = tabControl;

            m_BorderColor = Color.Empty;
            m_BorderColorSelected = Color.Empty;
            m_FocusColor = Color.Orange;

            if (m_TabControl.RightToLeftLayout)
            {
                m_ImageAlign = ContentAlignment.MiddleRight;
            }
            else
            {
                m_ImageAlign = ContentAlignment.MiddleLeft;
            }

            HotTrack = true;

            //	Must set after the _Overlap as this is used in the calculations of the actual padding
            Padding = new Point(6, 3);
        }

        #endregion Constructor

        #region Factory Methods

        public static TabStyleProvider CreateProvider(CustomTabControl tabControl)
        {
            TabStyleProvider provider;

            //	Depending on the display style of the tabControl generate an appropriate provider.
            switch (tabControl.DisplayStyle)
            {
                case TabStyle.None:
                    provider = new TabStyleNoneProvider(tabControl);
                    break;

                case TabStyle.Default:
                    provider = new TabStyleDefaultProvider(tabControl);
                    break;

                case TabStyle.Angled:
                    provider = new TabStyleAngledProvider(tabControl);
                    break;

                case TabStyle.Rounded:
                    provider = new TabStyleRoundedProvider(tabControl);
                    break;

                case TabStyle.VisualStudio:
                    provider = new TabStyleVisualStudioProvider(tabControl);
                    break;

                case TabStyle.Chrome:
                    provider = new TabStyleChromeProvider(tabControl);
                    break;

                case TabStyle.Ie8:
                    provider = new TabStyleIe8Provider(tabControl);
                    break;

                case TabStyle.Vs2010:
                    provider = new TabStyleVs2010Provider(tabControl);
                    break;

                default:
                    provider = new TabStyleDefaultProvider(tabControl);
                    break;
            }

            provider.m_Style = tabControl.DisplayStyle;
            return provider;
        }

        #endregion Factory Methods

        #region	Protected variables

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected CustomTabControl m_TabControl;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Point m_Padding;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected bool m_HotTrack;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected TabStyle m_Style = TabStyle.Default;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected ContentAlignment m_ImageAlign;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected int m_Radius = 1;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected int m_Overlap;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected bool m_FocusTrack;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected float m_Opacity = 1;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected bool m_ShowTabCloser;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color m_BorderColorSelected = Color.Empty;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color m_BorderColor = Color.Empty;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color m_BorderColorHot = Color.Empty;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color m_CloserColorActive = Color.Black;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color m_CloserColor = Color.DarkGray;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color m_FocusColor = Color.Empty;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color m_TextColor = Color.Empty;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color m_TextColorSelected = Color.Empty;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color m_TextColorDisabled = Color.Empty;

        #endregion

        #region overridable Methods

        public abstract void AddTabBorder(GraphicsPath path, Rectangle tabBounds);

        public virtual Rectangle GetTabRect(int index)
        {
            if (index < 0)
            {
                return new Rectangle();
            }
            var tabBounds = m_TabControl.GetTabRect(index);
            if (m_TabControl.RightToLeftLayout)
            {
                tabBounds.X = m_TabControl.Width - tabBounds.Right;
            }
            var firstTabinRow = m_TabControl.IsFirstTabInRow(index);

            //	Expand to overlap the tabpage
            switch (m_TabControl.Alignment)
            {
                case TabAlignment.Top:
                    tabBounds.Height += 2;
                    break;

                case TabAlignment.Bottom:
                    tabBounds.Height += 2;
                    tabBounds.Y -= 2;
                    break;

                case TabAlignment.Left:
                    tabBounds.Width += 2;
                    break;

                case TabAlignment.Right:
                    tabBounds.X -= 2;
                    tabBounds.Width += 2;
                    break;
            }

            //	Greate Overlap unless first tab in the row to align with tabpage
            if ((!firstTabinRow || m_TabControl.RightToLeftLayout) && m_Overlap > 0)
            {
                if (m_TabControl.Alignment <= TabAlignment.Bottom)
                {
                    tabBounds.X -= m_Overlap;
                    tabBounds.Width += m_Overlap;
                }
                else
                {
                    tabBounds.Y -= m_Overlap;
                    tabBounds.Height += m_Overlap;
                }
            }

            //	Adjust first tab in the row to align with tabpage
            EnsureFirstTabIsInView(ref tabBounds, index);

            return tabBounds;
        }

        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        protected virtual void EnsureFirstTabIsInView(ref Rectangle tabBounds, int index)
        {
            //	Adjust first tab in the row to align with tabpage
            //	Make sure we only reposition visible tabs, as we may have scrolled out of view.

            var firstTabinRow = m_TabControl.IsFirstTabInRow(index);

            if (firstTabinRow)
            {
                if (m_TabControl.Alignment <= TabAlignment.Bottom)
                {
                    if (m_TabControl.RightToLeftLayout)
                    {
                        if (tabBounds.Left < m_TabControl.Right)
                        {
                            var tabPageRight = m_TabControl.GetPageBounds(index).Right;
                            if (tabBounds.Right > tabPageRight)
                            {
                                tabBounds.Width -= (tabBounds.Right - tabPageRight);
                            }
                        }
                    }
                    else
                    {
                        if (tabBounds.Right > 0)
                        {
                            var tabPageX = m_TabControl.GetPageBounds(index).X;
                            if (tabBounds.X < tabPageX)
                            {
                                tabBounds.Width -= (tabPageX - tabBounds.X);
                                tabBounds.X = tabPageX;
                            }
                        }
                    }
                }
                else
                {
                    if (m_TabControl.RightToLeftLayout)
                    {
                        if (tabBounds.Top < m_TabControl.Bottom)
                        {
                            var tabPageBottom = m_TabControl.GetPageBounds(index).Bottom;
                            if (tabBounds.Bottom > tabPageBottom)
                            {
                                tabBounds.Height -= (tabBounds.Bottom - tabPageBottom);
                            }
                        }
                    }
                    else
                    {
                        if (tabBounds.Bottom > 0)
                        {
                            var tabPageY = m_TabControl.GetPageBounds(index).Location.Y;
                            if (tabBounds.Y < tabPageY)
                            {
                                tabBounds.Height -= (tabPageY - tabBounds.Y);
                                tabBounds.Y = tabPageY;
                            }
                        }
                    }
                }
            }
        }

        protected virtual Brush GetTabBackgroundBrush(int index)
        {
            LinearGradientBrush fillBrush = null;

            //	Capture the colours dependant on selection state of the tab
            var dark = Color.FromArgb(207, 207, 207);
            var light = Color.FromArgb(242, 242, 242);

            if (m_TabControl.SelectedIndex == index)
            {
                dark = SystemColors.ControlLight;
                light = SystemColors.Window;
            }
            else if (!m_TabControl.TabPages[index].Enabled)
            {
                light = dark;
            }
            else if (m_HotTrack && index == m_TabControl.ActiveIndex)
            {
                //	Enable hot tracking
                light = Color.FromArgb(234, 246, 253);
                dark = Color.FromArgb(167, 217, 245);
            }

            //	Get the correctly aligned gradient
            var tabBounds = GetTabRect(index);
            tabBounds.Inflate(3, 3);
            tabBounds.X -= 1;
            tabBounds.Y -= 1;
            switch (m_TabControl.Alignment)
            {
                case TabAlignment.Top:
                    if (m_TabControl.SelectedIndex == index)
                    {
                        dark = light;
                    }
                    fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Vertical);
                    break;

                case TabAlignment.Bottom:
                    fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Vertical);
                    break;

                case TabAlignment.Left:
                    fillBrush = new LinearGradientBrush(tabBounds, dark, light, LinearGradientMode.Horizontal);
                    break;

                case TabAlignment.Right:
                    fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Horizontal);
                    break;
            }

            //	Add the blend
            if (fillBrush != null)
            {
                fillBrush.Blend = GetBackgroundBlend();
            }

            return fillBrush;
        }

        #endregion

        #region	Base Properties

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TabStyle DisplayStyle
        {
            get { return m_Style; }
            set { m_Style = value; }
        }

        [Category("Appearance")]
        public ContentAlignment ImageAlign
        {
            get { return m_ImageAlign; }
            set
            {
                m_ImageAlign = value;
                m_TabControl.Invalidate();
            }
        }

        [Category("Appearance")]
        public Point Padding
        {
            get { return m_Padding; }
            set
            {
                m_Padding = value;

                //	This line will trigger the handle to recreate, therefore invalidating the control
                if (m_ShowTabCloser)
                {
                    if (value.X + m_Radius / 2 < -6)
                    {
                        ((TabControl)m_TabControl).Padding = new Point(0, value.Y);
                    }
                    else
                    {
                        ((TabControl)m_TabControl).Padding = new Point(value.X + m_Radius / 2 + 6, value.Y);
                    }
                }
                else
                {
                    if (value.X + m_Radius / 2 < 1)
                    {
                        ((TabControl)m_TabControl).Padding = new Point(0, value.Y);
                    }
                    else
                    {
                        ((TabControl)m_TabControl).Padding = new Point(value.X + m_Radius / 2 - 1, value.Y);
                    }
                }
            }
        }

        [Category("Appearance"), DefaultValue(1), Browsable(true)]
        public int Radius
        {
            get { return m_Radius; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException(Resources.TabStyleProvider_Radius_The_radius_must_be_greater_than_1, "value");
                }
                m_Radius = value;

                //	Adjust padding
                Padding = m_Padding;
            }
        }

        [Category("Appearance")]
        public int Overlap
        {
            get { return m_Overlap; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(Resources.TabStyleProvider_Overlap_The_tabs_cannot_have_a_negative_overlap, "value");
                }
                m_Overlap = value;
            }
        }

        [Category("Appearance")]
        public bool FocusTrack
        {
            get { return m_FocusTrack; }
            set
            {
                m_FocusTrack = value;
                m_TabControl.Invalidate();
            }
        }

        [Category("Appearance")]
        public bool HotTrack
        {
            get { return m_HotTrack; }
            set
            {
                m_HotTrack = value;
                ((TabControl)m_TabControl).HotTrack = value;
            }
        }

        [Category("Appearance")]
        public bool ShowTabCloser
        {
            get { return m_ShowTabCloser; }
            set
            {
                m_ShowTabCloser = value;

                //	Adjust padding
                Padding = m_Padding;
            }
        }

        [Category("Appearance")]
        public float Opacity
        {
            get { return m_Opacity; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(Resources.TabStyleProvider_Opacity_The_opacity_must_be_between_0_and_1, "value");
                }
                if (value > 1)
                {
                    throw new ArgumentException(Resources.TabStyleProvider_Opacity_The_opacity_must_be_between_0_and_1, "value");
                }
                m_Opacity = value;
                m_TabControl.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColorSelected
        {
            get
            {
                if (m_BorderColorSelected.IsEmpty)
                {
                    return ThemedColors.ToolBorder;
                }
                return m_BorderColorSelected;
            }
            set
            {
                if (value.Equals(ThemedColors.ToolBorder))
                {
                    m_BorderColorSelected = Color.Empty;
                }
                else
                {
                    m_BorderColorSelected = value;
                }
                m_TabControl.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColorHot
        {
            get
            {
                if (m_BorderColorHot.IsEmpty)
                {
                    return SystemColors.ControlDark;
                }
                return m_BorderColorHot;
            }
            set
            {
                if (value.Equals(SystemColors.ControlDark))
                {
                    m_BorderColorHot = Color.Empty;
                }
                else
                {
                    m_BorderColorHot = value;
                }
                m_TabControl.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColor
        {
            get
            {
                if (m_BorderColor.IsEmpty)
                {
                    return SystemColors.ControlDark;
                }
                return m_BorderColor;
            }
            set
            {
                if (value.Equals(SystemColors.ControlDark))
                {
                    m_BorderColor = Color.Empty;
                }
                else
                {
                    m_BorderColor = value;
                }
                m_TabControl.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColor
        {
            get
            {
                if (m_TextColor.IsEmpty)
                {
                    return SystemColors.ControlText;
                }
                return m_TextColor;
            }
            set
            {
                if (value.Equals(SystemColors.ControlText))
                {
                    m_TextColor = Color.Empty;
                }
                else
                {
                    m_TextColor = value;
                }
                m_TabControl.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorSelected
        {
            get
            {
                if (m_TextColorSelected.IsEmpty)
                {
                    return SystemColors.ControlText;
                }
                return m_TextColorSelected;
            }
            set
            {
                if (value.Equals(SystemColors.ControlText))
                {
                    m_TextColorSelected = Color.Empty;
                }
                else
                {
                    m_TextColorSelected = value;
                }
                m_TabControl.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorDisabled
        {
            get
            {
                if (m_TextColor.IsEmpty)
                {
                    return SystemColors.ControlDark;
                }
                return m_TextColorDisabled;
            }
            set
            {
                if (value.Equals(SystemColors.ControlDark))
                {
                    m_TextColorDisabled = Color.Empty;
                }
                else
                {
                    m_TextColorDisabled = value;
                }
                m_TabControl.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "Orange")]
        public Color FocusColor
        {
            get { return m_FocusColor; }
            set
            {
                m_FocusColor = value;
                m_TabControl.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "Black")]
        public Color CloserColorActive
        {
            get { return m_CloserColorActive; }
            set
            {
                m_CloserColorActive = value;
                m_TabControl.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "DarkGrey")]
        public Color CloserColor
        {
            get { return m_CloserColor; }
            set
            {
                m_CloserColor = value;
                m_TabControl.Invalidate();
            }
        }

        #endregion

        #region Painting

        public bool IsTabPinned(int index)
        {
            return m_TabControl.TabPages[index] is INonCloseableTabPage;
        }

        public void PaintTab(int index, Graphics graphics)
        {
            using (var tabpath = GetTabBorder(index))
            {
                using (var fillBrush = GetTabBackgroundBrush(index))
                {
                    //	Paint the background
                    graphics.FillPath(fillBrush, tabpath);

                    //	Paint a focus indication
                    if (m_TabControl.Focused)
                    {
                        DrawTabFocusIndicator(tabpath, index, graphics);
                    }

                    //	Paint the closer
                    DrawTabCloser(index, graphics);
                }
            }
        }

        protected virtual void DrawTabCloser(int index, Graphics graphics)
        {
            if (m_ShowTabCloser)
            {
                var closerRect = m_TabControl.GetTabCloserRect(index);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var closerPath = GetCloserPath(closerRect))
                {
                    if (closerRect.Contains(m_TabControl.MousePosition))
                    {
                        using (var closerPen = new Pen(m_CloserColorActive))
                        {
                            graphics.DrawPath(closerPen, closerPath);
                        }
                    }
                    else
                    {
                        using (var closerPen = new Pen(m_CloserColor))
                        {
                            graphics.DrawPath(closerPen, closerPath);
                        }
                    }
                }
            }
        }

        protected static GraphicsPath GetCloserPath(Rectangle closerRect)
        {
            var closerPath = new GraphicsPath();
            closerPath.AddLine(closerRect.X, closerRect.Y, closerRect.Right, closerRect.Bottom);
            closerPath.CloseFigure();
            closerPath.AddLine(closerRect.Right, closerRect.Y, closerRect.X, closerRect.Bottom);
            closerPath.CloseFigure();

            return closerPath;
        }

        private void DrawTabFocusIndicator(GraphicsPath tabpath, int index, Graphics graphics)
        {
            if (m_FocusTrack && m_TabControl.Focused && index == m_TabControl.SelectedIndex)
            {
                Brush focusBrush = null;
                var pathRect = tabpath.GetBounds();
                var focusRect = Rectangle.Empty;
                switch (m_TabControl.Alignment)
                {
                    case TabAlignment.Top:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Y, (int)pathRect.Width, 4);
                        focusBrush = new LinearGradientBrush(focusRect, m_FocusColor, SystemColors.Window, LinearGradientMode.Vertical);
                        break;

                    case TabAlignment.Bottom:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Bottom - 4, (int)pathRect.Width, 4);
                        focusBrush = new LinearGradientBrush(focusRect, SystemColors.ControlLight, m_FocusColor, LinearGradientMode.Vertical);
                        break;

                    case TabAlignment.Left:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Y, 4, (int)pathRect.Height);
                        focusBrush = new LinearGradientBrush(focusRect, m_FocusColor, SystemColors.ControlLight, LinearGradientMode.Horizontal);
                        break;

                    case TabAlignment.Right:
                        focusRect = new Rectangle((int)pathRect.Right - 4, (int)pathRect.Y, 4, (int)pathRect.Height);
                        focusBrush = new LinearGradientBrush(focusRect, SystemColors.ControlLight, m_FocusColor, LinearGradientMode.Horizontal);
                        break;
                }

                //	Ensure the focus stip does not go outside the tab
                var focusRegion = new Region(focusRect);
                focusRegion.Intersect(tabpath);
                if (focusBrush != null)
                {
                    graphics.FillRegion(focusBrush, focusRegion);
                    focusRegion.Dispose();
                    focusBrush.Dispose();
                }
            }
        }

        #endregion

        #region Background brushes

        private Blend GetBackgroundBlend()
        {
            var relativeIntensities = new[] { 0f, 0.7f, 1f };
            var relativePositions = new[] { 0f, 0.6f, 1f };

            //	Glass look to top aligned tabs
            if (m_TabControl.Alignment == TabAlignment.Top)
            {
                relativeIntensities = new[] { 0f, 0.5f, 1f, 1f };
                relativePositions = new[] { 0f, 0.5f, 0.51f, 1f };
            }

            var blend = new Blend();
            blend.Factors = relativeIntensities;
            blend.Positions = relativePositions;

            return blend;
        }

        public virtual Brush GetPageBackgroundBrush(int index)
        {
            //	Capture the colours dependant on selection state of the tab
            var light = Color.FromArgb(242, 242, 242);
            if (m_TabControl.Alignment == TabAlignment.Top)
            {
                light = Color.FromArgb(207, 207, 207);
            }

            if (m_TabControl.SelectedIndex == index)
            {
                light = SystemColors.Window;
            }
            else if (!m_TabControl.TabPages[index].Enabled)
            {
                light = Color.FromArgb(207, 207, 207);
            }
            else if (m_HotTrack && index == m_TabControl.ActiveIndex)
            {
                //	Enable hot tracking
                light = Color.FromArgb(234, 246, 253);
            }

            return new SolidBrush(light);
        }

        #endregion

        #region Tab border and rect

        public GraphicsPath GetTabBorder(int index)
        {
            var path = new GraphicsPath();
            var tabBounds = GetTabRect(index);

            AddTabBorder(path, tabBounds);

            path.CloseFigure();
            return path;
        }

        #endregion
    }
}