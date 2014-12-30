using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Threading;
using Com.Ericmas001.AppMonitor.DataTypes.ViewModels.Sections;
using Com.Ericmas001.Wpf;
using Com.Ericmas001.Wpf.ViewModels.Tabs;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.AppMonitor.DataTypes.ViewModels
{
    public abstract class BaseGroupedContentViewModel : BaseContentViewModel
    {

        private bool m_IsLoadingTree;

        private BackgroundWorker m_BwTree = new BackgroundWorker();
        private FastObservableCollection<TreeElementViewModel> m_Items = new FastObservableCollection<TreeElementViewModel>();
        private TreeElementViewModel m_SelectedTreeElement;
        private TreeElementViewModel m_SelectedGridElement;


        protected List<TreeElementViewModel> CachedTreeElements { get; set; }


        public virtual bool HasGrouping
        {
            get { return true; }
        }

        public bool IsGroupExpanded { get; set; }
        public ChooseGroupViewModel ChooseGroupVm { get; set; }

        public FastObservableCollection<TreeElementViewModel> Items
        {
            get { return m_Items; }
        }
        public bool IsLoadingTree
        {
            get { return m_IsLoadingTree; }
            set
            {
                if ((m_IsLoadingTree != value))
                {
                    m_IsLoadingTree = value;
                    RaisePropertyChanged("IsLoadingTree");
                }
            }
        }

        public virtual string BigLoadingTreeMessage
        {
            get { return "Loading DataTree ..."; }
        }
        public virtual string SmallLoadingTreeMessage
        {
            get { return "Preparing Result Tree ..."; }
        }
        public TreeElementViewModel SelectedTreeElement
        {
            get { return m_SelectedTreeElement; }
            set
            {
                m_SelectedTreeElement = value;
                RaisePropertyChanged("SelectedTreeElement");
                RaisePropertyChanged("HasSomethingTreeElementSelected");
            }
        }
        public bool HasSomethingTreeElementSelected
        {
            get { return m_SelectedTreeElement != null; }
        }

        public TreeElementViewModel SelectedGridElement
        {
            get { return m_SelectedGridElement; }
            set
            {
                m_SelectedGridElement = value;
                RaisePropertyChanged("SelectedGridElement");
            }
        }

        public BaseGroupedContentViewModel(Dispatcher appCurrentDispatcher)
            : base(appCurrentDispatcher)
        {
            m_BwTree.DoWork += BuildTree;
            m_BwTree.RunWorkerCompleted += TreeBuilded;
        }

        protected abstract void BuildTree(object sender, DoWorkEventArgs e);

        protected virtual void TreeBuilded(object sender, RunWorkerCompletedEventArgs e)
        {
            IsLoadingTree = false;
            RefreshInterfaceAfterTreeAsync();
        }
        protected virtual void RefreshInterfaceAfterTree()
        {
            Items.AddItems(CachedTreeElements);
            if (Items.Count == 1)
                Items[0].IsExpanded = true;
        }

        protected override void RefreshInterface()
        {
            Items.Clear();
            IsLoadingTree = true;
            if (!m_BwTree.IsBusy)
                m_BwTree.RunWorkerAsync();
        }

        protected void RefreshInterfaceAfterTreeAsync()
        {
            AppCurrentDispatcher.Invoke(RefreshInterfaceAfterTree);
        }

        protected void HandlingTabCreation(object sender, BaseTabViewModel tab)
        {
            CreateNewTab(tab);
        }

    }

}
