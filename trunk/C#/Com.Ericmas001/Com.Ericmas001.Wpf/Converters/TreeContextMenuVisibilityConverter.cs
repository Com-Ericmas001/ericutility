using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.Wpf.Converters
{
    public class TreeContextMenuVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = value as IEnumerable<TreeContextMenuItemViewModel>;
            if (items == null) return Visibility.Visible;

            if (items.Any(x => x.ExecuteCommand.CanExecute(null)))
                return Visibility.Visible;

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
