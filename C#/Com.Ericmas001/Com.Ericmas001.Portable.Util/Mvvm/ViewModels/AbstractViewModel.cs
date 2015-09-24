using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Portable.Util.Mvvm.ViewModels
{
    public class AbstractViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public event EventHandler OnRequestClose = delegate { };

        public virtual string Error
        {
            get { return null; }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void CloseView()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}
