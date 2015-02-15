using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Util.Entities.Filters.Enums;
using Com.Ericmas001.Wpf.Entities.Filters;
using Com.Ericmas001.Wpf.ViewModels.Sections;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.Wpf.ViewModels.Tabs
{
    public abstract class BaseGroupedContentViewModel<TDataItem> : BaseContentViewModel
        where TDataItem : IDataItem
    {
        private readonly LoadingViewModel m_LoadingTreeVm;

        public LoadingViewModel LoadingTreeVm
        {
            get { return m_LoadingTreeVm; }
        }

        private readonly BunchOfDataItems<TDataItem> m_DataItems;

        public override bool CanCloseTab
        {
            get { return true; }
        }

        protected IEnumerable<TDataItem> Data
        {
            get { return m_DataItems.Data; }
        }

        protected BunchOfDataItems<TDataItem> DataItems
        {
            get { return m_DataItems; }
        }
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

        public BaseGroupedContentViewModel(Dispatcher appCurrentDispatcher, BunchOfDataItems<TDataItem> data)
            : base(appCurrentDispatcher)
        {
            m_DataItems = data;
            m_LoadingTreeVm = new LoadingViewModel(appCurrentDispatcher, BuildTree)
            {
                BigLoadingMessage = BigLoadingTreeMessage,
                SmallLoadingMessage = SmallLoadingTreeMessage
            };
            m_LoadingTreeVm.OnDataObtained += RefreshInterfaceAfterTree;
            m_LoadingTreeVm.OnErrorObtained += m_LoadingTreeVm_OnErrorObtained;
        }

        void m_LoadingTreeVm_OnErrorObtained(object sender, KeyEventArgs<Exception> e)
        {
            Logs.LogError(e.Key.ToString());
        }


        public override void Init()
        {
            InitGroupingAndFiltering();
            ChooseGroupVm.OnGroupsChanged += delegate { RefreshInterface(); };

            if (CanRefresh)
                RefreshDataAndInterface();
        }

        protected virtual void InitGroupingAndFiltering()
        {
            ChooseGroupVm = new ChooseGroupViewModel(GetAllGroupingCriterias(), OrderCriterias, new string[0]);
        }

        public abstract string[] GetAllGroupingCriterias();

        protected virtual IEnumerable<string> OrderCriterias(IEnumerable<string> criterias)
        {
            return criterias.OrderBy(x => x);
        }
        protected virtual IEnumerable<TDataItem> OrderItems(IEnumerable<TDataItem> items, IEnumerable<string> allCriterias)
        {
            return items;
        }
        protected virtual string ObtainOrdering(IEnumerable<string> usedCriterias, string criteria, IGrouping<String, TDataItem> group)
        {
            return group.Key;
        }

        protected virtual string ObtainGroupingValue(TDataItem item, string criteria)
        {
            return item.ObtainValue(criteria);
        }

        public virtual IGrouping<string, TDataItem>[] ObtainGrouping(string criteria, IEnumerable<TDataItem> items)
        {
            return items.GroupBy(x => ObtainGroupingValue(x, criteria)).ToArray();
        }

        protected abstract TreeElementViewModel CreateLeaf(TreeElementViewModel parent, TDataItem item, IEnumerable<string> criteres);
        protected abstract TreeElementViewModel CreateBranch(TreeElementViewModel parent, string currentCritere, string value, IEnumerable<string> usedCriteres);


        protected virtual string[] GetAllFiltersCriteria()
        {
            return GetAllGroupingCriterias();
        }

        public virtual IEnumerable<BaseFilterInCreation> GenerateFilter(string crit)
        {
            return new[] { new SimpleFilterInCreation(crit,FilterEnum.Text,DataItems)  };
        }

        public virtual IEnumerable<string> GroupedCriterias()
        {
            return ChooseGroupVm.ChoosenGroups.Items;
        }

        protected List<TreeElementViewModel> FillTree(TreeElementViewModel node, IEnumerable<TDataItem> items, IEnumerable<string> criterias, IEnumerable<string> allCriterias)
        {
            var result = new List<TreeElementViewModel>();
            var allCriteriasArray = allCriterias as string[] ?? allCriterias.ToArray();
            var criteriasArray = criterias as string[] ?? criterias.ToArray();

            if ((criterias == null | !criteriasArray.Any()))
            {
                foreach (TDataItem elem in OrderItems(items, allCriteriasArray))
                {
                    TreeElementViewModel leaf = CreateLeaf(node, elem, allCriteriasArray);
                    if (leaf != null)
                    {
                        leaf.OnTabCreation += HandlingTabCreation;
                        result.Add(leaf);
                    }
                }
            }
            else
            {
                string currentCriteria = criteriasArray.First();
                string[] usedCriterias = allCriteriasArray.Except(criteriasArray).Union(new[] { currentCriteria }).ToArray();

                IGrouping<string, TDataItem>[] groups = ObtainGrouping(currentCriteria, items);
                if (groups != null)
                {
                    foreach (IGrouping<String, TDataItem> x in groups.OrderBy(g => ObtainOrdering(usedCriterias, currentCriteria, g)))
                    {
                        TreeElementViewModel nextnode = CreateBranch(node, currentCriteria, x.Key, usedCriterias);
                        nextnode.AddChildren(FillTree(nextnode, x, criteriasArray.Skip(1), allCriteriasArray).ToArray());
                        nextnode.OnTabCreation += HandlingTabCreation;
                        result.Add(nextnode);
                    }
                }
            }
            return result;
        }

        protected virtual void GenerateFilters()
        {
            var filters = new Dictionary<string, BaseFilterInCreation[]>();
            foreach (string crit in GetAllFiltersCriteria())
            {
                BaseFilterInCreation[] myfilters = GenerateFilter(crit).ToArray();
                if (myfilters.Any())
                    filters.Add(crit, myfilters);
            }
            ChooseGroupVm.FieldsToFilter = filters;
        }

        private IEnumerable<TDataItem> GetFilteredData()
        {

            TDataItem[] filteredData = Data.ToArray();
            foreach (BaseCompiledFilter cf in ChooseGroupVm.CurrentFilters)
            {
                string crit = cf.Info.FieldToFilter;
                filteredData = filteredData.Where(d => cf.IsSurvivingTheFilter(d.ObtainFilterValue(crit),d)).ToArray();
            }

            return filteredData;
        }
        protected virtual TreeElementViewModel Root { get { return null; } }

        protected virtual void BuildTree(object sender, DoWorkEventArgs e)
        {
            string[] criteres = GroupedCriterias().ToArray();
            GenerateFilters();
            TreeElementViewModel root = Root;
            if(root == null)
                CachedTreeElements = FillTree(root, GetFilteredData(), criteres, criteres);
            else
            {
                root.AddChildren(FillTree(root, GetFilteredData(), criteres, criteres).ToArray());
                CachedTreeElements = new List<TreeElementViewModel>() { root };
            }
        }

        protected virtual void RefreshInterfaceAfterTree()
        {
            if (CachedTreeElements != null && CachedTreeElements.Any())
            { 
                Items.AddItems(CachedTreeElements);
                if (Items.Count == 1)
                    Items[0].IsExpanded = true;
            }
        }

        protected override void RefreshInterface()
        {
            Items.Clear();
            m_LoadingTreeVm.Execute();
        }

        protected void HandlingTabCreation(object sender, BaseTabViewModel tab)
        {
            CreateNewTab(tab);
        }

    }

}
