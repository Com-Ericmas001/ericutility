using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Com.Ericmas001.Wpf
{
    public class DisplayList<T> : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private ObservableCollection<T> m_Items = new ObservableCollection<T>();
        private int m_SelectedIndex;
        private T m_Selected;

        public ObservableCollection<T> Items
        {
            get { return m_Items; }
            set
            {
                m_Items = value;
                RaisePropertyChanged("Items");
            }
        }

        public int SelectedIndex
        {
            get { return m_SelectedIndex; }
            set
            {
                m_SelectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }

        public T Selected
        {
            get { return m_Selected; }
            set
            {
                m_Selected = value;
                RaisePropertyChanged("Selected");
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
