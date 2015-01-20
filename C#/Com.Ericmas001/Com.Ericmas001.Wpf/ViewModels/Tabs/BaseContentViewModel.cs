using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace Com.Ericmas001.Wpf.ViewModels.Tabs
{
    public abstract class BaseContentViewModel : BaseTabViewModel
    {

        private bool m_IsLoading;
        private BackgroundWorker m_Bw = new BackgroundWorker();
        private readonly Dispatcher m_AppCurrentDispatcher;

        public bool IsLoading
        {
            get { return m_IsLoading; }
            set
            {
                if ((m_IsLoading != value))
                {
                    m_IsLoading = value;
                    RaisePropertyChanged("IsLoading");
                }
            }
        }

        public virtual string BigLoadingMessage
        {
            get { return "Loading Data ..."; }
        }
        public virtual string SmallLoadingMessage
        {
            get { return "Fetching data from Database ..."; }
        }

        public virtual bool CanRefresh
        {
            get { return true; }
        }

        private RelayCommand m_RefreshCommand;
        public ICommand RefreshCommand
        {
            get { return m_RefreshCommand ?? (m_RefreshCommand = new RelayCommand(x => RefreshDataAndInterface(), x => CanRefresh)); }
        }

        protected Dispatcher AppCurrentDispatcher
        {
            get { return m_AppCurrentDispatcher; }
        }

        public BaseContentViewModel( Dispatcher appCurrentDispatcher )
        {
            m_AppCurrentDispatcher = appCurrentDispatcher;
            m_Bw.DoWork += ObtainData;
            m_Bw.RunWorkerCompleted += DataObtained;
        }

        public virtual void Init()
        {
            
        }

        protected abstract void RefreshInterface();

        protected abstract void ObtainData(object sender, DoWorkEventArgs e);

        protected virtual void RefreshDataAndInterface()
        {
            IsLoading = true;
            m_Bw.RunWorkerAsync();
        }

        protected virtual void DataObtained(object sender, RunWorkerCompletedEventArgs e)
        {
            IsLoading = false;
            RefreshInterfaceAsync();
        }

        protected void RefreshInterfaceAsync()
        {
            AppCurrentDispatcher.Invoke(new Action(RefreshInterface));
        }
    }
}
