using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Threading;
using Com.Ericmas001.Wpf.Validations;

namespace Com.Ericmas001.Wpf.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public event EventHandler OnRequestClose = delegate { };

        public virtual string Error
        {
            get { return null; }
        }

        public string this[string propertyName]
        {
            get
            {
                PropertyInfo prop = GetType().GetProperty(propertyName);
                IEnumerable<string> simples = prop.GetCustomAttributes(true).OfType<SimpleValidationAttribute>().Select(att => att.Validate(prop.GetValue(this, null) as String));
                IEnumerable<string> customs = prop.GetCustomAttributes(true).OfType<CustomValidationAttribute>().Select(att => att.Validate(this, prop.GetValue(this, null) as String));

                return simples.Union(customs).FirstOrDefault(msg => !String.IsNullOrEmpty(msg));
            }
        }

        public virtual bool IsAllInputsValidated()
        {
            IEnumerable<bool> subModels = GetType()
                .GetProperties()
                .Where(pi => typeof(BaseViewModel).IsAssignableFrom(pi.PropertyType) && pi.GetValue(this, null) != null)
                .Select(pi => ((BaseViewModel)pi.GetValue(this, null)).IsAllInputsValidated());

            return GetType().GetProperties().All(x => String.IsNullOrEmpty(this[x.Name]) && subModels.All(b => b));
        }

        protected string ExecuteSimpleValidations(string value, params SimpleValidationAttribute[] validations)
        {
            return validations.Select(x => x.Validate(value)).FirstOrDefault(x => !String.IsNullOrEmpty(x));
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void CloseWindow()
        {
            OnRequestClose(this,new EventArgs());
        }

        public static TViewModel ShowDialog<TViewModel, TWindow>()
            where TViewModel : BaseViewModel
            where TWindow : Window, new()
        {
            var window = new TWindow();
            var vm = window.DataContext as TViewModel;
            vm.OnRequestClose += (s, e) => window.Close();
            window.ShowDialog();
            return vm;
        }

        public static TViewModel ShowDialogSTA<TViewModel, TWindow>()
            where TViewModel : BaseViewModel
            where TWindow : Window, new()
        {
            TViewModel vm = null;
            Thread viewerThread = new Thread(delegate() { vm = BaseViewModel.ShowDialog<TViewModel, TWindow>(); });
            viewerThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            viewerThread.Start();
            viewerThread.Join();
            return vm;
        }
    }
}
