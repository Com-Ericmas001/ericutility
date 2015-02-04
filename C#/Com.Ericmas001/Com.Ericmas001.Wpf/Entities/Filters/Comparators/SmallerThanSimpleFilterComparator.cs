using System;
using System.Collections;
using System.Linq;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters.Comparators
{
    [FilterComparator(FilterComparatorEnum.SmallerThan)]
    public class SmallerThanSimpleFilterComparator : ComparableSimpleFilterComparator
    {
        public override bool IsComparableDataFiltered(IComparable comparatorValue, IComparable value, IDataItem item)
        {
            return value.CompareTo(comparatorValue) < 0;
        }
    }
}
