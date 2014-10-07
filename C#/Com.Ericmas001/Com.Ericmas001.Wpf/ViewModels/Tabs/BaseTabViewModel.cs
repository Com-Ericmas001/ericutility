using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace Com.Ericmas001.Wpf.ViewModels.Tabs
{
    public delegate void NewTabEventHandler(object sender, BaseTabViewModel tab);
    public class BaseTabViewModel : BaseViewModel
    {
        public event NewTabEventHandler OnTabCreation = delegate { };

        private RelayCommand m_CloseTabCommand;

        public virtual string TabHeader { get { return null; } }

        protected virtual string IconImageName { get { return null; } }

        public virtual ImageSource TabIcon
        {
            get { return String.IsNullOrEmpty(IconImageName) ? null : Application.Current.FindResource(IconImageName) as ImageSource; }
        }

        protected virtual string IconBigImageName { get { return null; } }

        public virtual ImageSource TabIconBig
        {
            get { return String.IsNullOrEmpty(IconBigImageName) ? null : Application.Current.FindResource(IconBigImageName) as ImageSource; }
        }

        public virtual bool CanCloseTab { get { return false; } }

        public bool HasHeaderText { get { return !String.IsNullOrEmpty(TabHeader); } }
        public bool HasHeaderIcon { get { return TabIcon != null; } }

        public ICommand CloseTabCommand
        {
            get
            {
                if (m_CloseTabCommand == null)
                    m_CloseTabCommand = new RelayCommand(p => CloseView(), p => CanCloseTab);
                return m_CloseTabCommand;
            }
        }

        public void CreateNewTab(BaseTabViewModel tab)
        {
            OnTabCreation(this, tab);
        }
    }
}
