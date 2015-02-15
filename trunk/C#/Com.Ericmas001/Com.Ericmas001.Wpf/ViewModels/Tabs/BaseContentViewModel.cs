using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using Com.Ericmas001.Util;

namespace Com.Ericmas001.Wpf.ViewModels.Tabs
{
    public abstract class BaseContentViewModel : BaseTabViewModel
    {
        private readonly LoadingViewModel m_LoadingDataVm;

        public LoadingViewModel LoadingDataVm
        {
            get { return m_LoadingDataVm; }
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

        public BaseContentViewModel( Dispatcher appCurrentDispatcher )
        {
            m_LoadingDataVm = new LoadingViewModel(appCurrentDispatcher, ObtainData)
            {
                BigLoadingMessage = this.BigLoadingMessage,
                SmallLoadingMessage = this.SmallLoadingMessage
            };
            m_LoadingDataVm.OnDataObtained += RefreshInterface;
            m_LoadingDataVm.OnErrorObtained += m_LoadingDataVm_OnErrorObtained;
        }

        void m_LoadingDataVm_OnErrorObtained(object sender, Util.KeyEventArgs<Exception> e)
        {
            Logs.LogError(e.Key.ToString());
        }

        public virtual void Init()
        {
            
        }

        protected abstract void RefreshInterface();

        protected abstract void ObtainData(object sender, DoWorkEventArgs e);

        protected void RefreshDataAndInterface()
        {
            LoadingDataVm.Execute();
        }
    }
}
