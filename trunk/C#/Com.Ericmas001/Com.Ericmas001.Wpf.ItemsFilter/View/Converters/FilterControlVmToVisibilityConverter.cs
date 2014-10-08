using Com.Ericmas001.Wpf.ItemsFilter.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Com.Ericmas001.Wpf.ItemsFilter.View
{
    [ValueConversion(typeof(System.Windows.Visibility), typeof(FilterControlVm))]
    public class FilterControlVmToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value is FilterControlVm) {
                FilterControlVm vm = (FilterControlVm)value;
                return vm.IsEnable ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }
            else
                return
                    System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
