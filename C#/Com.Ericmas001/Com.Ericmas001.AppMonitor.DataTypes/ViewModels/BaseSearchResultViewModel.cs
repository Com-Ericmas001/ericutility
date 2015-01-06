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
    public abstract class BaseSearchResultViewModel<TCategory, TCriteria, TDataItem, TGroupingAttribute> : BaseQueryResultViewModel<TDataItem>
        where TCategory : struct
        where TCriteria : struct
        where TDataItem : IDataItem<TCriteria>
        where TGroupingAttribute : Attribute, IManyCategoriesAttribute<TCategory>
    {
        private CategoryInfo<TCategory> m_CategoryInfo;

        protected abstract TCategory Category { get; }

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

        protected new TCriteria SearchCriteria
        {
            get { return EnumFactory<TCriteria>.Parse(base.SearchCriteria); }
        }

        public BaseSearchResultViewModel(Dispatcher appCurrentDispatcher, TCriteria criteria, string keyword)
            : base(appCurrentDispatcher, keyword, EnumFactory<TCriteria>.ToString(criteria), new BunchOfDataItems<TDataItem>())
        {
        }

        public override string[] GetAllGroupingCriterias()
        {
            return CriteriaHelper<TCriteria, TCategory>.GetAllGroupingCriterias<TGroupingAttribute>(Category).Where(crit => !crit.Equals(SearchCriteria)).Select(x=>EnumFactory<TCriteria>.ToString(x)).ToArray();
        }

        protected abstract BaseLeafTreeElement CreateLeaf(TreeElementViewModel parent, TDataItem item, IEnumerable<TCriteria> criteres);
        protected override BaseLeafTreeElement CreateLeaf(TreeElementViewModel parent, TDataItem item, IEnumerable<string> criteres)
        {
            return CreateLeaf(parent, item, criteres.Select(x => EnumFactory<TCriteria>.Parse(x)));
        }

        protected abstract BaseBranchTreeElement CreateBranch(TreeElementViewModel parent, TCriteria currentCritere, string value, IEnumerable<TCriteria> usedCriteres, TCategory category);
        protected override BaseBranchTreeElement CreateBranch(TreeElementViewModel parent, string currentCritere, string value, IEnumerable<string> usedCriteres)
        {
            return CreateBranch(parent, EnumFactory<TCriteria>.Parse(currentCritere), value, usedCriteres.Select(x => EnumFactory<TCriteria>.Parse(x)), Category);
        }

        protected override IEnumerable<string> OrderCriterias(IEnumerable<string> criterias)
        {
            return CriteriaHelper<TCriteria, TCategory>.OrderCriteriaStrings(criterias);
        }
        protected override IEnumerable<TDataItem> OrderItems(IEnumerable<TDataItem> items, IEnumerable<string> allCriterias)
        {
            return items.OrderBy(x => x.ObtainDateTime(allCriterias.Select(y => EnumFactory<TCriteria>.Parse(y))));
        }
        protected override string ObtainOrdering(IEnumerable<string> usedCriterias, string criteria, IGrouping<string, TDataItem> group)
        {
            return CriteriaHelper<TCriteria, TCategory>.ObtainOrdering(usedCriterias.Select(x => EnumFactory<TCriteria>.Parse(x)), EnumFactory<TCriteria>.Parse(criteria), group);
        }
        public override IGrouping<string, TDataItem>[] ObtainGrouping(string criteria, IEnumerable<TDataItem> items)
        {
            return CriteriaHelper<TCriteria, TCategory>.ObtainGrouping(EnumFactory<TCriteria>.Parse(criteria), items);
        }
        public override IEnumerable<FilterEnum> GenerateFilter(string crit)
        {
            var filterAtt = EnumFactory<TCriteria>.GetAttribute<FiltersAttribute>(EnumFactory<TCriteria>.Parse(crit));
            if ((filterAtt != null && !filterAtt.Filters.Contains(FilterEnum.None)))
                return filterAtt.Filters.ToArray();
            return null;
        }
        public override IEnumerable<string> GroupedCriterias()
        {
            return new[] { EnumFactory<TCriteria>.ToString(SearchCriteria)}.Union(base.GroupedCriterias());
        }
    }
}
