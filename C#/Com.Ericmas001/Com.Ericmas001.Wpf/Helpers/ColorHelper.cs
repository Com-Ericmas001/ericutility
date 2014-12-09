using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Com.Ericmas001.Wpf.Helpers
{
    public class ColorHelper
    {
        public static int PerceivedBrightness(Color c)
        {
            return (int)Math.Sqrt(
            c.R * c.R * .299 +
            c.G * c.G * .587 +
            c.B * c.B * .114);
        }
        public static SolidColorBrush GetForegroundFromBackground(SolidColorBrush background)
        {
            return PerceivedBrightness(background.Color) > 130 ? Brushes.Black : Brushes.White;
        }
    }
} 
