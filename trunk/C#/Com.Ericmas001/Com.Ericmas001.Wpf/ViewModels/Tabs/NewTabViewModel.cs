using System.Windows.Input;

namespace Com.Ericmas001.Wpf.ViewModels.Tabs
{
    public abstract class NewTabViewModel : BaseTabViewModel
    {
        protected override string IconImageName
        {
            get
            {
                return "Add";
            }
        }

        public abstract BaseTabViewModel CreateContentTab();

        private RelayCommand m_StartNewTabCommand;
        public ICommand StartNewTabCommand { get { return m_StartNewTabCommand ?? (m_StartNewTabCommand = new RelayCommand(x => CreateNewTab(CreateContentTab()), x => IsAllInputsValidated())); } }

    }
}
