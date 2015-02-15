using System;
using Com.Ericmas001.Util.Entities.Filters.Attributes;
using Com.Ericmas001.Util.Entities.Filters.Enums;

namespace Com.Ericmas001.Util.Entities.Filters.Comparators
{
    [FilterComparator(FilterComparatorEnum.GreaterThan)]
    public class GreaterThanSimpleFilterComparator : ComparableSimpleFilterComparator
    {
        public override bool IsComparableDataFiltered(IComparable comparatorValue, IComparable value, IDataItem item)
        {
            return value.CompareTo(comparatorValue) > 0;
        }
    }
}
