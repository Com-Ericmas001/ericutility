using System;
using System.Globalization;
using System.Windows.Data;

namespace Com.Ericmas001.Wpf.Converters
{
    public class EnumMatchToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;
            return value.ToString().Equals(parameter.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;
            return (bool)value ? Enum.Parse(targetType, parameter.ToString()) : null;
        }
    }
}
