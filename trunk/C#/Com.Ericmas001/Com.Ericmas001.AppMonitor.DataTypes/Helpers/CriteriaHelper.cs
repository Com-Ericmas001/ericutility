using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.AppMonitor.DataTypes.Attributes;
using Com.Ericmas001.AppMonitor.DataTypes.Entities;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Attributes;
using Com.Ericmas001.Wpf.Entities.Enums;

namespace Com.Ericmas001.AppMonitor.DataTypes.Helpers
{
    public class CriteriaHelper<TCriteria, TCategory>
        where TCriteria : struct
        where TCategory : struct
    {
        public static IGrouping<String, TDataItem>[] ObtainGrouping<TDataItem>(TCriteria criteria, IEnumerable<TDataItem> items) where TDataItem : IDataItem
        {
            return items.GroupBy(x => x.ObtainValue(EnumFactory<TCriteria>.ToString(criteria))).ToArray();
        }

        public static string ObtainOrdering<TDataItem>(IEnumerable<TCriteria> usedCriterias, TCriteria criteria, IGrouping<String, TDataItem> group) where TDataItem : IDataItem
        {
            return group.Key;
        }

        public static Dictionary<TCriteria, SearchTypeEnum> GetAllSearchCriterias<TSearchAttribute>(TCategory category)
            where TSearchAttribute : Attribute, ISearchCriteriaAttribute<TCategory>
        {
            return EnumFactory<TCriteria>.AllValues.Where(x => ContainsAttribute<TSearchAttribute>(category, x)).OrderBy(GetPriorite).ThenBy(EnumFactory<TCriteria>.ToString).ToDictionary(c => c, c => EnumFactory<TCriteria>.GetAttribute<TSearchAttribute>(c).SearchType);
        }
        public static TCriteria[] GetAllGroupingCriterias<TGroupingAttribute>(TCategory category)
            where TGroupingAttribute : Attribute, IManyCategoriesAttribute<TCategory>
        {
            return EnumFactory<TCriteria>.AllValues.Where(x => ContainsAttribute<TGroupingAttribute>(category, x)).OrderBy(GetPriorite).ThenBy(EnumFactory<TCriteria>.ToString).ToArray();
        }

        private static int GetPriorite(TCriteria criteria)
        {
            var prioAtt = EnumFactory<TCriteria>.GetAttribute<PriorityAttribute>(criteria);
            return prioAtt != null ? prioAtt.Priority : 0;
        }

        private static bool ContainsAttribute<TAttribute>(TCategory category, TCriteria criteria)
            where TAttribute : Attribute, IManyCategoriesAttribute<TCategory>
        {

            IManyCategoriesAttribute<TCategory> att = EnumFactory<TCriteria>.GetAttribute<TAttribute>(criteria);
            if (att != null && att.Categories.Contains(category))
            {
                return true;
            }

            return false;
        }

        public static string GenerateHeaderTag(TCriteria criteria)
        {
            var tagAtt = EnumFactory<TCriteria>.GetAttribute<TagAttribute>(criteria);
            return tagAtt != null ? tagAtt.Tag : string.Empty;
        }
    }
}
