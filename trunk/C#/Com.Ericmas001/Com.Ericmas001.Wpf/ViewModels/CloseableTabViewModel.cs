using System.Windows.Input;

namespace Com.Ericmas001.Wpf.ViewModels
{
    public class CloseableTabViewModel : BaseViewModel
    {
        private RelayCommand m_CloseTabCommand;

        public string TabName { get; private set; }

        public ICommand CloseTabCommand
        {
            get
            {
                if (m_CloseTabCommand == null)
                    m_CloseTabCommand = new RelayCommand(p => CloseWindow());
                return m_CloseTabCommand;
            }
        }

        public CloseableTabViewModel(string header)
        {
            TabName = header;
        }
    }
}
