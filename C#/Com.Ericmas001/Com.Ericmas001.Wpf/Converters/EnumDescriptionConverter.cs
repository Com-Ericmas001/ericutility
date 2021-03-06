﻿using System;
using System.Windows.Data;
using Com.Ericmas001.Portable.Util;

namespace Com.Ericmas001.Wpf.Converters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString()))
                return false;
            try
            {
                return typeof(EnumFactory<>).MakeGenericType(value.GetType()).GetMethod("ToString", new Type[] { value.GetType() }).Invoke(null, new object[] { value }).ToString();
            }
            catch
            {
                return value.ToString();
            }
        }


        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType,
          object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
