using System;
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

        public virtual string TabTitle { get { return null; } }

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

        public string Title { get { return !string.IsNullOrEmpty(TabTitle) ? TabTitle : TabHeader; } }

        public bool HasHeaderText { get { return !string.IsNullOrEmpty(TabHeader); } }
        public bool HasHeaderIcon { get { return TabIcon != null; } }

        public bool HasTitle { get { return !string.IsNullOrEmpty(Title); } }
        public bool HasTitleImage { get { return !string.IsNullOrEmpty(IconBigImageName); } }

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
