using System;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Util.Entities.Filters;
using Com.Ericmas001.Wpf.ViewModels;

namespace Com.Ericmas001.Wpf.Entities.Filters
{
    public abstract class BaseCompiledFilter : BaseViewModel
    {
        public event EventHandler RemoveMeAsAFilter;
        public event EventHandler UpdateAFilter;

        private FilterInfo m_Info;
        private bool m_IsActive = true;

        public bool IsActive
        {
            get { return m_IsActive; }
            set
            {
                if (value != m_IsActive)
                {
                    m_IsActive = value;
                    if (UpdateAFilter != null)
                        UpdateAFilter(this, new EventArgs());
                    RaisePropertyChanged("IsActive");
                }
            }
        }

        private RelayCommand m_DeleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return m_DeleteCommand ?? (m_DeleteCommand = new RelayCommand(x => RemoveFilter())); }
        }

        private void RemoveFilter()
        {
            if (RemoveMeAsAFilter != null)
                RemoveMeAsAFilter(this, new EventArgs());
        }

        public BaseCompiledFilter(FilterInfo info)
        {
            m_Info = info;
        }

        public string Description
        {
            get { return m_Info.ToString(); }
        }

        public FilterInfo Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }

        public override string ToString()
        {
            return Info.ToString();
        }

        public bool IsSurvivingTheFilter(string value, IDataItem item)
        {

            if (!m_IsActive)
            {
                return true;
            }

            return CheckIfIsSurvivingTheFilter(value, item);
        }

        protected abstract bool CheckIfIsSurvivingTheFilter(string value, IDataItem item);
    }
}
