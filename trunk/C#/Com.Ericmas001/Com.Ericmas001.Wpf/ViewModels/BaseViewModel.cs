using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Wpf.ViewModels
{
    public class BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public event EventHandler OnRequestClose = delegate { };

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
