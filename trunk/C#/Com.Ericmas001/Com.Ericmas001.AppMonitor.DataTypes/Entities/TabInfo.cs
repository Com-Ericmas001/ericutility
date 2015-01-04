﻿using System;
using System.Windows;
using System.Windows.Media;
using Com.Ericmas001.AppMonitor.DataTypes.Attributes;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Attributes;
using Com.Ericmas001.Wpf.Helpers;

namespace Com.Ericmas001.AppMonitor.DataTypes.Entities
{
    public class TabInfo
    {
        public TabInfo()
        {
            IconImageSmallName = String.Empty;
            IconImageBigName = String.Empty;
            Background = Brushes.Gray;
            ButtonBrush = Colors.DarkSlateGray;
            Description = String.Empty;
            Priorite = 100;
        }

        public string IconImageSmallName { get; set; }

        public string IconImageBigName { get; set; }

        public SolidColorBrush Background { get; set; }

        public Color ButtonBrush { get; set; }

        public string Description { get; set; }

        public int Priorite { get; set; }

        public SolidColorBrush HeaderForeground
        {
            get { return ColorHelper.GetForegroundFromBackground(Background); }
        }

        public virtual ImageSource IconImageSmall
        {
            get { return String.IsNullOrEmpty(IconImageSmallName) ? null : Application.Current.FindResource(IconImageSmallName) as ImageSource; }
        }
        public virtual ImageSource IconImageBig
        {
            get { return String.IsNullOrEmpty(IconImageBigName) ? null : Application.Current.FindResource(IconImageBigName) as ImageSource; }
        }
    }
}
