using System.Windows.Input;

namespace Com.Ericmas001.Wpf.ViewModels.Sections
{
    public abstract class ActionButtonSection : TabSection
    {
        private RelayCommand m_ExecuteCommand;
        public ICommand ExecuteCommand
        {
            get { return m_ExecuteCommand ?? (m_ExecuteCommand = new RelayCommand(x => CreateNewTab(CreateContentTab()))); }
        }
    }
}
