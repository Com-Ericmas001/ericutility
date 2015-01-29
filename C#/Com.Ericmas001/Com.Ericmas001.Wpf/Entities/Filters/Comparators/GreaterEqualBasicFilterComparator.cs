using System;
using System.Collections;
using System.Linq;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters.Comparators
{
    [FilterComparator(FilterComparatorEnum.GreaterEqual)]
    public class GreaterEqualBasicFilterComparator : BasicComparableFilterComparator
    {
        public override bool IsComparableDataFiltered(IComparable comparatorValue, IComparable value)
        {
            return value.CompareTo(comparatorValue) >= 0;
        }
    }
}
