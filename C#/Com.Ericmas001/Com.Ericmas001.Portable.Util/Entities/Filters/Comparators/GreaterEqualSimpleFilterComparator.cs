using System;
using Com.Ericmas001.Portable.Util.Entities.Filters.Attributes;
using Com.Ericmas001.Portable.Util.Entities.Filters.Enums;

namespace Com.Ericmas001.Portable.Util.Entities.Filters.Comparators
{
    [FilterComparator(FilterComparatorEnum.GreaterEqual)]
    public class GreaterEqualSimpleFilterComparator : ComparableSimpleFilterComparator
    {
        public override bool IsComparableDataFiltered(IComparable comparatorValue, IComparable value, IDataItem item)
        {
            return value.CompareTo(comparatorValue) >= 0;
        }
    }
}
