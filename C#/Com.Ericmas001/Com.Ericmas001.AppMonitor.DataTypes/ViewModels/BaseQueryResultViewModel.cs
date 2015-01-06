using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using Com.Ericmas001.AppMonitor.DataTypes.Attributes;
using Com.Ericmas001.AppMonitor.DataTypes.Helpers;
using Com.Ericmas001.AppMonitor.DataTypes.TreeElements;
using Com.Ericmas001.AppMonitor.DataTypes.ViewModels.Sections;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Wpf.Entities;
using Com.Ericmas001.Wpf.Entities.Enums;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.AppMonitor.DataTypes.ViewModels
{
    public abstract class BaseQueryResultViewModel<TDataItem> : BaseGroupedContentViewModel
        where TDataItem : IDataItem
    {
        private readonly string m_Keyword;
        private readonly string m_SearchCriteria;
        private readonly BunchOfDataItems<TDataItem> m_DataItems;

        public override string TabHeader
        {
            get { return string.Format("{0} {1}", Keyword, SearchCriteria); }
        }

        public override bool CanCloseTab
        {
            get { return true; }
        }

        protected IEnumerable<TDataItem> Data
        {
            get { return m_DataItems.Data; }
        }

        protected string Keyword
        {
            get { return m_Keyword; }
        }

        protected BunchOfDataItems<TDataItem> DataItems
        {
            get { return m_DataItems; }
        }

        protected string SearchCriteria
        {
            get { return m_SearchCriteria; }
        }

        public BaseQueryResultViewModel(Dispatcher appCurrentDispatcher, string keyword, string criteria, BunchOfDataItems<TDataItem> data)
            : base(appCurrentDispatcher)
        {
            m_Keyword = keyword;
            m_SearchCriteria = criteria;
            m_DataItems = data;

            InitGroupingAndFiltering();
            ChooseGroupVm.OnGroupsChanged += delegate { RefreshInterface(); };

            if(CanRefresh)
                RefreshDataAndInterface();
        }

        protected virtual void InitGroupingAndFiltering()
        {
            ChooseGroupVm = new ChooseGroupViewModel(m_DataItems, GetAllGroupingCriterias(), OrderCriterias, new string[0]);
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
            return items.GroupBy(x => ObtainGroupingValue(x,criteria)).ToArray();
        }

        protected abstract BaseLeafTreeElement CreateLeaf(TreeElementViewModel parent, TDataItem item, IEnumerable<string> criteres);
        protected abstract BaseBranchTreeElement CreateBranch(TreeElementViewModel parent, string currentCritere, string value, IEnumerable<string> usedCriteres);


        protected virtual string[] GetAllFiltersCriteria()
        {
            return GetAllGroupingCriterias();
        }

        public virtual IEnumerable<FilterEnum> GenerateFilter(string crit)
        {
            return new FilterEnum[] { FilterEnum.Text };
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
                    BaseLeafTreeElement leaf = CreateLeaf(node, elem, allCriteriasArray);
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
                string[] usedCriterias = allCriteriasArray.Except(criteriasArray).Union(new[] {currentCriteria}).ToArray();

                IGrouping<string, TDataItem>[] groups = ObtainGrouping(currentCriteria, items);
                if (groups != null)
                {
                    foreach (IGrouping<String, TDataItem> x in groups.OrderBy(g => ObtainOrdering(usedCriterias, currentCriteria, g)))
                    {
                        BaseBranchTreeElement nextnode = CreateBranch(node, currentCriteria, x.Key, usedCriterias);
                        nextnode.Children.AddItems(FillTree(nextnode, x, criteriasArray.Skip(1), allCriteriasArray));
                        nextnode.OnTabCreation += HandlingTabCreation;
                        result.Add(nextnode);
                    }
                }
            }
            return result;
        }

        protected virtual void GenerateFilters()
        {
            var filters = new Dictionary<string, FilterEnum[]>();
            foreach (string crit in GetAllFiltersCriteria())
            {
                IEnumerable<FilterEnum> myfilters = GenerateFilter(crit);
                if (myfilters != null && myfilters.Any())
                    filters.Add(crit, myfilters.ToArray());
            }
            ChooseGroupVm.FieldsToFilter = filters;
        }

        private IEnumerable<TDataItem> GetFilteredData()
        {

            TDataItem[] filteredData = Data.ToArray();
            foreach (Filter f in ChooseGroupVm.CurrentFilters)
            {
                string crit = f.Field;
                filteredData = filteredData.Where(d => f.IsSurvivingTheFilter(d.ObtainFilterValue(crit))).ToArray();
            }

            return filteredData;
        }

        protected override void BuildTree(object sender, DoWorkEventArgs e)
        {
            string[] criteres = GroupedCriterias().ToArray();
            GenerateFilters();
            CachedTreeElements = FillTree(null, GetFilteredData(), criteres, criteres);
        }
    }
}
