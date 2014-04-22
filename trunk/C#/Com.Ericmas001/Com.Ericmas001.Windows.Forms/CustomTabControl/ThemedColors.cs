/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
*/

using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Com.Ericmas001.Windows.Forms.CustomTabControl
{
    internal sealed class ThemedColors
    {
        #region "    Variables and Constants "

        private const string NORMAL_COLOR = "NormalColor";
        private const string HOME_STEAD = "HomeStead";
        private const string METALLIC = "Metallic";

        private static readonly Color[] m_ToolBorder;

        #endregion "    Variables and Constants "

        #region "    Properties "

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static ColorScheme CurrentThemeIndex
        {
            get { return GetCurrentThemeIndex(); }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static Color ToolBorder
        {
            get { return m_ToolBorder[(int)CurrentThemeIndex]; }
        }

        #endregion "    Properties "

        #region "    Constructors "

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ThemedColors()
        {
            m_ToolBorder = new[] { Color.FromArgb(127, 157, 185), Color.FromArgb(164, 185, 127), Color.FromArgb(165, 172, 178), Color.FromArgb(132, 130, 132) };
        }

        private ThemedColors()
        {
        }

        #endregion "    Constructors "

        private static ColorScheme GetCurrentThemeIndex()
        {
            var theme = ColorScheme.NoTheme;

            if (VisualStyleInformation.IsSupportedByOS && VisualStyleInformation.IsEnabledByUser && Application.RenderWithVisualStyles)
            {
                switch (VisualStyleInformation.ColorScheme)
                {
                    case NORMAL_COLOR:
                        theme = ColorScheme.NormalColor;
                        break;

                    case HOME_STEAD:
                        theme = ColorScheme.HomeStead;
                        break;

                    case METALLIC:
                        theme = ColorScheme.Metallic;
                        break;

                    default:
                        theme = ColorScheme.NoTheme;
                        break;
                }
            }

            return theme;
        }

        public enum ColorScheme
        {
            NormalColor = 0,
            HomeStead = 1,
            Metallic = 2,
            NoTheme = 3
        }
    }
}