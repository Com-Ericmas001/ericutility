using System;
using System.Collections.ObjectModel;

namespace Com.Ericmas001.Wpf.ViewModels.Tabs
{
    public abstract class TabControlViewModel: BaseViewModel
    {
        protected virtual NewTabViewModel CreateNewTab() 
        { 
            return null; 
        }
        protected virtual bool KeepNewTab
        {
            get
            {
                return false;
            }
        }

        public ObservableCollection<BaseTabViewModel> Tabs { get; private set; }
        private NewTabViewModel m_NewTab;

        private BaseTabViewModel m_SelectedTab;
        public BaseTabViewModel SelectedTab
        {
            get { return m_SelectedTab; }
            set
            {
                m_SelectedTab = value;
                RaisePropertyChanged("SelectedTab");
            }
        }

        public TabControlViewModel()
        {
            Tabs = new ObservableCollection<BaseTabViewModel>();
            AddNewTab();
        }

        public void AddTab(BaseTabViewModel tab)
        {
            if (tab != null)
            {
                if (m_NewTab != null)
                    Tabs.Remove(m_NewTab);

                tab.OnTabCreation += (s, t) => AddTab(t);
                tab.OnRequestClose += OnTabClosed;
                Tabs.Add(tab);
                SelectedTab = tab;

                AddNewTab();
            }
        }

        private void AddNewTab()
        {
            bool mustAdd = (!KeepNewTab || m_NewTab == null);
            if (mustAdd)
                m_NewTab = CreateNewTab();

            if (m_NewTab != null)
            {
                if (mustAdd)
                    m_NewTab.OnTabCreation += (s, t) => AddTab(t);
                Tabs.Add(m_NewTab);
            }
        }
        public void OnTabClosed(object sender, EventArgs e)
        {
            var tab = sender as BaseTabViewModel;
            if (tab == null)
                return;
            tab.OnRequestClose -= OnTabClosed;
            Tabs.Remove(tab);
        }
        public void SelectNewTab()
        {
            SelectedTab = m_NewTab;
        }
    }
}
