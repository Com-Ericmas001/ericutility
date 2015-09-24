using System;
using Com.Ericmas001.Portable.Util.Entities;
using Com.Ericmas001.Portable.Util.Entities.Filters;
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
        public virtual bool IsReadOnly
        {
            get { return false; }
        }
        public bool IsDeletable
        {
            get { return !IsReadOnly; }
        }

        private RelayCommand m_DeleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return m_DeleteCommand ?? (m_DeleteCommand = new RelayCommand(x => RemoveFilter(), x => IsDeletable)); }
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

        public BaseCompiledFilter()
        {
        }

        public string Description
        {
            get { return ToString(); }
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
