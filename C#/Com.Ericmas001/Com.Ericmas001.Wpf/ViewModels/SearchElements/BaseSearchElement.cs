using System;
using System.Windows.Input;

namespace Com.Ericmas001.Wpf.ViewModels.SearchElements
{
    public abstract class BaseSearchElement : BaseViewModel 
    {
        public event EventHandler ValueSubmitted = delegate { };

        public abstract string TextValue { get; }

        private RelayCommand m_SubmitValue;
        public ICommand SubmitValue { get { return m_SubmitValue ?? (m_SubmitValue = new RelayCommand(p => ValueSubmitted(this,new EventArgs()), p => IsAllInputsValidated())); } }

        public BaseSearchElement()
        {
        }
    }
}
