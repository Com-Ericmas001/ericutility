using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Com.Ericmas001.Wpf.ViewModels.Trees
{
    public abstract class TreeContextMenuItemViewModel : BaseViewModel
    {
        protected TreeElementViewModel Element { get; private set; }

        public abstract string Text { get; }

        public virtual ImageSource Icon
        {
            get { return String.IsNullOrEmpty(IconImageName) ? null : Application.Current.FindResource(IconImageName) as ImageSource; }
        }
        public virtual string IconImageName { get { return null; } }

        private RelayCommand m_ExecuteCommand;
        public ICommand ExecuteCommand { get { return m_ExecuteCommand ?? (m_ExecuteCommand = new RelayCommand(p => Execute(), p => CanExecute())); } }

        public TreeContextMenuItemViewModel(TreeElementViewModel element)
        {
            Element = element;
        }

        protected abstract void Execute();

        protected abstract bool CanExecute();
    }
}
