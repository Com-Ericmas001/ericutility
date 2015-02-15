using Com.Ericmas001.Wpf.ViewModels;

namespace Com.Ericmas001.Wpf.Entities
{
    public class CheckListItem : BaseViewModel
    {
        private string m_Name;
        private bool m_IsSelected;
        private object m_Value;

        public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public bool IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                m_IsSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        public object Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        public CheckListItem(string name, object value, bool isSelected = false)
        {
            m_Name = name;
            m_IsSelected = isSelected;
            m_Value = value;
        }
    }
}
