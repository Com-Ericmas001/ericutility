using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Com.Ericmas001.Wpf.ViewModels;

namespace Com.Ericmas001.Wpf
{
    public static class WpfWindowManager
    {
        public static T Show<T>(this Window window) where T : BaseViewModel
        {
            T vm = window.DataContext as T;
            vm.OnRequestClose += (s, e) => window.Close();
            window.Show();
            return vm;
        }
        public static T ShowDialog<T>(this Window window) where T : BaseViewModel
        {
            T vm = window.DataContext as T;
            vm.OnRequestClose += (s, e) => window.Close();
            window.ShowDialog();
            return vm;
        }
    }
}
