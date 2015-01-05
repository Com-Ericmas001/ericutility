using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using Com.Ericmas001.AppMonitor.DataTypes.Attributes;
using Com.Ericmas001.AppMonitor.DataTypes.Entities;
using Com.Ericmas001.AppMonitor.DataTypes.Enums;
using Com.Ericmas001.AppMonitor.DataTypes.Helpers;
using Com.Ericmas001.AppMonitor.DataTypes.TreeElements;
using Com.Ericmas001.AppMonitor.DataTypes.ViewModels.Sections;
using Com.Ericmas001.Util;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.AppMonitor.DataTypes.ViewModels
{
    public abstract class BaseSearchResultViewModel<TCategory, TCriteria, TDataItem, TGroupingAttribute> : BaseGroupedContentViewModel
        where TCategory : struct
        where TCriteria : struct
        where TDataItem : IDataItem<TCriteria>
        where TGroupingAttribute : Attribute, IManyCategoriesAttribute<TCategory>
    {
        private CategoryInfo<TCategory> m_CategoryInfo;

        private readonly TCriteria m_SearchCriteria;

        private readonly string m_Keyword;

        protected abstract TCategory Category { get; }
        protected BunchOfDataItems<TDataItem> m_DataItems = new BunchOfDataItems<TDataItem>();

        protected override string IconBigImageName
        {
            get
            {
                InitCatInfo();
                return m_CategoryInfo.IconImageBigName;
            }
        }

        protected override string IconImageName
        {
            get
            {
                InitCatInfo();
                return m_CategoryInfo.IconImageSmallName;
            }
        }

        private void InitCatInfo()
        {
            if (m_CategoryInfo == null)
                m_CategoryInfo = new CategoryInfo<TCategory>(Category);
        }
        public override string TabHeader
        {
            get { return string.Format("{0} {1}", CriteriaHelper<TCriteria, TCategory>.GenerateHeaderTag(SearchCriteria), Keyword); }
        }

        public override bool CanCloseTab
        {
            get { return true; }
        }

        protected IEnumerable<TDataItem> Data
        {
            get { return m_DataItems.Data; }
        }

        protected TCriteria SearchCriteria
        {
            get { return m_SearchCriteria; }
        }

        protected string Keyword
        {
            get { return m_Keyword; }
        }

        public BaseSearchResultViewModel(Dispatcher appCurrentDispatcher, TCriteria criteria, string keyword) : base(appCurrentDispatcher)
        {
            m_SearchCriteria = criteria;
            m_Keyword = keyword;

            InitGroupingAndFiltering();
            ChooseGroupVm.OnGroupsChanged += delegate { RefreshInterface(); };

            RefreshDataAndInterface();
        }

        protected virtual void InitGroupingAndFiltering()
        {
            TCriteria[] allAvailables = CriteriaHelper<TCriteria, TCategory>.GetAllGroupingCriterias<TGroupingAttribute>(Category).Where(crit => !crit.Equals(SearchCriteria)).ToArray();
            IEnumerable<string> availables = allAvailables.Select(EnumFactory<TCriteria>.ToString);
            IEnumerable<string> alreadyGrouped = new String[0];
            ChooseGroupVm = new ChooseGroupViewModel(m_DataItems, availables,CriteriaHelper<TCriteria, TCategory>.OrderCriteriaStrings, alreadyGrouped);
        }

        protected abstract BaseLeafTreeElement CreateLeaf(TreeElementViewModel parent, TDataItem item, IEnumerable<TCriteria> criteres);
        protected abstract BaseBranchTreeElement CreateBranch(TreeElementViewModel parent, TCriteria currentCritere, string value, IEnumerable<TCriteria> usedCriteres, TCategory category);

        protected List<TreeElementViewModel> FillTree(TreeElementViewModel node, IEnumerable<TDataItem> items, IEnumerable<TCriteria> criterias, IEnumerable<TCriteria> allCriterias)
        {
            var result = new List<TreeElementViewModel>();
            var allCriteriasArray = allCriterias as TCriteria[] ?? allCriterias.ToArray();
            var criteriasArray = criterias as TCriteria[] ?? criterias.ToArray();

            if ((criterias == null | !criteriasArray.Any()))
            {
                foreach (TDataItem elem in items.OrderBy(x => x.ObtainDateTime(allCriteriasArray)))
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
                TCriteria currentCriteria = criteriasArray.First();
                TCriteria[] usedCriterias = allCriteriasArray.Except(criteriasArray).Union(new[] {currentCriteria}).ToArray();

                IGrouping<string, TDataItem>[] groups = CriteriaHelper<TCriteria, TCategory>.ObtainGrouping(currentCriteria, items);
                if (groups != null)
                {
                    foreach (IGrouping<String, TDataItem> x in groups.OrderBy(g => CriteriaHelper<TCriteria, TCategory>.ObtainOrdering(usedCriterias, currentCriteria, g)))
                    {
                        BaseBranchTreeElement nextnode = CreateBranch(node, currentCriteria, x.Key, usedCriterias, Category);
                        nextnode.Children.AddItems(FillTree(nextnode, x, criteriasArray.Skip(1), allCriteriasArray));
                        nextnode.OnTabCreation += HandlingTabCreation;
                        result.Add(nextnode);
                    }
                }
            }
            return result;
        }

        private void GenerateFilters()
        {
            var filters = new Dictionary<string, FilterEnum[]>();
            foreach (TCriteria crit in CriteriaHelper<TCriteria, TCategory>.GetAllGroupingCriterias<TGroupingAttribute>(Category).Where(crit => !crit.Equals(SearchCriteria)))
            {
                var filterAtt = EnumFactory<TCriteria>.GetAttribute<FiltersAttribute>(crit);
                if ((filterAtt != null && !filterAtt.Filters.Contains(FilterEnum.None)))
                    filters.Add(EnumFactory<TCriteria>.ToString(crit), filterAtt.Filters.ToArray());
            }
            ChooseGroupVm.FieldsToFilter = filters;
        }

        private IEnumerable<TDataItem> GetFilteredData()
        {

            TDataItem[] filteredData = Data.ToArray();
            foreach (Filter f in ChooseGroupVm.CurrentFilters)
            {
                TCriteria crit = EnumFactory<TCriteria>.Parse(f.Field);
                filteredData = filteredData.Where(d => f.IsSurvivingTheFilter(d.ObtainFilterValue(crit))).ToArray();
            }

            return filteredData;
        }

        protected override void BuildTree(object sender, DoWorkEventArgs e)
        {
            TCriteria[] criteres = new[] { SearchCriteria }.Union(ChooseGroupVm.ChoosenGroups.Items.Select(EnumFactory<TCriteria>.Parse)).ToArray();

            GenerateFilters();

            CachedTreeElements = FillTree(null, GetFilteredData(), criteres, criteres);
        }
    }
}
