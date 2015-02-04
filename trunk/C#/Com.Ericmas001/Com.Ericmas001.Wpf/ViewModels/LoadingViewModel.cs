using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using Com.Ericmas001.Util;

namespace Com.Ericmas001.Wpf.ViewModels
{
    public class LoadingViewModel : BaseViewModel
    {
        public event EmptyHandler OnDataObtained = delegate { };
        public event EventHandler<KeyEventArgs<Exception>> OnErrorObtained = delegate { };

        private Dispatcher m_AppCurrentDispatcher;
        private bool m_IsLoading;
        private BackgroundWorker m_Bw = new BackgroundWorker();
        private string m_BigLoadingMessage = "Loading Data ...";
        private string m_SmallLoadingMessage = "Fetching data from Database ...";

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

        public string BigLoadingMessage
        {
            get { return m_BigLoadingMessage; }
            set
            {
                m_BigLoadingMessage = value;
                RaisePropertyChanged("BigLoadingMessage");
            }
        }

        public string SmallLoadingMessage
        {
            get { return m_SmallLoadingMessage; }
            set
            {
                m_SmallLoadingMessage = value;
                RaisePropertyChanged("SmallLoadingMessage");
            }
        }
        public LoadingViewModel( Dispatcher appCurrentDispatcher, DoWorkEventHandler fnctObtainData )
        {
            m_AppCurrentDispatcher = appCurrentDispatcher;
            m_Bw.DoWork += fnctObtainData;
            m_Bw.RunWorkerCompleted += DataObtained;
        }

        private void DataObtained(object sender, RunWorkerCompletedEventArgs e)
        {
            IsLoading = false;
            if (e.Error != null)
                OnErrorObtained(this, new KeyEventArgs<Exception>(e.Error));
            RefreshInterfaceAsync();
        }


        private void RefreshInterfaceAsync()
        {
            m_AppCurrentDispatcher.Invoke(new Action(RefreshInterface));
        }

        private void RefreshInterface()
        {
            OnDataObtained();
        }

        public void Execute()
        {
            if (!m_Bw.IsBusy)
            {
                IsLoading = true;
                m_Bw.RunWorkerAsync();
            }
        }
    }
}
